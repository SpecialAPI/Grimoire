using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.TriggerEffects
{
    public class InterpolatedDamageModifierSetterTriggerEffect : TriggerEffect
    {
        public StatusEffect_SO status;

        public override void DoEffect(IUnit sender, object args, TriggeredEffect triggerInfo, TriggerEffectExtraInfo extraInfo)
        {
            if (args is not DamageReceivedValueChangeException ex)
                return;

            if (!triggerInfo.doesPopup || !extraInfo.TryGetPopupUIAction(sender.ID, sender.IsUnitCharacter, false, out var action))
                action = null;

            ex.AddModifier(new InterpolatedIntValueModifier(status, sender, ex, action));
        }

        public override bool ManuallyHandlePopup => true;
    }

    public class InterpolatedIntValueModifier(StatusEffect_SO status, IUnit unit, DamageReceivedValueChangeException exception, CombatAction popupUIAction = null) : IntValueModifier(101)
    {
        public override int Modify(int value)
        {
            if (value <= 0)
                return value;

            exception.ShouldIgnoreUI = true;
            if (popupUIAction == null)
                CombatManager.Instance.AddUIAction(popupUIAction);

            if (unit.ApplyStatusEffect(status, value))
                return 0;

            return value;
        }
    }
}
