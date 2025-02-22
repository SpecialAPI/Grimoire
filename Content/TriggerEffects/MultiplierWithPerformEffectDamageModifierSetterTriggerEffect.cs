using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.TriggerEffects
{
    public class MultiplierWithPerformEffectDamageModifierSetterTriggerEffect : TriggerEffect
    {
        public int multiplier;
        public List<EffectInfo> effects;

        public override void DoEffect(IUnit sender, object args, TriggeredEffect triggerInfo, TriggerEffectExtraInfo extraInfo)
        {
            if (args is not DamageReceivedValueChangeException ex)
                return;

            if (!triggerInfo.doesPopup || !extraInfo.TryGetPopupUIAction(sender.ID, sender.IsUnitCharacter, false, out var action))
                action = null;

            ex.AddModifier(new MultiplierWithPerformEffectIntValueModifier(multiplier, effects, sender, action));
        }

        public override bool ManuallyHandlePopup => true;
    }

    public class MultiplierWithPerformEffectIntValueModifier(int multiplier, List<EffectInfo> effects, IUnit unit, CombatAction popupUIAction) : IntValueModifier(73)
    {
        public override int Modify(int value)
        {
            if (value <= 0)
                return value;

            if (popupUIAction != null)
                CombatManager.Instance.AddUIAction(popupUIAction);

            if (effects != null && effects.Count > 0)
                CombatManager.Instance.AddSubAction(new EffectAction([.. effects], unit));

            return value * multiplier;
        }
    }
}
