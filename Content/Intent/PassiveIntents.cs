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
        public static readonly string PA_Fatalism, PA_Fatalism_Remove;
        public static readonly string PA_FinancialHyperinflation, PA_FinancialHyperinflation_Remove;
        public static readonly string PA_Fleeting, PA_Fleeting_Remove;
        public static readonly string PA_Flonked, PA_Flonked_Remove;
        public static readonly string PA_Focus, PA_Focus_Remove;
        public static readonly string PA_ForbiddenFruit, PA_ForbiddenFruit_Remove;
        public static readonly string PA_Forgetful, PA_Forgetful_Remove;
        public static readonly string PA_Formless, PA_Formless_Remove;
        public static readonly string PA_FullReset, PA_FullReset_Remove;
        public static readonly string PA_Immortal, PA_Immortal_Remove;
        public static readonly string PA_Inanimate, PA_Inanimate_Remove;
        public static readonly string PA_Infantile, PA_Infantile_Remove;
        public static readonly string PA_Inferno, PA_Inferno_Remove;
        public static readonly string PA_Infestation, PA_Infestation_Remove;
        public static readonly string PA_Leaky, PA_Leaky_Remove;
        public static readonly string PA_Masochism, PA_Masochism_Remove;
        public static readonly string PA_MultiAttack, PA_MultiAttack_Remove;
        public static readonly string PA_Obscure, PA_Obscure_Remove;
        public static readonly string PA_Omnipresent, PA_Omnipresent_Remove;
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
            (PA_Abomination,        PA_Abomination_Remove)          = (IntentType_GameIDs.PA_Abomination.ToString(),            AddIntent(nameof(PA_Abomination_Remove),        Passives.Abomination1.passiveIcon, IntentColor_StatusRemove));
            (PA_Absorb,             PA_Absorb_Remove)               = (IntentType_GameIDs.PA_Absorb.ToString(),                 AddIntent(nameof(PA_Absorb_Remove),             Passives.Absorb.passiveIcon, IntentColor_StatusRemove));
            (PA_Anchored,           PA_Anchored_Remove)             = (IntentType_GameIDs.PA_Anchored.ToString(),               AddIntent(nameof(PA_Anchored_Remove),           Passives.Anchored.passiveIcon, IntentColor_StatusRemove));
            (PA_BoneSpurs,          PA_BoneSpurs_Remove)            = (IntentType_GameIDs.PA_BoneSpurs.ToString(),              AddIntent(nameof(PA_BoneSpurs_Remove),          Passives.BoneSpurs1.passiveIcon, IntentColor_StatusRemove));
            (PA_BronzosBlessing,    PA_BronzosBlessing_Remove)      = (IntentType_GameIDs.PA_BronzosBlessing.ToString(),        AddIntent(nameof(PA_BronzosBlessing_Remove),    Passives.BronzosBlessing.passiveIcon, IntentColor_StatusRemove));
            (PA_Catalyst,           PA_Catalyst_Remove)             = (IntentType_GameIDs.PA_Catalyst.ToString(),               AddIntent(nameof(PA_Catalyst_Remove),           Passives.Catalyst.passiveIcon, IntentColor_StatusRemove));
            (PA_Confusion,          PA_Confusion_Remove)            = (IntentType_GameIDs.PA_Confusion.ToString(),              AddIntent(nameof(PA_Confusion_Remove),          Passives.Confusion.passiveIcon, IntentColor_StatusRemove));
            (PA_Constricting,       PA_Constricting_Remove)         = (IntentType_GameIDs.PA_Constricting.ToString(),           AddIntent(nameof(PA_Constricting_Remove),       Passives.Constricting.passiveIcon, IntentColor_StatusRemove));
            (PA_Construct,          PA_Construct_Remove)            = (IntentType_GameIDs.PA_Construct.ToString(),              AddIntent(nameof(PA_Construct_Remove),          Passives.Construct.passiveIcon, IntentColor_StatusRemove));
            (PA_Delicate,           PA_Delicate_Remove)             = (IntentType_GameIDs.PA_Delicate.ToString(),               AddIntent(nameof(PA_Delicate_Remove),           Passives.Delicate.passiveIcon, IntentColor_StatusRemove));
            (PA_Dying,              PA_Dying_Remove)                = (IntentType_GameIDs.PA_Dying.ToString(),                  AddIntent(nameof(PA_Dying_Remove),              Passives.Dying.passiveIcon, IntentColor_StatusRemove));
            (PA_Enfeebled,          PA_Enfeebled_Remove)            = (IntentType_GameIDs.PA_Enfeebled.ToString(),              AddIntent(nameof(PA_Enfeebled_Remove),          Passives.Enfeebled.passiveIcon, IntentColor_StatusRemove));
            (PA_EssenceBlue,        PA_EssenceBlue_Remove)          = (IntentType_GameIDs.PA_Essence_Blue.ToString(),           AddIntent(nameof(PA_EssenceBlue_Remove),        Passives.EssenceBlue.passiveIcon, IntentColor_StatusRemove));
            (PA_EssencePurple,      PA_EssencePurple_Remove)        = (IntentType_GameIDs.PA_Essence_Purple.ToString(),         AddIntent(nameof(PA_EssencePurple_Remove),      Passives.EssencePurple.passiveIcon, IntentColor_StatusRemove));
            (PA_EssenceRed,         PA_EssenceRed_Remove)           = (IntentType_GameIDs.PA_Essence_Red.ToString(),            AddIntent(nameof(PA_EssenceRed_Remove),         Passives.EssenceRed.passiveIcon, IntentColor_StatusRemove));
            (PA_EssenceYellow,      PA_EssenceYellow_Remove)        = (IntentType_GameIDs.PA_Essence_Yellow.ToString(),         AddIntent(nameof(PA_EssenceYellow_Remove),      Passives.EssenceYellow.passiveIcon, IntentColor_StatusRemove));
            (PA_EssenceUntethered,  PA_EssenceUntethered_Remove)    = (IntentType_GameIDs.PA_Essence_Untethered.ToString(),     AddIntent(nameof(PA_EssenceUntethered_Remove),  Passives.EssenceUntethered.passiveIcon, IntentColor_StatusRemove));
            (PA_Fleeting,           PA_Fleeting_Remove)             = (IntentType_GameIDs.PA_Fleeting.ToString(),               AddIntent(nameof(PA_Fleeting_Remove),           Passives.Fleeting1.passiveIcon, IntentColor_StatusRemove));
            (PA_Focus,              PA_Focus_Remove)                = (IntentType_GameIDs.PA_Focus.ToString(),                  AddIntent(nameof(PA_Focus_Remove),              Passives.Focus.passiveIcon, IntentColor_StatusRemove));
            (PA_ForbiddenFruit,     PA_ForbiddenFruit_Remove)       = (IntentType_GameIDs.PA_ForbiddenFruit.ToString(),         AddIntent(nameof(PA_ForbiddenFruit_Remove),     Passives.ForbiddenFruitInHerImage.passiveIcon, IntentColor_StatusRemove));
            (PA_Forgetful,          PA_Forgetful_Remove)            = (IntentType_GameIDs.PA_Forgetful.ToString(),              AddIntent(nameof(PA_Forgetful_Remove),          Passives.Forgetful.passiveIcon, IntentColor_StatusRemove));
            (PA_Formless,           PA_Formless_Remove)             = (IntentType_GameIDs.PA_Formless.ToString(),               AddIntent(nameof(PA_Formless_Remove),           Passives.Formless.passiveIcon, IntentColor_StatusRemove));
            (PA_Immortal,           PA_Immortal_Remove)             = (IntentType_GameIDs.PA_Immortal.ToString(),               AddIntent(nameof(PA_Immortal_Remove),           Passives.Immortal.passiveIcon, IntentColor_StatusRemove));
            (PA_Inanimate,          PA_Inanimate_Remove)            = (IntentType_GameIDs.PA_Inanimate.ToString(),              AddIntent(nameof(PA_Inanimate_Remove),          Passives.Inanimate.passiveIcon, IntentColor_StatusRemove));
            (PA_Infantile,          PA_Infantile_Remove)            = (IntentType_GameIDs.PA_Infantile.ToString(),              AddIntent(nameof(PA_Infantile_Remove),          Passives.Infantile.passiveIcon, IntentColor_StatusRemove));
            (PA_Inferno,            PA_Inferno_Remove)              = (IntentType_GameIDs.PA_Inferno.ToString(),                AddIntent(nameof(PA_Inferno_Remove),            Passives.Inferno.passiveIcon, IntentColor_StatusRemove));
            (PA_Infestation,        PA_Infestation_Remove)          = (IntentType_GameIDs.PA_Infestation.ToString(),            AddIntent(nameof(PA_Infestation_Remove),        Passives.Infestation1.passiveIcon, IntentColor_StatusRemove));
            (PA_Leaky,              PA_Leaky_Remove)                = (IntentType_GameIDs.PA_Leaky.ToString(),                  AddIntent(nameof(PA_Leaky_Remove),              Passives.Leaky1.passiveIcon, IntentColor_StatusRemove));
            (PA_Masochism,          PA_Masochism_Remove)            = (IntentType_GameIDs.PA_Masochism.ToString(),              AddIntent(nameof(PA_Masochism_Remove),          Passives.Masochism1.passiveIcon, IntentColor_StatusRemove));
            (PA_MultiAttack,        PA_MultiAttack_Remove)          = (IntentType_GameIDs.PA_MultiAttack.ToString(),            AddIntent(nameof(PA_MultiAttack_Remove),        Passives.MultiAttack2.passiveIcon, IntentColor_StatusRemove));
            (PA_Overexert,          PA_Overexert_Remove)            = (IntentType_GameIDs.PA_Overexert.ToString(),              AddIntent(nameof(PA_Overexert_Remove),          Passives.Overexert1.passiveIcon, IntentColor_StatusRemove));
            (PA_PanicAttack,        PA_PanicAttack_Remove)          = (IntentType_GameIDs.PA_PanicAttack.ToString(),            AddIntent(nameof(PA_PanicAttack_Remove),        Passives.PanicAttack.passiveIcon, IntentColor_StatusRemove));
            (PA_Mutualism,          PA_Mutualism_Remove)            = (IntentType_GameIDs.PA_Mutualism.ToString(),              AddIntent(nameof(PA_Mutualism_Remove),          Passives.ParasiteMutualism.passiveIcon, IntentColor_StatusRemove));
            (PA_Parasitism,         PA_Parasitism_Remove)           = (IntentType_GameIDs.PA_Parasitism.ToString(),             AddIntent(nameof(PA_Parasitism_Remove),         Passives.ParasiteParasitism.passiveIcon, IntentColor_StatusRemove));
            (PA_Pure,               PA_Pure_Remove)                 = (IntentType_GameIDs.PA_Pure.ToString(),                   AddIntent(nameof(PA_Pure_Remove),               Passives.Pure.passiveIcon, IntentColor_StatusRemove));
            (PA_Reborn,             PA_Reborn_Remove)               = (IntentType_GameIDs.PA_Reborn.ToString(),                 AddIntent(nameof(PA_Reborn_Remove),             Passives.RebornToInHerImage.passiveIcon, IntentColor_StatusRemove));
            (PA_Skittish,           PA_Skittish_Remove)             = (IntentType_GameIDs.PA_Skittish.ToString(),               AddIntent(nameof(PA_Skittish_Remove),           Passives.Skittish.passiveIcon, IntentColor_StatusRemove));
            (PA_Slippery,           PA_Slippery_Remove)             = (IntentType_GameIDs.PA_Slippery.ToString(),               AddIntent(nameof(PA_Slippery_Remove),           Passives.Slippery.passiveIcon, IntentColor_StatusRemove));
            (PA_Transfusion,        PA_Transfusion_Remove)          = (IntentType_GameIDs.PA_Transfusion.ToString(),            AddIntent(nameof(PA_Transfusion_Remove),        Passives.Transfusion.passiveIcon, IntentColor_StatusRemove));
            (PA_TwoFaced,           PA_TwoFaced_Remove)             = (IntentType_GameIDs.PA_TwoFaced_RedBlue.ToString(),       AddIntent(nameof(PA_TwoFaced_Remove),           Passives.TwoFaced.passiveIcon, IntentColor_StatusRemove));
            (PA_Unstable,           PA_Unstable_Remove)             = (IntentType_GameIDs.PA_Unstable.ToString(),               AddIntent(nameof(PA_Unstable_Remove),           Passives.Unstable.passiveIcon, IntentColor_StatusRemove));
            (PA_Withering,          PA_Withering_Remove)            = (IntentType_GameIDs.PA_Withering.ToString(),              AddIntent(nameof(PA_Withering_Remove),          Passives.Withering.passiveIcon, IntentColor_StatusRemove));
            (PA_Parental,           PA_Parental_Remove)             = (IntentType_GameIDs.PA_Parental.ToString(),               AddIntent(nameof(PA_Parental_Remove),           Passives.Example_Parental_Vengeance.passiveIcon, IntentColor_StatusRemove));
            (PA_Decay,              PA_Decay_Remove)                = (IntentType_GameIDs.PA_Decay.ToString(),                  AddIntent(nameof(PA_Decay_Remove),              Passives.Example_Decay_MudLung.passiveIcon, IntentColor_StatusRemove));
            
            AddPassiveIntents(nameof(PA_Cashout),                   Passives.Cashout,                       out PA_Cashout,                 out PA_Cashout_Remove);
            AddPassiveIntents(nameof(PA_Fatalism),                  Passives.Fatalism,                      out PA_Fatalism,                out PA_Fatalism_Remove);
            AddPassiveIntents(nameof(PA_FinancialHyperinflation),   Passives.FinancialHyperinflation,       out PA_FinancialHyperinflation, out PA_FinancialHyperinflation_Remove);
            AddPassiveIntents(nameof(PA_Flonked),                   Passives.Flonked,                       out PA_Flonked,                 out PA_Flonked_Remove);
            AddPassiveIntents(nameof(PA_FullReset),                 Passives.FullReset,                     out PA_FullReset,               out PA_FullReset_Remove);
            AddPassiveIntents(nameof(PA_Obscure),                   Passives.Obscure,                       out PA_Obscure,                 out PA_Obscure_Remove);
            AddPassiveIntents(nameof(PA_Omnipresent),               Passives.Omnipresent,                   out PA_Omnipresent,             out PA_Omnipresent_Remove);
            AddPassiveIntents(nameof(PA_BonusAttack),               Passives.Example_BonusAttack_Mangle,    out PA_BonusAttack,             out PA_BonusAttack_Remove);
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
