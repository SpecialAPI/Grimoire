using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.TriggerEffects
{
    public class MultiplierWithPerformEffectDamageModifierSetterTriggerEffect : TriggerEffect
    {
        public int multiplier;
        public List<EffectInfo> effects;

        public override void DoEffect(IUnit sender, object args, TriggeredEffect triggerInfo, object activator = null)
        {
            if (args is not DamageReceivedValueChangeException ex)
                return;

            if(!activator.TryGetActivatorNameAndSprite(out var name, out var sprite))
            {
                name = string.Empty;
                sprite = null;
            }

            ex.AddModifier(new MultiplierWithPerformEffectIntValueModifier(multiplier, effects, sender, name, sprite));
        }
    }

    public class MultiplierWithPerformEffectIntValueModifier(int multiplier, List<EffectInfo> effects, IUnit unit, string passiveName, Sprite passiveSprite) : IntValueModifier(73)
    {
        public override int Modify(int value)
        {
            if (value <= 0)
                return value;

            if (!string.IsNullOrEmpty(passiveName) || passiveSprite != null)
                CombatManager.Instance.AddUIAction(new ShowPassiveInformationUIAction(unit.ID, unit.IsUnitCharacter, passiveName, passiveSprite));

            if (effects != null && effects.Count > 0)
                CombatManager.Instance.AddSubAction(new EffectAction([.. effects], unit));

            return value * multiplier;
        }
    }
}
