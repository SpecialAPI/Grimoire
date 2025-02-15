using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.TriggerEffects
{
    public class InvincibleDamageModifierSetterTriggerEffect : TriggerEffect
    {
        public int invincibility;
        public UnitStoreData_BasicSO invincibilityStoredValue;

        public override void DoEffect(IUnit sender, object args, TriggeredEffect triggerInfo, object activator = null)
        {
            if (args is not DamageReceivedValueChangeException ex)
                return;

            var inv = invincibility;

            if (invincibilityStoredValue != null)
                inv += sender.SimpleGetStoredValue(invincibilityStoredValue._UnitStoreDataID);

            inv = Mathf.Max(inv, 0);

            if (!activator.TryGetActivatorNameAndSprite(out var name, out var sprite))
            {
                name = null;
                sprite = null;
            }

            ex.AddModifier(new InvincibleIntValueModifier(inv, sender, name, sprite));
        }
    }

    public class InvincibleIntValueModifier(int invincibility, IUnit unit, string passiveName, Sprite passiveSprite) : IntValueModifier(100)
    {
        public override int Modify(int value)
        {
            if (value > invincibility)
                return value;

            if (unit != null && (!string.IsNullOrEmpty(passiveName) || passiveSprite != null))
                CombatManager.Instance.AddUIAction(new ShowPassiveInformationUIAction(unit.ID, unit.IsUnitCharacter, passiveName, passiveSprite));

            return 0;
        }
    }
}
