using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Misc
{
    public class UpdateEnemyHiddenTurnsCombatAction(IUnit unit) : CombatAction
    {
        public override IEnumerator Execute(CombatStats stats)
        {
            UpdateForUnit(unit);
            yield break;
        }

        public static void UpdateForUnit(IUnit unit)
        {
            if (unit is ITurn turn)
                CombatManager.Instance.AddUIAction(new UpdateEnemyHiddenTurnsUIAction(unit, CombatManager.Instance._stats.timeline.IsConfused || !turn.CanTurnShowInTimeline));
        }
    }
}
