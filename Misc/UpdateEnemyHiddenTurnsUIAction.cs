using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UIElements;

namespace Grimoire.Misc
{
    public class UpdateEnemyHiddenTurnsUIAction(IUnit unit, bool shouldBeHidden) : CombatAction
    {
        public override IEnumerator Execute(CombatStats stats)
        {
            var vis = stats.combatUI;
            var tl = vis._TimelineHandler;
            var didChangeTurns = false;

            if(tl is TimelineLayoutHandler_TurnBased tb)
            {
                var modifiedSlots = new List<int>();
                var newTimelineInfo = new List<TimelineInfo>();

                for (var i = 0; i < tb.TimelineSlotInfo.Count; i++)
                {
                    var slot = tb.TimelineSlotInfo[i];

                    if (slot.isPlayer)
                        continue;

                    if(slot.enemyID != unit.ID)
                        continue;

                    if(shouldBeHidden == slot.isSecret)
                        continue;

                    var en = tb.EnemiesInCombat[slot.enemyID];

                    slot.isSecret = shouldBeHidden;
                    slot.timelineIcon = slot.isSecret ? null : en.Portrait;

                    var abSlotID = slot.abilitySlotID;
                    if(abSlotID >= 0 && abSlotID < en.AbilityTimelineSlots.Count)
                    {
                        if (slot.isSecret)
                            en.AbilityTimelineSlots[abSlotID].Remove(i);
                        else
                            en.AbilityTimelineSlots[abSlotID].Add(i);
                    }

                    modifiedSlots.Add(i);
                    newTimelineInfo.Add(slot);
                    tb.TimelineSlotInfo[i] = slot;
                }

                if(modifiedSlots.Count > 0)
                {
                    didChangeTurns = true;
                    InstantUpdateTimelineSlots(tb.TimelineLayout, [.. modifiedSlots], [.. newTimelineInfo]);
                }
            }
            else if(tl is TimelineLayoutHandler_RealTime rt)
            {
            }

            if (didChangeTurns && !vis._isInfoFromCharacter && vis._unitInfoID != -1)
                vis.TryUpdateEnemyIDInformation(vis._unitInfoID);

            yield break;
        }

        public static void InstantUpdateTimelineSlots(TimelineZoneLayout tl, int[] modifiedSlots, TimelineInfo[] newTimelineInfo)
        {
            for (int i = 0; i < modifiedSlots.Length; i++)
            {
                var timelineInfo = newTimelineInfo[i];
                int slotIndex = modifiedSlots[i];

                Sprite icon;
                Sprite[] intents;
                Color[] spriteColors;
                if (timelineInfo.timelineIcon == null)
                {
                    icon = tl._blindTimelineIcon;
                    intents = null;
                    spriteColors = null;
                }
                else
                {
                    icon = timelineInfo.timelineIcon;
                    intents = tl.IntentHandler.GenerateSpritesFromAbility(timelineInfo.ability, false, out spriteColors);
                }

                tl._slotsInUse[slotIndex - 1].SetInformation(slotIndex, icon, raycastable: true, intents, spriteColors);
            }
        }
    }
}
