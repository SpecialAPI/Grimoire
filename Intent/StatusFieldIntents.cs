using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Grimoire.Intent
{
    public static class StatusFieldIntents
    {
        public static readonly string Status_Berserk, Status_Berserk_Remove;
        public static readonly string Status_Fury, Status_Fury_Remove;
        public static readonly string Status_Survive, Status_Survive_Remove;
        public static readonly string Status_Weakened, Status_Weakened_Remove;
        public static readonly string Status_Insight, Status_Insight_Remove;

        static StatusFieldIntents()
        {
            GrimoireProfile.TryInitializeProfile();

            AddStatusEffectIntents(nameof(Status_Berserk),      GrimoireStatusField.Berserk,        out Status_Berserk,         out Status_Berserk_Remove);
            AddStatusEffectIntents(nameof(Status_Fury),         GrimoireStatusField.Fury,           out Status_Fury,            out Status_Fury_Remove);
            AddStatusEffectIntents(nameof(Status_Survive),      GrimoireStatusField.Survive,        out Status_Survive,         out Status_Survive_Remove);
            AddStatusEffectIntents(nameof(Status_Weakened),     GrimoireStatusField.Weakened,       out Status_Weakened,        out Status_Weakened_Remove);
            AddStatusEffectIntents(nameof(Status_Insight),      GrimoireStatusField.Insight,        out Status_Insight,         out Status_Insight_Remove);
        }

        internal static void Init()
        {
        }
    }
}
