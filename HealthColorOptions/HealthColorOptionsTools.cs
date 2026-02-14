using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.HealthColorOptions
{
    public static class HealthColorOptionsTools
    {
        private static UnitStoreData_BasicSO StoreData_HealthColorHolder;

        internal static void BuildHealthColorHolderData()
        {
            if (StoreData_HealthColorHolder != null)
                return;

            var dat = StoreData_HealthColorHolder = CreateScriptable<UnitStoreData_BasicSO>();

            dat.name = $"{MOD_PREFIX}_HealthColorHolder_USD";
            dat._UnitStoreDataID = $"{MOD_PREFIX}_HealthColorHolder";

            UnitStoreData.AddCustom_Any_UnitStoreDataToPool(dat, dat._UnitStoreDataID);
        }

        internal static HealthColorOptionsHolder HealthOptionHolder(this IUnit unit)
        {
            if (unit == null || unit.Equals(null))
                return null;

            if (StoreData_HealthColorHolder == null)
                BuildHealthColorHolderData();

            unit.TryGetStoredData(StoreData_HealthColorHolder.name, out var hold, true);

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
