using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.TriggerEffects
{
    public class InterpolatedDamageModifierSetterTriggerEffect : TriggerEffect
    {
        public StatusEffect_SO status;

        public override void DoEffect(IUnit sender, object args, TriggeredEffect triggerInfo, object activator = null)
        {
            if (args is not DamageReceivedValueChangeException ex)
                return;

            if (!activator.TryGetActivatorNameAndSprite(out var name, out var sprite))
            {
                name = null;
                sprite = null;
            }

            ex.AddModifier(new InterpolatedIntValueModifier(status, sender, ex, name, sprite));
        }
    }

    public class InterpolatedIntValueModifier(StatusEffect_SO status, IUnit unit, DamageReceivedValueChangeException exception, string passiveName, Sprite passiveSprite) : IntValueModifier(101)
    {
        public override int Modify(int value)
        {
            if (value <= 0)
                return value;

            exception.ShouldIgnoreUI = true;
            if (unit != null && (!string.IsNullOrEmpty(passiveName) || passiveSprite != null))
                CombatManager.Instance.AddUIAction(new ShowPassiveInformationUIAction(unit.ID, unit.IsUnitCharacter, passiveName, passiveSprite));

            if (unit.ApplyStatusEffect(status, value))
                return 0;

            return value;
        }
    }
}
