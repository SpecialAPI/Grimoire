using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.TriggerEffects
{
    public class AddRandomExtraAbilityToPerformedTriggerEffect : TriggerEffect
    {
        public List<ExtraAbilityInfo> abilites;

        public override void DoEffect(IUnit sender, object args, TriggeredEffect triggerInfo, object activator = null)
        {
            if (args is not List<string> performed)
                return;

            if (abilites == null || abilites.Count <= 0)
                return;

            var ab = abilites[Random.Range(0, abilites.Count)];

            if (ab == null || ab.ability == null || string.IsNullOrEmpty(ab.ability.name))
                return;

            performed.Add(ab.ability.name);
        }
    }
}
