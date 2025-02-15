using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using Grimoire.Content.Passive;

namespace Grimoire.Content.Intent
{
    public static class PassiveIntents
    {
        #region Basegame Passive Intents
        public static readonly string PA_Abomination, PA_Abomination_Remove;
        public static readonly string PA_Absorb, PA_Absorb_Remove;
        public static readonly string PA_Anchored, PA_Anchored_Remove;
        public static readonly string PA_BoneSpurs, PA_BoneSpurs_Remove;
        public static readonly string PA_BronzosBlessing, PA_BronzosBlessing_Remove;
        public static readonly string PA_Cashout, PA_Cashout_Remove;
        public static readonly string PA_Catalyst, PA_Catalyst_Remove;
        public static readonly string PA_Confusion, PA_Confusion_Remove;
        public static readonly string PA_Constricting, PA_Constricting_Remove;
        public static readonly string PA_Construct, PA_Construct_Remove;
        public static readonly string PA_Delicate, PA_Delicate_Remove;
        public static readonly string PA_Dying, PA_Dying_Remove;
        public static readonly string PA_Enfeebled, PA_Enfeebled_Remove;
        public static readonly string PA_EssenceBlue, PA_EssenceBlue_Remove;
        public static readonly string PA_EssencePurple, PA_EssencePurple_Remove;
        public static readonly string PA_EssenceRed, PA_EssenceRed_Remove;
        public static readonly string PA_EssenceYellow, PA_EssenceYellow_Remove;
        public static readonly string PA_EssenceUntethered, PA_EssenceUntethered_Remove;
        public static readonly string PA_FinancialHyperinflation, PA_FinancialHyperinflation_Remove;
        public static readonly string PA_Fleeting, PA_Fleeting_Remove;
        public static readonly string PA_Focus, PA_Focus_Remove;
        public static readonly string PA_ForbiddenFruit, PA_ForbiddenFruit_Remove;
        public static readonly string PA_Forgetful, PA_Forgetful_Remove;
        public static readonly string PA_Formless, PA_Formless_Remove;
        public static readonly string PA_Immortal, PA_Immortal_Remove;
        public static readonly string PA_Inanimate, PA_Inanimate_Remove;
        public static readonly string PA_Infantile, PA_Infantile_Remove;
        public static readonly string PA_Inferno, PA_Inferno_Remove;
        public static readonly string PA_Infestation, PA_Infestation_Remove;
        public static readonly string PA_Leaky, PA_Leaky_Remove;
        public static readonly string PA_Masochism, PA_Masochism_Remove;
        public static readonly string PA_MultiAttack, PA_MultiAttack_Remove;
        public static readonly string PA_Obscure, PA_Obscure_Remove;
        public static readonly string PA_Overexert, PA_Overexert_Remove;
        public static readonly string PA_PanicAttack, PA_PanicAttack_Remove;
        public static readonly string PA_Mutualism, PA_Mutualism_Remove;
        public static readonly string PA_Parasitism, PA_Parasitism_Remove;
        public static readonly string PA_Pure, PA_Pure_Remove;
        public static readonly string PA_Reborn, PA_Reborn_Remove;
        public static readonly string PA_Skittish, PA_Skittish_Remove;
        public static readonly string PA_Slippery, PA_Slippery_Remove;
        public static readonly string PA_Transfusion, PA_Transfusion_Remove;
        public static readonly string PA_TwoFaced, PA_TwoFaced_Remove;
        public static readonly string PA_Unstable, PA_Unstable_Remove;
        public static readonly string PA_Withering, PA_Withering_Remove;
        public static readonly string PA_Parental, PA_Parental_Remove;
        public static readonly string PA_BonusAttack, PA_BonusAttack_Remove;
        public static readonly string PA_Decay, PA_Decay_Remove;
        #endregion

