using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.Effect
{
    public class CasterStoredValueChangeByPreviousExitValueEffect : EffectSO
    {
        public UnitStoreData_BasicSO storedValue;
        public bool increase = true;
        public int minimumValue;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            var change = entryVariable * PreviousExitValue;
            exitAmount = caster.SimpleGetStoredValue(storedValue._UnitStoreDataID);

            if (!increase)
                change = -change;

            exitAmount = Mathf.Max(minimumValue, exitAmount + change);
            caster.SimpleSetStoredValue(storedValue._UnitStoreDataID, exitAmount);

            return true;
        }
    }
}
