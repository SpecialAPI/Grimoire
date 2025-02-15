using BrutalAPI;
using Grimoire.Content.StatusEffect.EffectTypes;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Grimoire.Content.StatusEffect
{
    public static class CustomStatusEffects
    {
        public static StatusEffect_SO Berserk;
        public static StatusEffect_SO Fury;
        public static StatusEffect_SO Survive;
        public static StatusEffect_SO Weakened;
        public static StatusEffect_SO Insight;
        public static StatusEffect_SO Disappearing;
        public static StatusEffect_SO Salted;
        public static StatusEffect_SO Funky;
        public static StatusEffect_SO BadTrip;

        static CustomStatusEffects()
        {
            GrimoireProfile.TryInitializeProfile();

            LoadedDBsHandler.GlossaryDB.AddNewStatusID(StatusField.Gutted.EffectInfo);
            LoadedDBsHandler.GlossaryDB.AddNewStatusID(StatusField.Stunned.EffectInfo);

            Berserk =
                NewStatusEffect<BerserkStatusEffect>("Berserk_SE", "Berserk_ID")
                .SetBasicInformation("Berserk", "Deal double damage.\n1 point of Berserk is lost at the end of each turn.", "Berserk")
                .SetSounds("event:/Combat/StatusEffects/SE_Focus_In")
                .AddToDatabase(true);

            Fury =
                NewStatusEffect<FuryStatusEffect>("Fury_SE", "Fury_ID")
                .SetBasicInformation("Fury", "When performing an ability, repeat it for each point of Fury and remove all Fury.\n1 point of Fury is lost at the end of each turn.", "Fury")
                .SetSounds("event:/Combat/StatusEffects/SE_Focus_In")
                .AddToDatabase(true);

            Survive =
                NewStatusEffect<SurviveStatusEffect>("Survive_SE", "Survive_ID")
                .SetBasicInformation("Survive", "Survive 1 instance of fatal damage for each point of Survive.", "Survive")
                .SetSounds("event:/Combat/StatusEffects/SE_Divine_Apl")
                .AddToDatabase(true);

            Weakened =
                NewStatusEffect<WeakenedStatusEffect>("Weakened_SE", "Weakened_ID")
                .SetBasicInformation("Weakened", "Weakened party members are 1 level lower than they would be otherwise for each point of Weakened.\nDamage dealt by Weakened enemies is divided by 1 plus 0.25 multiplied by the amount of Weakened..\n1 point of Weakened is lost at the end of each turn.", "Weakened")
                .SetSounds("event:/Combat/StatusEffects/SE_Frail_Apl")
                .AddToDatabase(true);

            Insight =
                NewStatusEffect<InsightStatusEffect>("Insight_SE", "Insight_ID")
                .SetBasicInformation("Insight", "The abilities performed by this enemy on the next turns are predetermined.\nHas no effect on party members.", "Insight")
                .SetSounds("event:/Combat/StatusEffects/SE_Linked_Apl")
                .AddToDatabase(true);

            Disappearing =
                NewStatusEffect<DisappearingStatusEffect>("Disappearing_SE", "Disappearing_ID")
                .SetBasicInformation("Disappearing", "Receive half of Disappearing as damage on turn end.\nHalf of Disappearing is removed at the end of each turn.\nDisappearing damage will be prevented if this party member/enemy is Constricted.", "Disappearing")
                .SetSounds("event:/Combat/StatusEffects/SE_Linked_Apl")
                .AddToDatabase(true);

            Salted =
                NewStatusEffect<SaltedStatusEffect>("Salted_SE", "Salted_ID")
                .SetBasicInformation("Salted", "Increase all other status effects on this party member/enemy by 2 on turn end.\n1 Salted is lost at the end of each turn.", "Salted")
                .SetSounds("event:/Combat/StatusEffects/SE_Frail_Apl")
                .AddToDatabase(true);

            Funky =
                NewStatusEffect<FunkyStatusEffect>("Funky_SE", "Funky_ID")
                .SetBasicInformation("Funky", "Receive additional healing equal to the amount of Funky.\n1 Funky is removed at the end of each turn.", "Funky")
                .SetSounds("event:/Combat/StatusEffects/SE_Linked_Apl")
                .AddToDatabase(true);

            BadTrip =
                NewStatusEffect<BadTripStatusEffect>("BadTrip_SE", "BadTrip_ID")
                .SetBasicInformation("Bad Trip", "Inflicted negative status effects are increased by the amount of Bad Trip.\n1 Bad Trip is removed at the end of each turn.", "BadTrip")
                .SetSounds("event:/Combat/StatusEffects/SE_Cursed_Apl")
                .AddToDatabase(true);
        }

        internal static void Init()
        {
        }
    }
}