        #region Modded Passive Intents
        public static readonly string PA_CoreRed, PA_CoreRed_Remove;
        public static readonly string PA_CoreBlue, PA_CoreBlue_Remove;
        public static readonly string PA_CoreYellow, PA_CoreYellow_Remove;
        public static readonly string PA_CorePurple, PA_CorePurple_Remove;
        public static readonly string PA_CoreGrey, PA_CoreGrey_Remove;
        public static readonly string PA_CoreUntethered, PA_CoreUntethered_Remove;
        public static readonly string PA_Immaterial, PA_Immaterial_Remove;
        public static readonly string PA_Sturdy, PA_Sturdy_Remove;
        public static readonly string PA_Resilient, PA_Resilient_Remove;
        public static readonly string PA_Volatile, PA_Volatile_Remove;
        public static readonly string PA_Impunity, PA_Impunity_Remove;
        public static readonly string PA_Sacrilege, PA_Sacrilege_Remove;
        public static readonly string PA_Sweeping, PA_Sweeping_Remove;
        public static readonly string PA_Thorny, PA_Thorny_Remove;
        public static readonly string PA_Billiard, PA_Billiard_Remove;
        public static readonly string PA_Connoisseur, PA_Connoisseur_Remove;
        public static readonly string PA_Chaos, PA_Chaos_Remove;
        public static readonly string PA_Grinding, PA_Grinding_Remove;
        public static readonly string PA_Disruption, PA_Disruption_Remove;
        public static readonly string PA_Interpolated, PA_Interpolated_Remove;
        public static readonly string PA_Humorous, PA_Humorous_Remove;
        public static readonly string PA_Mirage, PA_Mirage_Remove;
        public static readonly string PA_Warts, PA_Warts_Remove;
        public static readonly string PA_AltAttacks, PA_AltAttacks_Remove;
        public static readonly string PA_Invincible, PA_Invincible_Remove;
        public static readonly string PA_Disguised, PA_Disguised_Remove;
        #endregion

