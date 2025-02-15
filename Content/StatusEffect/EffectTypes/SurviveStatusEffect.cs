using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Grimoire.Content.StatusEffect.EffectTypes
{
    public class SurviveStatusEffect : StatusEffect_SO
    {
        public override bool IsPositive => true;

        public override void OnTriggerAttached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, TriggerCalls.OnBeingDamaged.ToString(), caller);
        }

        public override void OnTriggerDettached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, TriggerCalls.OnBeingDamaged.ToString(), caller);
        }

        public override void OnEventCall_01(StatusEffect_Holder holder, object sender, object args)
        {
            if (args is not DamageReceivedValueChangeException ex || sender is not IStatusEffector effector || sender is not IUnit unit)
                return;

            ex.AddModifier(new SurviveIntValueModifier(1, this, effector, holder, unit));
        }
    }

    public class SurviveIntValueModifier(int survivingHealth, SurviveStatusEffect survive, IStatusEffector effector, StatusEffect_Holder holder, IUnit unit) : IntValueModifier(200)
    {
        public override int Modify(int value)
        {
            if (unit.CurrentHealth - value >= survivingHealth)
                return value;

            survive.ReduceDuration(holder, effector);
            return Mathf.Max(0, unit.CurrentHealth - survivingHealth);
        }
    }
}
