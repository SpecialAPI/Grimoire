using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.EffectorConditions
{
    public class DamageReceivedValueChangeDetectionWithDamageTypeEffectorCondition : EffectorConditionSO
    {
        public string damageType;
        public bool damageTypeMustMatch;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if(args is not DamageReceivedValueChangeException ex)
                return false;

            var matches = ex.damageTypeID == damageType;
            return matches == damageTypeMustMatch;
        }
    }
}