        static PassiveIntents()
        {
            GrimoireProfile.TryInitializeProfile();

            #region Basegame Passive Intent Setup
            AddPassiveIntents(nameof(PA_Abomination),               Passives.Abomination1,                  out PA_Abomination,             out PA_Abomination_Remove);
            AddPassiveIntents(nameof(PA_Absorb),                    Passives.Absorb,                        out PA_Absorb,                  out PA_Absorb_Remove);
            AddPassiveIntents(nameof(PA_Anchored),                  Passives.Anchored,                      out PA_Anchored,                out PA_Anchored_Remove);
            AddPassiveIntents(nameof(PA_BoneSpurs),                 Passives.BoneSpurs1,                    out PA_BoneSpurs,               out PA_BoneSpurs_Remove);
            AddPassiveIntents(nameof(PA_BronzosBlessing),           Passives.BronzosBlessing,               out PA_BronzosBlessing,         out PA_BronzosBlessing_Remove);
            AddPassiveIntents(nameof(PA_Cashout),                   Passives.Cashout,                       out PA_Cashout,                 out PA_Cashout_Remove);
            AddPassiveIntents(nameof(PA_Catalyst),                  Passives.Catalyst,                      out PA_Catalyst,                out PA_Catalyst_Remove);
            AddPassiveIntents(nameof(PA_Confusion),                 Passives.Confusion,                     out PA_Confusion,               out PA_Confusion_Remove);
            AddPassiveIntents(nameof(PA_Constricting),              Passives.Constricting,                  out PA_Constricting,            out PA_Constricting_Remove);
            AddPassiveIntents(nameof(PA_Construct),                 Passives.Construct,                     out PA_Construct,               out PA_Construct_Remove);
            AddPassiveIntents(nameof(PA_Delicate),                  Passives.Delicate,                      out PA_Delicate,                out PA_Delicate_Remove);
            AddPassiveIntents(nameof(PA_Dying),                     Passives.Dying,                         out PA_Dying,                   out PA_Dying_Remove);
            AddPassiveIntents(nameof(PA_Enfeebled),                 Passives.Enfeebled,                     out PA_Enfeebled,               out PA_Enfeebled_Remove);
            AddPassiveIntents(nameof(PA_EssenceBlue),               Passives.EssenceBlue,                   out PA_EssenceBlue,             out PA_EssenceBlue_Remove);
            AddPassiveIntents(nameof(PA_EssencePurple),             Passives.EssencePurple,                 out PA_EssencePurple,           out PA_EssencePurple_Remove);
            AddPassiveIntents(nameof(PA_EssenceRed),                Passives.EssenceRed,                    out PA_EssenceRed,              out PA_EssenceRed_Remove);
            AddPassiveIntents(nameof(PA_EssenceYellow),             Passives.EssenceYellow,                 out PA_EssenceYellow,           out PA_EssenceYellow_Remove);
            AddPassiveIntents(nameof(PA_EssenceUntethered),         Passives.EssenceUntethered,             out PA_EssenceUntethered,       out PA_EssenceUntethered_Remove);
            AddPassiveIntents(nameof(PA_FinancialHyperinflation),   Passives.FinancialHyperinflation,       out PA_FinancialHyperinflation, out PA_FinancialHyperinflation_Remove);
            AddPassiveIntents(nameof(PA_Fleeting),                  Passives.Fleeting1,                     out PA_Fleeting,                out PA_Fleeting_Remove);
            AddPassiveIntents(nameof(PA_Focus),                     Passives.Focus,                         out PA_Focus,                   out PA_Focus_Remove);
            AddPassiveIntents(nameof(PA_ForbiddenFruit),            Passives.ForbiddenFruitInHerImage,      out PA_ForbiddenFruit,          out PA_ForbiddenFruit_Remove);
            AddPassiveIntents(nameof(PA_Forgetful),                 Passives.Forgetful,                     out PA_Forgetful,               out PA_Forgetful_Remove);
            AddPassiveIntents(nameof(PA_Formless),                  Passives.Formless,                      out PA_Formless,                out PA_Formless_Remove);
            AddPassiveIntents(nameof(PA_Immortal),                  Passives.Immortal,                      out PA_Immortal,                out PA_Immortal_Remove);
            AddPassiveIntents(nameof(PA_Inanimate),                 Passives.Inanimate,                     out PA_Inanimate,               out PA_Inanimate_Remove);
            AddPassiveIntents(nameof(PA_Infantile),                 Passives.Infantile,                     out PA_Infantile,               out PA_Infantile_Remove);
            AddPassiveIntents(nameof(PA_Inferno),                   Passives.Inferno,                       out PA_Inferno,                 out PA_Inferno_Remove);
            AddPassiveIntents(nameof(PA_Infestation),               Passives.Infestation0,                  out PA_Infestation,             out PA_Infestation_Remove);
            AddPassiveIntents(nameof(PA_Leaky),                     Passives.Leaky1,                        out PA_Leaky,                   out PA_Leaky_Remove);
            AddPassiveIntents(nameof(PA_Masochism),                 Passives.Masochism1,                    out PA_Masochism,               out PA_Masochism_Remove);
            AddPassiveIntents(nameof(PA_MultiAttack),               Passives.MultiAttack2,                  out PA_MultiAttack,             out PA_MultiAttack_Remove);
            AddPassiveIntents(nameof(PA_Obscure),                   Passives.Obscure,                       out PA_Obscure,                 out PA_Obscure_Remove);
            AddPassiveIntents(nameof(PA_Overexert),                 Passives.Overexert1,                    out PA_Overexert,               out PA_Overexert_Remove);
            AddPassiveIntents(nameof(PA_PanicAttack),               Passives.PanicAttack,                   out PA_PanicAttack,             out PA_PanicAttack_Remove);
            AddPassiveIntents(nameof(PA_Pure),                      Passives.Pure,                          out PA_Pure,                    out PA_Pure_Remove);
            AddPassiveIntents(nameof(PA_Reborn),                    Passives.RebornToInHerImage,            out PA_Reborn,                  out PA_Reborn_Remove);
            AddPassiveIntents(nameof(PA_Skittish),                  Passives.Skittish,                      out PA_Skittish,                out PA_Skittish_Remove);
            AddPassiveIntents(nameof(PA_Slippery),                  Passives.Slippery,                      out PA_Slippery,                out PA_Slippery_Remove);
            AddPassiveIntents(nameof(PA_Transfusion),               Passives.Transfusion,                   out PA_Transfusion,             out PA_Transfusion_Remove);
            AddPassiveIntents(nameof(PA_TwoFaced),                  Passives.TwoFaced,                      out PA_TwoFaced,                out PA_TwoFaced_Remove);
            AddPassiveIntents(nameof(PA_Unstable),                  Passives.Unstable,                      out PA_Unstable,                out PA_Unstable_Remove);
            AddPassiveIntents(nameof(PA_Withering),                 Passives.Withering,                     out PA_Withering,               out PA_Withering_Remove);
            AddPassiveIntents(nameof(PA_Parental),                  Passives.Example_Parental_Vengeance,    out PA_Parental,                out PA_Parental_Remove);
            AddPassiveIntents(nameof(PA_BonusAttack),               Passives.Example_BonusAttack_Mangle,    out PA_BonusAttack,             out PA_BonusAttack_Remove);
            AddPassiveIntents(nameof(PA_Decay),                     Passives.Example_Decay_MudLung,         out PA_Decay,                   out PA_Decay_Remove);

            PA_Mutualism            = IntentType_GameIDs.PA_Mutualism.ToString();
            PA_Mutualism_Remove     = AddIntent(nameof(PA_Mutualism_Remove), Passives.ParasiteMutualism.passiveIcon, IntentColor_StatusRemove);
            PA_Parasitism           = IntentType_GameIDs.PA_Parasitism.ToString();
            PA_Parasitism_Remove    = AddIntent(nameof(PA_Parasitism_Remove), Passives.ParasiteParasitism.passiveIcon, IntentColor_StatusRemove);
            #endregion

            #region Modded Passive Setup
            AddPassiveIntents(nameof(PA_CoreRed),               CustomPassives.CoreRed,                 out PA_CoreRed,         out PA_CoreRed_Remove);
            AddPassiveIntents(nameof(PA_CoreBlue),              CustomPassives.CoreBlue,                out PA_CoreBlue,        out PA_CoreBlue_Remove);
            AddPassiveIntents(nameof(PA_CoreYellow),            CustomPassives.CoreYellow,              out PA_CoreYellow,      out PA_CoreYellow_Remove);
            AddPassiveIntents(nameof(PA_CorePurple),            CustomPassives.CorePurple,              out PA_CorePurple,      out PA_CorePurple_Remove);
            AddPassiveIntents(nameof(PA_CoreGrey),              CustomPassives.CoreGrey,                out PA_CoreGrey,        out PA_CoreGrey_Remove);
            AddPassiveIntents(nameof(PA_CoreUntethered),        CustomPassives.CoreUntethered,          out PA_CoreUntethered,  out PA_CoreUntethered_Remove);
            AddPassiveIntents(nameof(PA_Immaterial),            CustomPassives.Immaterial,              out PA_Immaterial,      out PA_Immaterial_Remove);
            AddGenericAddRemoveIntents(nameof(PA_Sturdy),       CustomPassives.SturdySprite,            out PA_Sturdy,          out PA_Sturdy_Remove);
            AddGenericAddRemoveIntents(nameof(PA_Resilient),    CustomPassives.ResilientSprite,         out PA_Resilient,       out PA_Resilient_Remove);
            AddGenericAddRemoveIntents(nameof(PA_Volatile),     CustomPassives.VolatileSprite,          out PA_Volatile,        out PA_Volatile_Remove);
            AddPassiveIntents(nameof(PA_Impunity),              CustomPassives.Impunity,                out PA_Impunity,        out PA_Impunity_Remove);
            AddPassiveIntents(nameof(PA_Sacrilege),             CustomPassives.Sacrilege,               out PA_Sacrilege,       out PA_Sacrilege_Remove);
            AddPassiveIntents(nameof(PA_Sweeping),              CustomPassives.Sweeping,                out PA_Sweeping,        out PA_Sweeping_Remove);
            AddPassiveIntents(nameof(PA_Thorny),                CustomPassives.Thorny,                  out PA_Thorny,          out PA_Thorny_Remove);
            AddPassiveIntents(nameof(PA_Billiard),              CustomPassives.Billiard,                out PA_Billiard,        out PA_Billiard_Remove);
            AddPassiveIntents(nameof(PA_Connoisseur),           CustomPassives.Connoisseur,             out PA_Connoisseur,     out PA_Connoisseur_Remove);
            AddPassiveIntents(nameof(PA_Chaos),                 CustomPassives.Chaos,                   out PA_Chaos,           out PA_Chaos_Remove);
            AddPassiveIntents(nameof(PA_Grinding),              CustomPassives.Grinding,                out PA_Grinding,        out PA_Grinding_Remove);
            AddPassiveIntents(nameof(PA_Disruption),            CustomPassives.Disruption,              out PA_Disruption,      out PA_Disruption_Remove);
            AddPassiveIntents(nameof(PA_Interpolated),          CustomPassives.Interpolated,            out PA_Interpolated,    out PA_Interpolated_Remove);
            AddPassiveIntents(nameof(PA_Humorous),              CustomPassives.Humorous,                out PA_Humorous,        out PA_Humorous_Remove);
            AddPassiveIntents(nameof(PA_Mirage),                CustomPassives.Mirage,                  out PA_Mirage,          out PA_Mirage_Remove);
            AddGenericAddRemoveIntents(nameof(PA_Warts),        CustomPassives.WartsSprite,             out PA_Warts,           out PA_Warts_Remove);
            AddGenericAddRemoveIntents(nameof(PA_AltAttacks),   CustomPassives.AltAttacksSprite,        out PA_AltAttacks,      out PA_AltAttacks_Remove);
            AddGenericAddRemoveIntents(nameof(PA_Invincible),   CustomPassives.InvincibleSprite,        out PA_Invincible,      out PA_Invincible_Remove);
            AddPassiveIntents(nameof(PA_Disguised),             CustomPassives.Disguised,               out PA_Disguised,       out PA_Disguised_Remove);
            #endregion
        }

        internal static void Init()
        {
        }
    }
}
