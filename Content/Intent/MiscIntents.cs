using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.Intent
{
    public static class MiscIntents
    {
        public static readonly string Threaten_1_2;
        public static readonly string Threaten_3_6;
        public static readonly string Threaten_7_10;
        public static readonly string Threaten_11_15;
        public static readonly string Threaten_16_20;
        public static readonly string Threaten_21;
        public static readonly string Threaten_Death;

        static MiscIntents()
        {
            GrimoireProfile.TryInitializeProfile();

            Threaten_1_2    = AddDamageIntent(nameof(Threaten_1_2),   "Intent_Threaten_1_2_Left",     "Intent_Threaten_1_2_Right");
            Threaten_3_6    = AddDamageIntent(nameof(Threaten_3_6),   "Intent_Threaten_3_6_Left",     "Intent_Threaten_3_6_Right");
            Threaten_7_10   = AddDamageIntent(nameof(Threaten_7_10),  "Intent_Threaten_7_10_Left",    "Intent_Threaten_7_10_Right");
            Threaten_11_15  = AddDamageIntent(nameof(Threaten_11_15), "Intent_Threaten_11_15_Left",   "Intent_Threaten_11_15_Right");
            Threaten_16_20  = AddDamageIntent(nameof(Threaten_16_20), "Intent_Threaten_16_20_Left",   "Intent_Threaten_16_20_Right");
            Threaten_21     = AddDamageIntent(nameof(Threaten_21),    "Intent_Threaten_21_Left",      "Intent_Threaten_21_Right");
            Threaten_Death  = AddDamageIntent(nameof(Threaten_Death), "Intent_Threaten_Death_Left",   "Intent_Threaten_Death_Right");
        }

        public static string IntentForThreaten(int amount)
        {
            return amount switch
            {
                <= 2 => Threaten_1_2,
                <= 6 => Threaten_3_6,
                <= 10 => Threaten_7_10,
                <= 15 => Threaten_11_15,
                <= 20 => Threaten_16_20,

                _ => Threaten_21
            };
        }

        internal static void Init()
        {
        }
    }
}
