using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.Misc
{
    public static class PassiveStoredValues
    {
        public static UnitStoreData_BasicSO ResilientAddition;
        public static UnitStoreData_BasicSO ResilientRemainingDamage;
        public static UnitStoreData_BasicSO WartsAddition;
        public static UnitStoreData_BasicSO SturdyAddition;
        public static UnitStoreData_BasicSO InvincibleAddition;
        public static UnitStoreData_BasicSO VolatileAddition;

        static PassiveStoredValues()
        {
            GrimoireProfile.TryInitializeProfile();

            ResilientAddition = NewStoredValue<AdvancedStoredValueIntInfo>("Resilient_USD", "Resilient").SetFormat($"Resilient {StoredValueInsert_PlusMinusPlusZero}").SetColor(StoredValueColor_Positive);
            ResilientRemainingDamage = NewStoredValue<AdvancedStoredValueIntInfo>("ResilientRemainingDamage_USD", "ResilientRemainingDamage").SetFormat("Remaining damage: {0}").SetCustomDisplayCondition(x => x.m_MainData >= 0).SetDynamicColor(x => x.m_MainData > 0 ? StoredValueColor_Positive : StoredValueColor_Negative);
            WartsAddition = NewStoredValue<AdvancedStoredValueIntInfo>("Warts_USD", "Warts").SetFormat($"Warts {StoredValueInsert_PlusMinusPlusZero}").SetColor(Color.yellow);
            SturdyAddition = NewStoredValue<AdvancedStoredValueIntInfo>("Sturdy_USD", "Sturdy").SetFormat($"Sturdy {StoredValueInsert_PlusMinusPlusZero}").SetColor(StoredValueColor_Positive);
            InvincibleAddition = NewStoredValue<AdvancedStoredValueIntInfo>("Invincible_USD", "Invincible").SetFormat($"Invincible {StoredValueInsert_PlusMinusPlusZero}").SetColor(Color.yellow);
            VolatileAddition = NewStoredValue<AdvancedStoredValueIntInfo>("Volatile_USD", "Volatile").SetFormat($"Volatile {StoredValueInsert_PlusMinusPlusZero}").SetColor(StoredValueColor_Negative);
        }

        internal static void Init()
        {
        }
    }
}
