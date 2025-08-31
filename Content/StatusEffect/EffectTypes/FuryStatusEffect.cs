using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.StatusEffect.EffectTypes
{
    public class FuryStatusEffect : StatusEffect_SO
    {
        public override bool IsPositive => true;

        public override void OnTriggerAttached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, CustomTriggers.OnBeforeAbilityEffects, caller);
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_02, TriggerCalls.OnTurnFinished.ToString(), caller);
        }

        public override void OnTriggerDettached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, CustomTriggers.OnBeforeAbilityEffects, caller);
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_02, TriggerCalls.OnTurnFinished.ToString(), caller);
        }

        public override void OnEventCall_01(StatusEffect_Holder holder, object sender, object args)
        {
            if (args is not AbilityUsedContext ctx || sender is not IUnit unit || sender is not IStatusEffector effector || ctx.ability == null)
                return;

            var repeats = holder.m_ContentMain + holder.Restrictor;

            for (int i = 0; i < repeats; i++)
            {
                var effects = new EffectAction(ctx.ability.effects, unit);

                CombatManager.Instance.AddSubAction(effects);
                CombatManager.Instance.AddSubAction(new ReduceStatusDurationAction(this, holder, effector));
            }
        }

        public override void OnEventCall_02(StatusEffect_Holder holder, object sender, object args)
        {
            if (sender is not IStatusEffector effector)
                return;

            ReduceDuration(holder, effector);
        }
    }

    public class ReduceStatusDurationAction(StatusEffect_SO effect, StatusEffect_Holder hold, IStatusEffector effector) : CombatAction
    {
        public override IEnumerator Execute(CombatStats stats)
        {
            effect.ReduceDuration(hold, effector);
            yield break;
        }
    }
}
