using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.Misc
{
    public static class CustomDamageTypes
    {
        public static readonly string DisappearingDamage = $"{MOD_PREFIX}_DisapearingDamage";

        internal static void Init()
        {
            LoadedDBsHandler.CombatDB.AddNewSound(DisappearingDamage, "event:/Combat/StatusEffects/SE_Divine_Trg");
            LoadedDBsHandler.CombatDB.AddNewTextColor(DisappearingDamage, new(Color.yellow, Color.grey, Color.grey, Color.yellow));
        }
    }
}
