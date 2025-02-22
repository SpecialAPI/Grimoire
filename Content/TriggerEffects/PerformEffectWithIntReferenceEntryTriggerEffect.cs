using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.TriggerEffects
{
    public class PerformEffectWithIntReferenceEntryTriggerEffect(List<EffectInfo> effects) : TriggerEffect
    {
        public List<EffectInfo> effects = effects;

        public override void DoEffect(IUnit sender, object args, TriggeredEffect triggerInfo, TriggerEffectExtraInfo extraInfo)
        {
            if(!args.TryGetIntReference(out var intRef))
                return;

            if (triggerInfo.immediate)
                CombatManager.Instance.ProcessImmediateAction(new ImmediateEffectAction([.. effects], sender, intRef.value));
            else
                CombatManager.Instance.AddSubAction(new EffectAction([.. effects], sender, intRef.value));
        }
    }
}
