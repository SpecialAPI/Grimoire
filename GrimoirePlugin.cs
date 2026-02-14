using BepInEx;
using Grimoire.Content.FieldEffects;
using Grimoire.Content.Intent;
using Grimoire.Content.Misc;
using Grimoire.Content.Passive;
using Grimoire.Content.StatusEffect;
using Grimoire.HealthColorOptions;
using System;

namespace Grimoire
{
    [BepInPlugin(MOD_GUID, MOD_NAME, MOD_VERSION)]
    public class GrimoirePlugin : BaseUnityPlugin
    {
        public const string MOD_GUID = "BrutalOrchestraModding.Grimoire";
        public const string MOD_NAME = "Grimoire";
        public const string MOD_VERSION = "0.0.1";
        public const string MOD_PREFIX = "Grimoire";

        internal static Harmony HarmonyInstance;

        internal void Awake()
        {
            HarmonyInstance = new Harmony(MOD_GUID);
            HarmonyInstance.PatchAll();

            HealthColorOptionsTools.BuildUnitExtData();
            GrimoireProfile.TryInitializeProfile();

            PassiveStoredValues.Init();
            CustomDamageTypes.Init();
            CustomStatusEffects.Init();
            CustomFieldEffects.Init();
            CustomPassives.Init();

            StatusFieldIntents.Init();
            PassiveIntents.Init();
            MiscIntents.Init();

            GlossaryAdditions.Init();
        }
    }
}
