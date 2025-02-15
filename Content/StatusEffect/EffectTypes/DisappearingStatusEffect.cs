using Grimoire.Content.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.StatusEffect.EffectTypes
{
    public class DisappearingStatusEffect : StatusEffect_SO
    {
        public override bool IsPositive => false;

        public override void OnTriggerAttached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, TriggerCalls.OnTurnFinished.ToString(), caller);
        }

        public override void OnTriggerDettached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, TriggerCalls.OnTurnFinished.ToString(), caller);
        }

        public override void OnSubActionTrigger(StatusEffect_Holder holder, object sender, object args, bool stateCheck)
        {
            if (sender is IUnit u)
            {
                var isConstricted = false;

                for (int i = 0; i < u.Size; i++)
                {
                    if (!CombatManager.Instance._stats.combatSlots.UnitInSlotContainsFieldEffect(u.SlotID + i, u.IsUnitCharacter, StatusField_GameIDs.Constricted_ID.ToString()))
                        continue;

                    isConstricted = true;
                    break;
                }

                var dmg = 0;

                if (!isConstricted)
                    dmg = Mathf.CeilToInt((holder.StatusContent + holder.Restrictor) / 2f);

                u.Damage(dmg, null, DeathType_GameIDs.Basic.ToString(), -1, true, true, false, CustomDamageTypes.DisappearingDamage);
            }

            if(sender is IStatusEffector effector)
                ReduceDuration(holder, effector);
        }

        public override void OnEventCall_01(StatusEffect_Holder holder, object sender, object args)
        {
            CombatManager.Instance.AddSubAction(new PerformStatusEffectAction(holder, sender, args, false));
        }

        public override void ReduceDuration(StatusEffect_Holder holder, IStatusEffector effector)
        {
            if (!CanReduceDuration)
                return;

            int contentMain = holder.m_ContentMain;
            holder.m_ContentMain /= 2;

            if (TryRemoveStatusEffect(holder, effector) || contentMain == holder.m_ContentMain)
                return;

            effector.StatusEffectValuesChanged(_StatusID, holder.m_ContentMain - contentMain, true);
        }
    }
}
