using System;
using System.Collections.Generic;
using System.Text;
using Grimoire.HealthColorOptions;

namespace Grimoire.Content.Effect
{
    public class AddHealthColorsNotInOptionsEffect : EffectSO
    {
        public List<ManaColorSO> healthColors;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (healthColors == null || healthColors.Count <= 0)
                return false;

            foreach (var t in targets)
            {
                if (t == null || !t.HasUnit)
                    continue;

                var u = t.Unit;

                foreach (var c in healthColors)
                {
                    if (u.GetHealthColorOptions().Contains(c))
                        continue;

                    u.AddHealthColor(c);
                    exitAmount++;
                }
            }

            return exitAmount > 0;
        }
    }
}
