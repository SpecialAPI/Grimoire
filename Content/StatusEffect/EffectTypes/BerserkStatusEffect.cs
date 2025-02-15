﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.StatusEffect.EffectTypes
{
    public class BerserkStatusEffect : StatusEffect_SO
    {
        public override bool IsPositive => true;

        public override void OnTriggerAttached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, TriggerCalls.OnWillApplyDamage.ToString(), caller);
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_02, TriggerCalls.OnTurnFinished.ToString(), caller);
        }

        public override void OnTriggerDettached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, TriggerCalls.OnWillApplyDamage.ToString(), caller);
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_02, TriggerCalls.OnTurnFinished.ToString(), caller);
        }

        public override void OnEventCall_01(StatusEffect_Holder holder, object sender, object args)
        {
            if (args is not DamageDealtValueChangeException ex)
                return;

            ex.AddModifier(new PercentageValueModifier(true, 100, true));
        }

        public override void OnEventCall_02(StatusEffect_Holder holder, object sender, object args)
        {
            if (sender is not IStatusEffector effector)
                return;

            ReduceDuration(holder, effector);
        }
    }
}
