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

        public override void DoEffect(IUnit sender, object args, TriggeredEffect triggerInfo, object activator = null)
        {
            if (args is not DamageReceivedValueChangeException ex)
                return;

            var cap = damageCapMax;

            if (damageCapStoredValue != null)
                cap += sender.SimpleGetStoredValue(damageCapStoredValue._UnitStoreDataID);

            cap = Mathf.Max(cap, 0);

            if(!activator.TryGetActivatorNameAndSprite(out var name, out var sprite))
            {
                name = null;
                sprite = null;
            }

            ex.AddModifier(new DamageCapIntValueModifier(cap, sender, name, sprite));
        }
    }

    public class DamageCapIntValueModifier(int damageCapMax, IUnit unit, string passiveName, Sprite passiveSprite) : IntValueModifier(98)
    {
        public override int Modify(int value)
        {
            if (value <= damageCapMax)
                return value;

            if (unit != null && (!string.IsNullOrEmpty(passiveName) || passiveSprite != null))
                CombatManager.Instance.AddUIAction(new ShowPassiveInformationUIAction(unit.ID, unit.IsUnitCharacter, passiveName, passiveSprite));

            return damageCapMax;
        }
    }
}
