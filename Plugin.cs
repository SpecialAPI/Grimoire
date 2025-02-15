global using static Grimoire.Plugin;

using BepInEx;
using Grimoire.Content.FieldEffects;
using Grimoire.Content.Intent;
using Grimoire.Content.Misc;
using Grimoire.Content.Passive;
using Grimoire.Content.StatusEffect;
using System;

namespace Grimoire
{
    [BepInPlugin(MODGUID, MODNAME, MODVERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public const string MODGUID = "BrutalOrchestraModding.Grimoire";
        public const string MODNAME = "Grimoire";
        public const string MODVERSION = "1.0.0";
        public const string MODPREFIX = "Grimoire";

        internal void Awake()
        {
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
