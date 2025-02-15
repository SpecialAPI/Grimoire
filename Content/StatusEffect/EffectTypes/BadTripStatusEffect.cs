using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.StatusEffect.EffectTypes
{
    public class BadTripStatusEffect : StatusEffect_SO
    {
        public override bool IsPositive => false;

        public override void OnTriggerAttached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, TriggerCalls.OnStatusEffectContentAdded.ToString(), caller);
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_02, TriggerCalls.OnTurnFinished.ToString(), caller);
        }

        public override void OnTriggerDettached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, TriggerCalls.OnStatusEffectContentAdded.ToString(), caller);
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_02, TriggerCalls.OnTurnFinished.ToString(), caller);
        }

        public override void OnEventCall_01(StatusEffect_Holder holder, object sender, object args)
        {
            if (sender is not IStatusEffector effector || args is not StatusFieldApplication application)
                return;

            if (application.statusID == StatusID || application.isStatusPositive)
                return;

            var status = (IStatusEffect)null;

            foreach(var st in effector.StatusEffects)
            {
                if (st == null || st.StatusID != application.statusID)
                    continue;

                status = st;
                break;
            }

            if (status == null)
                return;

            var oldContent = status.StatusContent;
            var res = status.TryIncreaseContent(holder.StatusContent + holder.Restrictor);

            if (!res)
                return;

            effector.StatusEffectValuesChanged(status.StatusID, status.StatusContent - oldContent, true);
        }

        public override void OnEventCall_02(StatusEffect_Holder holder, object sender, object args)
        {
            if (sender is not IStatusEffector effector)
                return;

            ReduceDuration(holder, effector);
        }
    }
}
