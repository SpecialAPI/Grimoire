using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.HealthColorOptions
{
    public static class HealthColorOptionsTools
    {
        private static UnitStoreData_BasicSO StoreData_UnitExt;

        internal static void BuildUnitExtData()
        {
            if (StoreData_UnitExt != null)
                return;

            var dat = StoreData_UnitExt = CreateScriptable<UnitStoreData_BasicSO>();

            dat.name = $"{MOD_PREFIX}_HealthColorHolder_USD";
            dat._UnitStoreDataID = $"{MOD_PREFIX}_HealthColorHolder";

            LoadedDBsHandler.MiscDB.AddNewUnitStoreData(dat.name, dat);
        }

        internal static HealthColorOptionsHolder HealthOptionHolder(this IUnit unit)
        {
            if (unit == null || unit.Equals(null))
                return null;

            if (StoreData_UnitExt == null)
                BuildUnitExtData();

            unit.TryGetStoredData(StoreData_UnitExt.name, out var hold, true);

            if (hold == null)
                return null;

            return (hold.m_ObjectData ??= new HealthColorOptionsHolder(unit)) as HealthColorOptionsHolder;
        }

        public static IReadOnlyList<ManaColorSO> GetHealthColorOptions(this IUnit unit)
        {
            return unit.HealthOptionHolder().healthColors;
        }

        public static int GetCurrentHealthColorIndex(this IUnit unit)
        {
            return unit.HealthOptionHolder().currentHealthColor;
        }

        public static void AddHealthColor(this IUnit unit, ManaColorSO color)
        {
            unit.HealthOptionHolder().healthColors.Add(color);
        }
    }
}
