using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.TriggerEffects
{
    public class ConnoisseurDamageModifierSetterTriggerEffect : TriggerEffect
    {
        public override void DoEffect(IUnit sender, object args, TriggeredEffect triggerInfo, object activator = null)
        {
            if (args is not DamageDealtValueChangeException ex)
                return;

            if (ex.damagedUnit is not IStatusEffector effector || effector.StatusEffects.Count <= 0)
                return;

            if (activator.TryGetActivatorNameAndSprite(out var name, out var sprite))
                CombatManager.Instance.AddUIAction(new ShowPassiveInformationUIAction(sender.ID, sender.IsUnitCharacter, name, sprite));

            ex.AddModifier(new PercentageValueModifier(true, Mathf.CeilToInt(effector.StatusEffects.Count * 100f / 3f), true));
        }
    }
}
