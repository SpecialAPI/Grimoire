using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.Effect
{
    public class ChangeToRandomHealthColorIncludingCurrentEffect : EffectSO
    {
        public List<ManaColorSO> healthColors;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (healthColors == null || healthColors.Count <= 0)
                return false;

            foreach(var t in targets)
            {
                if (t == null || !t.HasUnit)
                    continue;

                var col = healthColors[Random.Range(0, healthColors.Count)];
                t.Unit.ChangeHealthColor(col);

                exitAmount++;
            }

            return exitAmount > 0;
        }
    }
}
