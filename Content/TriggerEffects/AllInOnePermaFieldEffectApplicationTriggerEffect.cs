using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.TriggerEffects
{
    public class AllInOnePermaFieldEffectApplicationTriggerEffect : TriggerEffect
    {
        public List<int> targetOffsets;
        public bool applyOnAllySlots;

        public FieldEffect_SO field;
        public int amount;

        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if (extraInfo.activation == TriggerEffectActivation.Connection)
            {
                if (!extraInfo.TryGetPopupUIAction(sender.ID, sender.IsUnitCharacter, false, out var action))
                    action = null;

                CombatManager.Instance.AddSubAction(new PermaFieldEffectConnectedAction(sender, field, amount, targetOffsets, applyOnAllySlots, action));
            }

            else if (extraInfo.activation == TriggerEffectActivation.Disconnection)
                CombatManager.Instance.AddSubAction(new PermaFieldEffectDisconnectedAction(sender, field, amount, targetOffsets, applyOnAllySlots));

            else if (extraInfo.activation == TriggerEffectActivation.Trigger && ValueReferenceTools.TryGetIntHolder(args, out var intRef))
                CombatManager.Instance.ProcessImmediateAction(new PermaFieldEffectMoveAction(sender, intRef.Value, field, amount, targetOffsets, applyOnAllySlots));
        }

        public override bool ManuallyHandlePopup => true;
    }

    public class PermaFieldEffectConnectedAction(IUnit unit, FieldEffect_SO field, int amount, List<int> targetOffsets, bool applyOnAllySlots, CombatAction popupUIAction) : CombatAction
    {
        public override IEnumerator Execute(CombatStats stats)
        {
            if (!unit.IsAlive)
                yield break;

            if(unit.IsUnitCharacter && stats.TryGetCharacterOnField(unit.ID) == null)
                yield break;
            else if(!unit.IsUnitCharacter && stats.TryGetEnemyOnField(unit.ID) == null)
                yield break;

            if (popupUIAction != null)
                yield return popupUIAction.Execute(stats);

            if(targetOffsets == null || targetOffsets.Count <= 0 || field == null)
                yield break;

            var character = applyOnAllySlots == unit.IsUnitCharacter;

            foreach (var offs in targetOffsets)
            {
                var slot = offs + unit.SlotID;
                var size = offs == 0 ? unit.Size : 1;

                if (offs > 0)
                    slot += unit.Size - 1;

                stats.combatSlots.ApplyFieldEffect(slot, character, field, 0, amount, size);
            }
        }
    }

    public class PermaFieldEffectDisconnectedAction(IUnit unit, FieldEffect_SO field, int amount, List<int> targetOffsets, bool applyOnAllySlots) : CombatAction
    {
        public override IEnumerator Execute(CombatStats stats)
        {
            if (targetOffsets == null || targetOffsets.Count <= 0 || field == null)
                yield break;

            var character = applyOnAllySlots == unit.IsUnitCharacter;
            var slots = character ? stats.combatSlots.CharacterSlots : stats.combatSlots.EnemySlots;

            foreach (var offs in targetOffsets)
            {
                var firstSID = offs + unit.SlotID;
                var size = offs == 0 ? unit.Size : 1;

                if (offs > 0)
                    firstSID += unit.Size - 1;

                for(var sid = firstSID; sid < firstSID + size; sid++)
                {
                    var slot = slots[sid];

                    if(slot == null)
                        continue;

                    var effects = slot.FieldEffects;
                    for (var i = effects.Count - 1; i >= 0; i--)
                    {
                        var fe = effects[i];

                        if (fe == null || !fe.IsField(field.FieldID))
                            continue;

                        fe.TryRemoveAnyContent(0, amount);
                        break;
                    }
                }
            }
        }
    }

    public class PermaFieldEffectMoveAction(IUnit unit, int oldSlot, FieldEffect_SO field, int amount, List<int> targetOffsets, bool applyOnAllySlots) : IImmediateAction
    {
        public void Execute(CombatStats stats)
        {
            if (targetOffsets == null || targetOffsets.Count <= 0 || field == null)
                return;

            var character = applyOnAllySlots == unit.IsUnitCharacter;
            var slots = character ? stats.combatSlots.CharacterSlots : stats.combatSlots.EnemySlots;

            foreach (var offs in targetOffsets)
            {
                var firstSID = offs + oldSlot;
                var size = offs == 0 ? unit.Size : 1;

                if (offs > 0)
                    firstSID += unit.Size - 1;

                for (var sid = firstSID; sid < firstSID + size; sid++)
                {
                    var slot = slots[sid];

                    if (slot == null)
                        continue;

                    var effects = slot.FieldEffects;
                    for (var i = effects.Count - 1; i >= 0; i--)
                    {
                        var fe = effects[i];

                        if (fe == null || !fe.IsField(field.FieldID))
                            continue;

                        fe.TryRemoveAnyContent(0, amount);
                        break;
                    }
                }
            }

            foreach (var offs in targetOffsets)
            {
                var slot = offs + unit.SlotID;
                var size = offs == 0 ? unit.Size : 1;

                if (offs > 0)
                    slot += unit.Size - 1;

                stats.combatSlots.ApplyFieldEffect(slot, character, field, 0, amount, size);
            }
        }
    }
}
