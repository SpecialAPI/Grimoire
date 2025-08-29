using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.TriggerEffects
{
    public class AddOrRemoveExtraAbilitiesTriggerEffect : TriggerEffect
    {
        public List<ExtraAbilityInfo> abilites;
        public bool remove;

        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if (abilites == null)
                return;

            foreach (var ab in abilites)
            {
                if(remove)
                    sender.TryRemoveExtraAbility(ab);
                else
                    sender.AddExtraAbility(ab);
            }
        }
    }
}
