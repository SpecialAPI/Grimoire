using Grimoire.Content.FieldEffects.EffectTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.FieldEffects
{
    public static class CustomFieldEffects
    {
        public static readonly FieldEffect_SO Thunderstorm;
        public static readonly FieldEffect_SO ShadowHands;

        static CustomFieldEffects()
        {
            GrimoireProfile.TryInitializeProfile();

            Thunderstorm = NewFieldEffect<ThunderstormFieldEffect>("Thunderstorm_FE", "Thunderstorm_ID")
                .SetBasicInformation("Thunderstorm", "Damage dealt and received will be increased by 1 for each Thunderstorm.\nDying in the Thunderstorm will transfer the Thunderstorm to whatever killed them.\nIf Thunderstorm is created in a new position, all of it will move to that position.", "Thunderstorm")
                .SetSounds("event:/Combat/StatusEffects/SE_Fire_Apl")
                .SetFieldPrefabs("ThunderstormCharacter", "ThunderstormEnemy")
                .AddToDatabase(true);

            ShadowHands = NewFieldEffect<ShadowHandsFieldEffect>("ShadowHands_FE", "ShadowHands_ID")
                .SetBasicInformation("Shadow Hands", "Upon taking any damage or performing an ability in Shadow Hands, move to the left or right.\n1 Shadow Hands is lost upon taking damage.", "ShadowHands")
                .SetSounds("event:/Combat/StatusEffects/SE_Constricted_Apl")
                .SetFieldPrefabs("ShadowHandsCharacter", "ShadowHandsEnemy")
                .AddToDatabase(true);
        }

        internal static void Init()
        {
        }
    }
}
