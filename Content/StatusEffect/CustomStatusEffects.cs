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
        }

        internal static void Init()
        {
        }
    }
}
