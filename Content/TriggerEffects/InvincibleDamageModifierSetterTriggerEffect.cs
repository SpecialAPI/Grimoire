using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.TriggerEffects
{
    public class InvincibleDamageModifierSetterTriggerEffect : TriggerEffect
    {
        public int invincibility;
        public UnitStoreData_BasicSO invincibilityStoredValue;

        public override void DoEffect(IUnit sender, object args, TriggeredEffect triggerInfo, TriggerEffectExtraInfo extraInfo)
        {
            if (args is not DamageReceivedValueChangeException ex)
                return;

            var inv = invincibility;

            if (invincibilityStoredValue != null)
                inv += sender.SimpleGetStoredValue(invincibilityStoredValue._UnitStoreDataID);

            inv = Mathf.Max(inv, 0);

            if (!triggerInfo.doesPopup || !extraInfo.TryGetPopupUIAction(sender.ID, sender.IsUnitCharacter, false, out var action))
                action = null;

            ex.AddModifier(new InvincibleIntValueModifier(inv, action));
        }

        public override bool ManuallyHandlePopup => true;
    }

    public class InvincibleIntValueModifier(int invincibility, CombatAction popupUIAction) : IntValueModifier(100)
    {
        public override int Modify(int value)
        {
            if (value > invincibility)
                return value;

            if (popupUIAction != null)
                CombatManager.Instance.AddUIAction(popupUIAction);

            return 0;
        }
    }
}
