using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Grimoire.Content.TriggerEffects
{
    public class DamageCapDamageModifierSetterTriggerEffect : TriggerEffect
    {
        public int damageCapMax;
        public UnitStoreData_BasicSO damageCapStoredValue;

        public override void DoEffect(IUnit sender, object args, TriggerEffectInfo triggerInfo, TriggerEffectActivationExtraInfo extraInfo)
        {
            if (args is not DamageReceivedValueChangeException ex)
                return;

            var cap = damageCapMax;

            if (damageCapStoredValue != null)
                cap += sender.SimpleGetStoredValue(damageCapStoredValue._UnitStoreDataID);

            cap = Mathf.Max(cap, 0);

            if (!triggerInfo.doesPopup || !extraInfo.TryGetPopupUIAction(sender.ID, sender.IsUnitCharacter, false, out var action))
                action = null;

            ex.AddModifier(new DamageCapIntValueModifier(cap, action));
        }

        public override bool ManuallyHandlePopup => true;
    }

    public class DamageCapIntValueModifier(int damageCapMax, CombatAction popupUIAction = null) : IntValueModifier(98)
    {
        public override int Modify(int value)
        {
            if (value <= damageCapMax)
                return value;

            if (popupUIAction != null)
                CombatManager.Instance.AddUIAction(popupUIAction);

            return damageCapMax;
        }
    }
}
