using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.TriggerEffects
{
    public class ConnoisseurDamageModifierSetterTriggerEffect : TriggerEffect
    {
        public override void DoEffect(IUnit sender, object args, TriggeredEffect triggerInfo, TriggerEffectExtraInfo extraInfo)
        {
            if (args is not DamageDealtValueChangeException ex)
                return;

            if (ex.damagedUnit is not IStatusEffector effector || effector.StatusEffects.Count <= 0)
                return;

            if (triggerInfo.doesPopup && extraInfo.TryGetPopupUIAction(sender.ID, sender.IsUnitCharacter, false, out var act))
                CombatManager.Instance.AddUIAction(act);

            ex.AddModifier(new PercentageValueModifier(true, Mathf.CeilToInt(effector.StatusEffects.Count * 100f / 3f), true));
        }

        public override bool ManuallyHandlePopup => true;
    }
}
