using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Grimoire.Intent
{
    public static class StatusFieldIntents
    {
        public static readonly string Status_Fury, Status_Fury_Remove;
        public static readonly string Status_Survive, Status_Survive_Remove;
        public static readonly string Status_Weakened, Status_Weakened_Remove;
        public static readonly string Status_Insight, Status_Insight_Remove;

        public static readonly string Field_Mist, Field_Mist_Remove;

        static StatusFieldIntents()
        {
            GrimoireProfile.TryInitializeProfile();
            
            AddStatusEffectIntents(nameof(Status_Fury),         GrimoireStatusField.Fury,           out Status_Fury,            out Status_Fury_Remove);
            AddStatusEffectIntents(nameof(Status_Survive),      GrimoireStatusField.Survive,        out Status_Survive,         out Status_Survive_Remove);
            AddStatusEffectIntents(nameof(Status_Weakened),     GrimoireStatusField.Weakened,       out Status_Weakened,        out Status_Weakened_Remove);
            AddStatusEffectIntents(nameof(Status_Insight),      GrimoireStatusField.Insight,        out Status_Insight,         out Status_Insight_Remove);

            AddFieldEffectIntents(nameof(Field_Mist),   GrimoireStatusField.Mist,   out Field_Mist,     out Field_Mist_Remove);
        }

        internal static void Init()
        {
        }
    }
}
