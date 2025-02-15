using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.StatusEffect.EffectTypes
{
    public class SaltedStatusEffect : StatusEffect_SO
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

        public override void OnEventCall_01(StatusEffect_Holder holder, object sender, object args)
        {
            if (sender is not IStatusEffector effector)
                return;

            for(var i = 0; i < effector.StatusEffects.Count; i++)
            {
                var status = effector.StatusEffects[i];

                if(status.StatusID == StatusID)
                    continue;

                var oldContent = status.StatusContent;
                var success = status.TryIncreaseContent(2);

                if (!success)
                    continue;

                effector.StatusEffectValuesChanged(status.StatusID, status.StatusContent - oldContent, true);

                var application = new StatusFieldApplication(status.StatusID, status.IsPositive, status.StatusContent);
                CombatManager.Instance.PostNotification(TriggerCalls.OnStatusEffectContentAdded.ToString(), effector, application);
            }

            ReduceDuration(holder, effector);
        }
    }
}
