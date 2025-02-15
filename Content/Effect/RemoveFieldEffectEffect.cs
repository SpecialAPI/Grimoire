using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.Effect
{
    public class RemoveFieldEffectEffect : EffectSO
    {
        public FieldEffect_SO field;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach(var t in targets)
            {
                if(t == null || t.SlotID < 0)
                    continue;

                var cSlots = stats.combatSlots;
                var slots = t.IsTargetCharacterSlot ? cSlots.CharacterSlots : cSlots.EnemySlots;

                if (slots == null || t.SlotID >= slots.Length)
                    continue;

                var slot = slots[t.SlotID];
                exitAmount += slot.TryRemoveFieldEffect(field.FieldID);
            }

            return exitAmount > 0;
        }
    }
}
