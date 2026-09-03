using BepInEx;
using Grimoire.HealthColorOptions;
using Grimoire.Intent;
using Grimoire.Misc;
using Grimoire.Passive;
using Grimoire.StatusEffect;
using System;

namespace Grimoire
{
    [BepInDependency(BrutalAPI.BrutalAPI.GUID)]
    [BepInDependency(PentaclePlugin.MOD_GUID)]
    [BepInPlugin(MOD_GUID, MOD_NAME, MOD_VERSION)]
    public class GrimoirePlugin : BaseUnityPlugin
    {
        public const string MOD_GUID = "BrutalOrchestraModding.Grimoire";
        public const string MOD_NAME = "Grimoire";
        public const string MOD_VERSION = "0.0.2";
        public const string MOD_PREFIX = "Grimoire";

        internal static Harmony HarmonyInstance;

        internal void Awake()
        {
            HarmonyInstance = new Harmony(MOD_GUID);
            HarmonyInstance.PatchAll();

            HealthColorOptionsTools.BuildHealthColorHolderData();
            GrimoireProfile.TryInitializeProfile();

            PassiveStoredValues.Init();
            CustomStatusEffects.Init();
            CustomPassives.Init();

            StatusFieldIntents.Init();
            PassiveIntents.Init();
            MiscIntents.Init();
        }
    }
}
