using System;
using System.Collections.Generic;
using System.Text;
using static UnityEngine.UIElements.StyleSheets.Dimension;

namespace Grimoire.Content.FieldEffects.EffectTypes
{
    public class ShadowHandsFieldEffect : FieldEffect_SO
    {
        public override bool IsPositive => false;

        public override bool CanBeRemoved(FieldEffect_Holder holder)
        {
            return true;
        }

        public override void OnTriggerAttached(FieldEffect_Holder holder, IUnit caller)
        {
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, TriggerCalls.OnAbilityUsed.ToString(), caller);
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_02, TriggerCalls.OnDamaged.ToString(), caller);
        }

        public override void OnTriggerDettached(FieldEffect_Holder holder, IUnit caller)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, TriggerCalls.OnAbilityUsed.ToString(), caller);
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_02, TriggerCalls.OnDamaged.ToString(), caller);
        }

        public override void OnSubActionTrigger(FieldEffect_Holder holder, object sender, object args, bool stateCheck)
        {
            CombatManager.Instance.ProcessImmediateAction(new ShadowHandsSwap(sender as IUnit, CombatType_GameIDs.Swap_Mass.ToString()));
        }

        public override void OnEventCall_01(FieldEffect_Holder holder, object sender, object args)
        {
            CombatManager.Instance.AddSubAction(new PerformSlotStatusEffectAction(holder, sender, args, false));
        }

        public override void OnEventCall_02(FieldEffect_Holder holder, object sender, object args)
        {
            CombatManager.Instance.AddSubAction(new PerformSlotStatusEffectAction(holder, sender, args, false));
            ReduceDuration(holder);
        }

        public override void OnSlotEffectorTriggerAttached(FieldEffect_Holder holder)
        {
        }

        public override void OnSlotEffectorTriggerDettached(FieldEffect_Holder holder)
        {
        }
    }

    public class ShadowHandsSwap(IUnit unit, string swapTypeID) : IImmediateAction
    {
        public void Execute(CombatStats stats)
        {
            if (unit.IsUnitCharacter)
            {
                var move = (Random.Range(0, 2) * 2) - 1;
                if (unit.SlotID + move >= 0 && unit.SlotID + move < stats.combatSlots.CharacterSlots.Length)
                {
                    stats.combatSlots.SwapCharacters(unit.SlotID, unit.SlotID + move, true, swapTypeID);
                    return;
                }

                move *= -1;
                if (unit.SlotID + move < 0 || unit.SlotID + move >= stats.combatSlots.CharacterSlots.Length)
                    return;

                stats.combatSlots.SwapCharacters(unit.SlotID, unit.SlotID + move, true, swapTypeID);
            }
            else
            {
                var move = (Random.Range(0, 2) * (unit.Size + 1)) - 1;
                if (stats.combatSlots.CanEnemiesSwap(unit.SlotID, unit.SlotID + move, out var firstSlotSwap, out var secondSlotSwap))
                {
                    stats.combatSlots.SwapEnemies(unit.SlotID, firstSlotSwap, unit.SlotID + move, secondSlotSwap, true, swapTypeID);
                    return;
                }

                move = (move < 0) ? unit.Size : (-1);
                if (!stats.combatSlots.CanEnemiesSwap(unit.SlotID, unit.SlotID + move, out firstSlotSwap, out secondSlotSwap))
                    return;

                stats.combatSlots.SwapEnemies(unit.SlotID, firstSlotSwap, unit.SlotID + move, secondSlotSwap, true, swapTypeID);
            }
        }
    }
}
