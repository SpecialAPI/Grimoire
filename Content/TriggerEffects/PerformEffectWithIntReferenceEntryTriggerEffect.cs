using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.TriggerEffects
{
    public class PerformEffectWithIntReferenceEntryTriggerEffect(List<EffectInfo> effects) : TriggerEffect
    {
        public List<EffectInfo> effects = effects;

        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if(!ValueReferenceTools.TryGetIntHolder(args, out var intRef))
                return;

            if (triggerInfo.immediate)
                CombatManager.Instance.ProcessImmediateAction(new ImmediateEffectAction([.. effects], sender, intRef.Value));
            else
                CombatManager.Instance.AddSubAction(new EffectAction([.. effects], sender, intRef.Value));
        }
    }
}
