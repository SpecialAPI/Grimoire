using BrutalAPI;
using Grimoire.Content.StatusEffect;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Grimoire.Content.Intent
{
    public static class StatusFieldIntents
    {
        public static readonly string Status_Berserk, Status_Berserk_Remove;
        public static readonly string Status_Fury, Status_Fury_Remove;
        public static readonly string Status_Survive, Status_Survive_Remove;
        public static readonly string Status_Weakened, Status_Weakened_Remove;
        public static readonly string Status_Insight, Status_Insight_Remove;
        public static readonly string Status_Funky, Status_Funky_Remove;
        public static readonly string Status_BadTrip, Status_BadTrip_Remove;

        static StatusFieldIntents()
        {
            GrimoireProfile.TryInitializeProfile();

            AddStatusEffectIntents(nameof(Status_Berserk),      CustomStatusEffects.Berserk,        out Status_Berserk,         out Status_Berserk_Remove);
            AddStatusEffectIntents(nameof(Status_Fury),         CustomStatusEffects.Fury,           out Status_Fury,            out Status_Fury_Remove);
            AddStatusEffectIntents(nameof(Status_Survive),      CustomStatusEffects.Survive,        out Status_Survive,         out Status_Survive_Remove);
            AddStatusEffectIntents(nameof(Status_Weakened),     CustomStatusEffects.Weakened,       out Status_Weakened,        out Status_Weakened_Remove);
            AddStatusEffectIntents(nameof(Status_Insight),      CustomStatusEffects.Insight,        out Status_Insight,         out Status_Insight_Remove);
            AddStatusEffectIntents(nameof(Status_Funky),        CustomStatusEffects.Funky,          out Status_Funky,           out Status_Funky_Remove);
            AddStatusEffectIntents(nameof(Status_BadTrip),      CustomStatusEffects.BadTrip,        out Status_BadTrip,         out Status_BadTrip_Remove);
        }

        internal static void Init()
        {
        }
    }
}
