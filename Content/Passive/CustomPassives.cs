using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Grimoire.Content.TriggerEffects;
using Grimoire.Content.Effect;
using Grimoire.Content.Misc;
using Grimoire.Content.EffectorConditions;
using Grimoire.Content.FieldEffects;
using Pentacle.Effects;

namespace Grimoire.Content.Passive
{
    public static class CustomPassives
    {
        public static readonly BasePassiveAbilitySO CoreRed;
        public static readonly BasePassiveAbilitySO CoreBlue;
        public static readonly BasePassiveAbilitySO CoreYellow;
        public static readonly BasePassiveAbilitySO CorePurple;
        public static readonly BasePassiveAbilitySO CoreGrey;
        public static readonly BasePassiveAbilitySO CoreUntethered;

        public static readonly BasePassiveAbilitySO Immaterial;
        public static readonly BasePassiveAbilitySO Impunity;
        public static readonly BasePassiveAbilitySO Sacrilege;
        public static readonly BasePassiveAbilitySO Sweeping;
        public static readonly BasePassiveAbilitySO Thorny;
        public static readonly BasePassiveAbilitySO Billiard;
        public static readonly BasePassiveAbilitySO Connoisseur;
        public static readonly BasePassiveAbilitySO Chaos;
        public static readonly BasePassiveAbilitySO Grinding;
        public static readonly BasePassiveAbilitySO Disruption;
        public static readonly BasePassiveAbilitySO Interpolated;
        public static readonly BasePassiveAbilitySO Humorous;
        public static readonly BasePassiveAbilitySO Mirage;
        public static readonly BasePassiveAbilitySO Disguised;

        internal static readonly Sprite UntetheredCoreSprite;
        internal static readonly Sprite SturdySprite;
        internal static readonly Sprite ResilientSprite;
        internal static readonly Sprite VolatileSprite;
        internal static readonly Sprite WartsSprite;
        internal static readonly Sprite AltAttacksSprite;
        internal static readonly Sprite InvincibleSprite;

        private static readonly Dictionary<int, BasePassiveAbilitySO> GeneratedSturdy = [];
        private static readonly Dictionary<int, BasePassiveAbilitySO> GeneratedResilient = [];
        private static readonly Dictionary<int, BasePassiveAbilitySO> GeneratedVolatile = [];
        private static readonly Dictionary<int, BasePassiveAbilitySO> GeneratedInvincible = [];
        private static readonly Dictionary<int, BasePassiveAbilitySO> GeneratedWarts = [];
        private static readonly Dictionary<int, BasePassiveAbilitySO> GeneratedProtected = [];

        static CustomPassives()
        {
            GrimoireProfile.TryInitializeProfile();

            UntetheredCoreSprite = ResourceLoader.LoadSprite("Core_Untethered");
            SturdySprite = ResourceLoader.LoadSprite("Sturdy");
            ResilientSprite = ResourceLoader.LoadSprite("Resilient");
            VolatileSprite = ResourceLoader.LoadSprite("Volatile");
            WartsSprite = ResourceLoader.LoadSprite("Warts");
            AltAttacksSprite = ResourceLoader.LoadSprite("AltAttacks");
            InvincibleSprite = ResourceLoader.LoadSprite("Invincible");

            CoreRed     = CoreGenerator([Pigments.Red],     ResourceLoader.LoadSprite("Core_Red"))      .AddToDatabase();
            CoreBlue    = CoreGenerator([Pigments.Blue],    ResourceLoader.LoadSprite("Core_Blue"))     .AddToDatabase();
            CoreYellow  = CoreGenerator([Pigments.Yellow],  ResourceLoader.LoadSprite("Core_Yellow"))   .AddToDatabase();
            CorePurple  = CoreGenerator([Pigments.Purple],  ResourceLoader.LoadSprite("Core_Purple"))   .AddToDatabase();
            CoreGrey    = CoreGenerator([Pigments.Grey],    ResourceLoader.LoadSprite("Core_Grey"))     .AddToDatabase();
            CoreUntethered =
                CoreGenerator([Pigments.Red, Pigments.Blue, Pigments.Yellow, Pigments.Purple], UntetheredCoreSprite, "Core_Untethered_PA", "CoreUntethered", "Untethered Core", "Allows this ally's health color to be toggled to any basic color.")
                .AddToDatabase();

            Immaterial = NewPassive<MultiCustomTriggerEffectPassive>("Immaterial_PA", "Immaterial")
            .SetBasicInformation("Immaterial", "Immaterial")
            .AutoSetDescriptions("This ally is immune to direct damage and all status effects.")
            .AddToGlossary("This party member/enemy is immune to direct damage and all status effects.")
            .SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnBeingDamaged.ToString(),
                    doesPopup = true,
                    immediate = true,

                    effect = new MultiplierIntValueSetterTriggerEffect(0),
                    conditions = [CreateScriptable<DamageReceivedValueChangeDetectionEffectorCondition>(x => x._onlyDirectDamage = true)]
                },
                new()
                {
                    trigger = TriggerCalls.CanApplyStatusEffect.ToString(),
                    doesPopup = true,
                    immediate = true,

                    effect = new StatusEffectApplicationFalseSetterTriggerEffect()
                }
            })
            .AddToDatabase();

            Impunity = NewPassive<MultiCustomTriggerEffectPassive>("Impunity_PA", "Impunity")
            .SetBasicInformation("Impunity", "Impunity")
            .AutoSetDescriptions("The yellow pigment generator now generates gray pigment instead.")
            .AddToGlossary("The yellow pigment generator now generates gray pigment instead.")
            .SetConnectionEffects(new()
            {
                new()
                {
                    immediate = false,
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<ChangePigmentGeneratorPool_Effect>(x => x._newPool = [Pigments.Grey]))
                    })
                }
            })
            .AddToDatabase();

            Sacrilege = NewPassive<MultiCustomTriggerEffectPassive>("Sacrilege_PA", "Sacrilege")
            .SetBasicInformation("Sacrilege", "Sacrilege")
            .AutoSetDescriptions("This ally is Cursed.")
            .AddToGlossary("This party member/enemy is Cursed.")
            .SetConnectionEffects(new()
            {
                new()
                {
                    immediate = false,
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = StatusField.Cursed), 0, Targeting.Slot_SelfSlot)
                    })
                }
            })
            .AddToDatabase();

            Sweeping = NewPassive<MultiCustomTriggerEffectPassive>("Sweeping_PA", "Sweeping")
            .SetBasicInformation("Sweeping", "Sweeping")
            .AutoSetDescriptions("Upon performing an ability, this ally and the Left and Right allies will be randomly moved.")
            .AddToGlossary("Upon performing an ability, this party member/enemy and the Left and Right party members/enemies will be randomly moved.")
            .SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnAbilityUsed.ToString(),
                    immediate = false,
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<MassSwapZoneEffect>(), 0, Targeting.Slot_SelfAndSides)
                    })
                }
            })
            .AddToDatabase();

            Thorny = NewPassive<MultiCustomTriggerEffectPassive>("Thorny_PA", "Thorny")
            .SetBasicInformation("Thorny", "Thorny")
            .SetCharacterDescription("If the wrong pigment is used while performing an ability apply 1 Scar to this party member.")
            .AddToGlossary("If the wrong pigment is used while performing an ability apply 1 Scar to this party member.")
            .SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnWillReceiveCostDamage.ToString(),
                    immediate = false,
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = StatusField.Scars), 1, Targeting.Slot_SelfSlot)
                    })
                }
            })
            .AddToDatabase();

            Billiard = NewPassive<MultiCustomTriggerEffectPassive>("Billiard_PA", "Billiard")
            .SetBasicInformation("Billiard", "Billiard")
            .AutoSetDescriptions("Upon recieving direct damage, remove Constricted from this ally's position and move to the left.")
            .AddToGlossary("Upon recieving direct damage, remove Constricted from this party member/enemy's position and move to the left.")
            .SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnDirectDamaged.ToString(),
                    immediate = false,
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<RemoveFieldEffectEffect>(x => x._field = StatusField.Constricted), 0, Targeting.Slot_SelfAll),
                        Effects.GenerateEffect(CreateScriptable<SwapToOneSideEffect>(x => x._swapRight = false), 0, Targeting.Slot_SelfSlot),
                    })
                }
            })
            .AddToDatabase();

            Connoisseur = NewPassive<MultiCustomTriggerEffectPassive>("Connoisseur_PA", "Connoisseur")
            .SetBasicInformation("Connoisseur", "Connoisseur")
            .AutoSetDescriptions("This ally deals 1/3 extra damage for each status effect on targets.")
            .AddToGlossary("This party member/enemy deals 1/3 extra damage for each status effect on targets.")
            .SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnWillApplyDamage.ToString(),
                    immediate = true,
                    doesPopup = true,

                    effect = new ConnoisseurDamageModifierSetterTriggerEffect()
                }
            })
            .AddToDatabase();

            Chaos = NewPassive<MultiCustomTriggerEffectPassive>("Chaos_PA", "Chaos")
            .SetBasicInformation("Chaos", "Chaos")
            .SetCharacterDescription("This party member's health color and costs are randomized at the start of combat.")
            .SetEnemyDescription("This party member's health color is randomized at the start of combat.")
            .AddToGlossary("This party member's health color and costs are randomized at the start of combat.")
            .SetConnectionEffects(new()
            {
                new()
                {
                    immediate = false,
                    doesPopup = true,
                    
                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<ChangeToRandomHealthColorIncludingCurrentEffect>(x => x.healthColors = new()
                        {
                            Pigments.Red,
                            Pigments.Blue,
                            Pigments.Yellow,
                            Pigments.Purple,
                            Pigments.Grey
                        }), 0, Targeting.Slot_SelfSlot),

                        Effects.GenerateEffect(CreateScriptable<RandomizeAllTargetCostsEffect>(x => x.pool = new()
                        {
                            Pigments.Red,
                            Pigments.Red,
                            Pigments.Red,
                            Pigments.Red,
                            Pigments.Red,
                            Pigments.Red,
                            Pigments.Blue,
                            Pigments.Blue,
                            Pigments.Blue,
                            Pigments.Blue,
                            Pigments.Blue,
                            Pigments.Blue,
                            Pigments.Yellow,
                            Pigments.Yellow,
                            Pigments.Yellow,
                            Pigments.Yellow,
                            Pigments.Yellow,
                            Pigments.Yellow,
                            Pigments.Purple,
                            Pigments.Purple,
                            Pigments.Purple,
                            Pigments.Purple,
                            Pigments.Purple,
                            Pigments.Purple,
                            Pigments.RedBlue,
                            Pigments.BlueRed,
                            Pigments.RedPurple,
                            Pigments.PurpleRed,
                            Pigments.RedYellow,
                            Pigments.YellowRed,
                            Pigments.BluePurple,
                            Pigments.PurpleBlue,
                            Pigments.BlueYellow,
                            Pigments.YellowBlue,
                            Pigments.YellowPurple,
                            Pigments.PurpleYellow,
                            Pigments.Grey,
                        }), 0, Targeting.Slot_SelfSlot)
                    })
                }
            })
            .AddToDatabase();

            Grinding = NewPassive<MultiCustomTriggerEffectPassive>("Grinding_PA", "Grinding")
            .SetBasicInformation("Grinding", "Grinding")
            .AutoSetDescriptions("Upon dying, add 1 cost of this ally's health color to every party member ability.\nWill not add to \"Slap\".")
            .AddToGlossary("Upon dying, add 1 cost of this party member/enemy's health color to every party member ability.")
            .SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnDeath.ToString(),
                    immediate = false,
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<AddCostByHealthColorEffect>(x =>
                        {
                            x.limit = -1;
                            x.ignoredAbilities = ["Slap_A"];
                        }), 1, Targeting.AllUnits)
                    })
                }
            })
            .AddToDatabase();

            Disruption = NewPassive<MultiCustomTriggerEffectPassive>("Disruption_PA", "Disruption")
            .SetBasicInformation("Disruption", "Disruption")
            .AutoSetDescriptions("Randomize and reduce all party member ability costs.")
            .AddToGlossary("Randomize and reduce all party member ability costs.")
            .SetConnectionEffects(new()
            {
                new()
                {
                    immediate = false,
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<RandomizeAndChangeCostAmountEffect>(x =>
                        {
                            x.pool = new()
                            {
                                Pigments.Red,
                                Pigments.Red,
                                Pigments.Red,
                                Pigments.Red,
                                Pigments.Red,
                                Pigments.Red,
                                Pigments.Blue,
                                Pigments.Blue,
                                Pigments.Blue,
                                Pigments.Blue,
                                Pigments.Blue,
                                Pigments.Blue,
                                Pigments.Yellow,
                                Pigments.Yellow,
                                Pigments.Yellow,
                                Pigments.Yellow,
                                Pigments.Yellow,
                                Pigments.Yellow,
                                Pigments.Purple,
                                Pigments.Purple,
                                Pigments.Purple,
                                Pigments.Purple,
                                Pigments.Purple,
                                Pigments.Purple,
                                Pigments.RedBlue,
                                Pigments.BlueRed,
                                Pigments.RedPurple,
                                Pigments.PurpleRed,
                                Pigments.RedYellow,
                                Pigments.YellowRed,
                                Pigments.BluePurple,
                                Pigments.PurpleBlue,
                                Pigments.BlueYellow,
                                Pigments.YellowBlue,
                                Pigments.YellowPurple,
                                Pigments.PurpleYellow,
                                Pigments.Grey,
                            };

                            x.increase = false;
                            x.minAmount = 1;
                        }), 1, Targeting.AllUnits)
                    })
                }
            })
            .AddToDatabase();

            Interpolated = NewPassive<MultiCustomTriggerEffectPassive>("Interpolated_PA", "Interpolated")
            .SetBasicInformation("Interpolated", "Interpolated")
            .AutoSetDescriptions("All damage this ally recieves is turned into an equivalent amount of Disappearing.")
            .AddToGlossary("All damage this party member/enemy recieves is turned into an equivalent amount of Disappearing.")
            .SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnBeingDamaged.ToString(),
                    immediate = true,
                    doesPopup = true,

                    effect = new InterpolatedDamageModifierSetterTriggerEffect()
                    {
                        status = CustomStatusEffects.Disappearing
                    },
                    conditions = new()
                    {
                        CreateScriptable<DamageReceivedValueChangeDetectionWithDamageTypeEffectorCondition>(x =>
                        {
                            x.damageType = CustomDamageTypes.DisappearingDamage;
                            x.damageTypeMustMatch = false;
                        })
                    }
                }
            })
            .AddToDatabase();

            Humorous = NewPassive<MultiCustomTriggerEffectPassive>("Humorous_PA", "Humorous")
            .SetBasicInformation("Humorous", "Humorous")
            .AutoSetDescriptions("Upon taking damage, this ally's health will change to red.")
            .AddToGlossary("Upon taking damage, this party member/enemy's health will change to red.")
            .SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnDirectDamaged.ToString(),
                    immediate = false,
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<ChangeToRandomHealthColorEffect>(x => x._healthColors = [Pigments.Red]), 0, Targeting.Slot_SelfSlot)
                    })
                }
            })
            .AddToDatabase();

            Mirage = NewPassive<MultiCustomTriggerEffectPassive>("Mirage_PA", "Mirage")
            .SetBasicInformation("Mirage", "Mirage")
            .AutoSetDescriptions("This ally will apply 1 Shadow Hands to their current position at the beginning of each turn.")
            .AddToGlossary("This party member/enemy will apply 1 Shadow Hands to their current position at the beginning of each turn.")
            .SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnTurnStart.ToString(),
                    immediate = false,
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<FieldEffect_Apply_Effect>(x => x._Field = CustomFieldEffects.ShadowHands), 1, Targeting.Slot_SelfAll)
                    })
                }
            })
            .AddToDatabase();

            Disguised = NewPassive<MultiCustomTriggerEffectPassive>("Disguised_PA", "Disguised")
            .SetBasicInformation("Disguised", "Disguised")
            .AutoSetDescriptions("Prevents incoming damage to this ally once and then removes this passive.")
            .AddToGlossary("Prevents incoming damage to this party member/enemy once and then removes this passive.")
            .SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnBeingDamaged.ToString(),
                    immediate = true,
                    doesPopup = true,

                    effect = new MultiplierWithPerformEffectDamageModifierSetterTriggerEffect()
                    {
                        multiplier = 0,
                        effects = new()
                        {
                            Effects.GenerateEffect(CreateScriptable<RemovePassiveEffect>(x => x.m_PassiveID = "Disguised"), 0, Targeting.Slot_SelfSlot)
                        }
                    }
                }
            })
            .AddToDatabase();

            Glossary.CreateAndAddCustom_PassiveToGlossary("Pigment Core", "Unlocks the ability to change the color of this party member/enemy's health through a button next to their health bar.", UntetheredCoreSprite);
            Glossary.CreateAndAddCustom_PassiveToGlossary("Sturdy", "Damage received by this party member/enemy is rounded down to a certain amount.", SturdySprite);
            Glossary.CreateAndAddCustom_PassiveToGlossary("Resilient", "Damage received by this party member/enemy is capped at a certain amount per turn.", ResilientSprite);
            Glossary.CreateAndAddCustom_PassiveToGlossary("Volatile", "Upon this party member/enemy receiving any damage, deal a certain amount of indirect damage to all enemies/party members.", VolatileSprite);
            Glossary.CreateAndAddCustom_PassiveToGlossary("Warts", "Apply Shield to this position upon receiving direct damage.", WartsSprite);
            Glossary.CreateAndAddCustom_PassiveToGlossary("Alt Attacks", "This enemy will perform an additional ability each turn, this ability is randomly selected from a given set.", AltAttacksSprite);
            Glossary.CreateAndAddCustom_PassiveToGlossary("Invincible", "Prevent all incoming damage that is less than or equal to this party member/enemy's level of Invincible.", InvincibleSprite);
        }

        internal static void Init()
        {
        }

        public static BasePassiveAbilitySO ProtectedGenerator(int count)
        {
            return GetOrCreatePassive(GeneratedProtected, count, x =>
            {
                var pa = NewPassive<MultiCustomTriggerEffectPassive>($"Protected_{x}_PA", "Protected")
                    .SetBasicInformation($"Protected ({x})", StatusField.Shield.EffectInfo.icon) // TODO: add unique passive icon
                    .AutoSetDescriptions($"Permanently applies {x} shield to this ally's position.");

                var effect = new EffectsAndTrigger()
                {
                    trigger = TriggerCalls.OnMoved.ToString(),
                    immediate = true,
                    doesPopup = true,

                    effect = new AllInOnePermaFieldEffectApplicationTriggerEffect()
                    {
                        amount = x,
                        applyOnAllySlots = true,
                        field = StatusField.Shield,
                        targetOffsets = [0]
                    }
                };

                pa.SetConnectionEffects([effect]);
                pa.SetDisconnectionEffects([effect]);
                pa.SetTriggerEffects([effect]);

                return pa;
            });
        }

        public static BasePassiveAbilitySO DisguisedGenerator(CharacterSO disguisedTransformation, CharacterSO nonDisguisedTransformation)
        {
            if(disguisedTransformation == null)
            {
                Debug.LogError("Null disguised transformation.");
                return null;
            }

            if(nonDisguisedTransformation == null)
            {
                Debug.LogError("Null non disguised transformation");
                return null;
            }

            var trimmedDisguisedId = disguisedTransformation.name;
            if (trimmedDisguisedId.EndsWith("_CH"))
                trimmedDisguisedId = trimmedDisguisedId.Substring(0, trimmedDisguisedId.Length - "_CH".Length);

            var trimmedNonDisguisedId = nonDisguisedTransformation.name;
            if (trimmedNonDisguisedId.EndsWith("_CH"))
                trimmedNonDisguisedId = trimmedNonDisguisedId.Substring(0, trimmedNonDisguisedId.Length - "_CH".Length);

            var pa = NewPassive<MultiCustomTriggerEffectPassive>($"Disguised_{trimmedDisguisedId}_{trimmedNonDisguisedId}_PA", "Disguised")
                .SetBasicInformation("Disguised", "Disguised")
                .SetCharacterDescription("This party member disguises themself at the start of combat.\nPrevents incoming damage to this party member once, undisguises them and then removes this passive.");

            pa.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.OnCombatStart.ToString(),
                    immediate = false,
                    doesPopup = true,

                    effect = new PerformEffectTriggerEffect(new()
                    {
                        Effects.GenerateEffect(CreateScriptable<CasterTransformationEffect>(x =>
                        {
                            x._fullyHeal = false;
                            x._maintainTimelineAbilities = false;
                            x._maintainMaxHealth = true;
                            x._currentToMaxHealth = false;

                            x._enemyTransformation = null;
                            x._characterTransformation = disguisedTransformation.name;
                        })),
                        Effects.GenerateEffect(CreateScriptable<AddPassiveEffect>(x => x._passiveToAdd = pa), 0, Targeting.Slot_SelfSlot)
                    })
                },
                new()
                {
                    trigger = TriggerCalls.OnBeingDamaged.ToString(),
                    immediate = true,
                    doesPopup = true,

                    effect = new MultiplierWithPerformEffectDamageModifierSetterTriggerEffect()
                    {
                        multiplier = 0,
                        effects = new()
                        {
                            Effects.GenerateEffect(CreateScriptable<CasterTransformationEffect>(x =>
                            {
                                x._fullyHeal = false;
                                x._maintainTimelineAbilities = false;
                                x._maintainMaxHealth = true;
                                x._currentToMaxHealth = false;

                                x._enemyTransformation = null;
                                x._characterTransformation = nonDisguisedTransformation.name;
                            })),
                            Effects.GenerateEffect(CreateScriptable<RemovePassiveEffect>(x => x.m_PassiveID = "Disguised"), 0, Targeting.Slot_SelfSlot)
                        }
                    }
                }
            });

            return pa;
        }

        public static BasePassiveAbilitySO AltAttacksGenerator(List<ExtraAbilityInfo> bonusAbilities)
        {
            if(bonusAbilities == null)
            {
                Debug.LogError("Null bonus abilities.");
                return null;
            }

            var abIds = new List<string>();
            var abNames = new List<string>();

            foreach (var ab in bonusAbilities)
            {
                if(ab == null)
                    continue;

                var a = ab.ability;

                if(a == null || string.IsNullOrEmpty(a.name))
                    continue;
                
                var trimmedAbilityId = a.name;
                if (trimmedAbilityId.EndsWith("_A"))
                    trimmedAbilityId = trimmedAbilityId.Substring(0, trimmedAbilityId.Length - "_A".Length);

                abIds.Add(trimmedAbilityId);

                if(!string.IsNullOrEmpty(ab.ability._abilityName))
                    abNames.Add(ab.ability._abilityName);
            }

            if(abIds.Count <= 0)
            {
                Debug.LogError("Empty/invalid bonus abilities.");
                return null;
            }

            var pa = NewPassive<MultiCustomTriggerEffectPassive>($"AltAttacks_{string.Join("_", abNames)}_PA", $"AltAttacks_{string.Join("_", abIds)}")
                .SetBasicInformation("Alt Attacks", AltAttacksSprite)
                .SetEnemyDescription($"This enemy will perform an additional ability each turn, this ability is randomly selected from the following:\n{string.Join("\n", abNames)}");

            pa.SetConnectionEffects(new()
            {
                new()
                {
                    immediate = true,
                    doesPopup = true,

                    effect = new AddOrRemoveExtraAbilitiesTriggerEffect()
                    {
                        abilites = bonusAbilities,
                        remove = false
                    }
                }
            });
            pa.SetDisconnectionEffects(new()
            {
                new()
                {
                    immediate = true,
                    doesPopup = true,

                    effect = new AddOrRemoveExtraAbilitiesTriggerEffect()
                    {
                        abilites = bonusAbilities,
                        remove = true
                    }
                }
            });
            pa.SetTriggerEffects(new()
            {
                new()
                {
                    trigger = TriggerCalls.ExtraAdditionalAttacks.ToString(),
                    immediate = true,
                    doesPopup = false,

                    effect = new AddRandomExtraAbilityToPerformedTriggerEffect()
                    {
                        abilites = bonusAbilities
                    }
                }
            });

            return pa;
        }

        public static BasePassiveAbilitySO WartsGenerator(int count)
        {
            return GetOrCreatePassive(GeneratedWarts, count, x =>
            {
                var pa = NewPassive<MultiCustomTriggerEffectPassive>($"Warts_{x}_PA", "Warts")
                    .SetBasicInformation($"Warts ({x})", WartsSprite)
                    .AutoSetDescriptions($"Apply {x} Shield to this position upon receiving direct damage.");

                pa.SetTriggerEffects(new()
                {
                    new()
                    {
                        trigger = TriggerCalls.OnDirectDamaged.ToString(),
                        doesPopup = true,
                        immediate = false,

                        effect = new PerformEffectTriggerEffect(new()
                        {
                            Effects.GenerateEffect(CreateScriptable<CasterStoreValueCheckOverThresholdEffect>(e => e.m_unitStoredDataID = PassiveStoredValues.WartsAddition._UnitStoreDataID)),
                            Effects.GenerateEffect(CreateScriptable<FieldEffect_Apply_Effect>(e =>
                            {
                                e._Field = StatusField.Shield;
                                e._UsePreviousExitValueAsMultiplier = true;
                                e._PreviousExtraAddition = x;
                            }), 1, Targeting.Slot_SelfAll)
                        })
                    }
                });

                return pa;
            });
        }

        public static BasePassiveAbilitySO InvincibleGenerator(int count)
        {
            return GetOrCreatePassive(GeneratedInvincible, count, x =>
            {
                var pa = NewPassive<MultiCustomTriggerEffectPassive>($"Invincible_{x}_PA", "Invincible")
                    .SetBasicInformation($"Invincible ({x})", InvincibleSprite)
                    .AutoSetDescriptions($"Prevent all incoming damage that is less than or equal to {x}.");

                pa.SetTriggerEffects(new()
                {
                    new()
                    {
                        trigger = TriggerCalls.OnBeingDamaged.ToString(),
                        doesPopup = true,
                        immediate = true,

                        effect = new InvincibleDamageModifierSetterTriggerEffect()
                        {
                            invincibility = x,
                            invincibilityStoredValue = PassiveStoredValues.InvincibleAddition
                        }
                    }
                });

                return pa;
            });
        }

        public static BasePassiveAbilitySO SturdyGenerator(int count)
        {
            return GetOrCreatePassive(GeneratedSturdy, count, x =>
            {
                var pa = NewPassive<MultiCustomTriggerEffectPassive>($"Sturdy_{x}_PA", "Sturdy")
                    .SetBasicInformation($"Sturdy ({x})", SturdySprite)
                    .AutoSetDescriptions($"Damage received by this ally is rounded down to {x}.");

                pa.SetTriggerEffects(new()
                {
                    new()
                    {
                        trigger = TriggerCalls.OnBeingDamaged.ToString(),
                        doesPopup = true,
                        immediate = true,

                        effect = new DamageCapDamageModifierSetterTriggerEffect()
                        {
                            damageCapMax = x,
                            damageCapStoredValue = PassiveStoredValues.SturdyAddition
                        } 
                    }
                });

                return pa;
            });
        }

        public static BasePassiveAbilitySO ResilientGenerator(int count)
        {
            return GetOrCreatePassive(GeneratedResilient, count, x =>
            {
                var pa = NewPassive<MultiCustomTriggerEffectPassive>($"Resilient_{x}_PA", "Resilient")
                    .SetBasicInformation($"Resilient ({x})", ResilientSprite)
                    .AutoSetDescriptions($"Damage received by this ally is capped at {x} per turn.");

                pa.StoredValues = new()
                {
                    PassiveStoredValues.ResilientRemainingDamage,
                    PassiveStoredValues.ResilientAddition
                };

                pa.SetTriggerEffects(new()
                {
                    new()
                    {
                        trigger = TriggerCalls.OnBeingDamaged.ToString(),
                        doesPopup = true,
                        immediate = true,

                        effect = new DamageCapDamageModifierSetterTriggerEffect()
                        {
                            damageCapMax = 0,
                            damageCapStoredValue = PassiveStoredValues.ResilientRemainingDamage
                        } 
                    },

                    new()
                    {
                        trigger = TriggerCalls.OnDamaged.ToString(),
                        doesPopup = false,
                        immediate = true,

                        effect = new PerformEffectWithIntReferenceEntryTriggerEffect(new()
                        {
                            Effects.GenerateEffect(CreateScriptable<CasterStoredValueChangeByPreviousExitValueEffect>(x =>
                            {
                                x.storedValue = PassiveStoredValues.ResilientRemainingDamage;
                                x.increase = false;
                                x.minimumValue = 0;
                            }), 1)
                        })
                    },

                    new()
                    {
                        trigger = CustomTriggers.OnPlayerTurnStartUniversal,
                        doesPopup = false,
                        immediate = true,

                        effect = new PerformEffectTriggerEffect(new()
                        {
                            Effects.GenerateEffect(CreateScriptable<CasterStoreValueSetterEffect>(x => x.m_unitStoredDataID = PassiveStoredValues.ResilientRemainingDamage._UnitStoreDataID), x),

                            Effects.GenerateEffect(CreateScriptable<CasterStoreValueCheckOverThresholdEffect>(x => x.m_unitStoredDataID = PassiveStoredValues.ResilientAddition._UnitStoreDataID)),
                            Effects.GenerateEffect(CreateScriptable<CasterStoredValueChangeByPreviousExitValueEffect>(x =>
                            {
                                x.storedValue = PassiveStoredValues.ResilientRemainingDamage;
                                x.increase = true;
                            }), 1),
                        })
                    }
                });

                pa.SetConnectionEffects(new()
                {
                    new()
                    {
                        doesPopup = false,
                        immediate = true,

                        effect = new PerformEffectTriggerEffect(new()
                        {
                            Effects.GenerateEffect(CreateScriptable<CasterStoreValueSetterEffect>(x => x.m_unitStoredDataID = PassiveStoredValues.ResilientRemainingDamage._UnitStoreDataID), x),

                            Effects.GenerateEffect(CreateScriptable<CasterStoreValueCheckOverThresholdEffect>(x => x.m_unitStoredDataID = PassiveStoredValues.ResilientAddition._UnitStoreDataID)),
                            Effects.GenerateEffect(CreateScriptable<CasterStoredValueChangeByPreviousExitValueEffect>(x =>
                            {
                                x.storedValue = PassiveStoredValues.ResilientRemainingDamage;
                                x.increase = true;
                            }), 1),
                        })
                    }
                });
                pa.SetDisconnectionEffects(new()
                {
                    new()
                    {
                        doesPopup = false,
                        immediate = true,
                
                        effect = new PerformEffectTriggerEffect(new()
                        {
                            Effects.GenerateEffect(CreateScriptable<CasterStoreValueSetterEffect>(x => x.m_unitStoredDataID = PassiveStoredValues.ResilientRemainingDamage._UnitStoreDataID), -1)
                        })
                    }
                });

                return pa;
            });
        }

        public static BasePassiveAbilitySO VolatileGenerator(int count)
        {
            return GetOrCreatePassive(GeneratedVolatile, count, x =>
            {
                var pa = NewPassive<MultiCustomTriggerEffectPassive>($"Volatile_{x}_PA", "Volatile")
                    .SetBasicInformation($"Volatile ({x})", VolatileSprite)
                    .AutoSetDescriptions($"Upon this ally receiving any damage, deal {x} indirect damage to all opponents.");

                pa.SetTriggerEffects(new()
                {
                    new()
                    {
                        trigger = TriggerCalls.OnDamaged.ToString(),
                        doesPopup = true,
                        immediate = false,

                        effect = new PerformEffectTriggerEffect(new()
                        {
                            Effects.GenerateEffect(CreateScriptable<DamageByStoredValueEffect>(x =>
                            {
                                x._indirect = true;
                                x.m_unitStoredDataID = PassiveStoredValues.VolatileAddition._UnitStoreDataID;
                            }), 1, Targeting.Unit_AllOpponents)
                        })
                    }
                });

                return pa;
            });
        }

        public static BasePassiveAbilitySO CoreGenerator(List<ManaColorSO> addedHealthOptions, Sprite passiveSprite, string id = "Core_{0}_PA", string passiveId = "Core{0}", string coreName = "{0} Core", string coreDescription = "Allows this ally's health color to be toggled to {0}.")
        {
            if (addedHealthOptions == null)
            {
                Debug.LogError("Null health options");

                return null;
            }

            var pigmentsString = "";
            var pigmentsId = "";
            for (var i = 0; i < addedHealthOptions.Count; i++)
            {
                if (i > 0)
                {
                    if (i == addedHealthOptions.Count - 1)
                        pigmentsString += " and ";

                    else
                        pigmentsString += ", ";
                }

                var pigment = addedHealthOptions[i];

                if (pigment == null)
                    continue;

                pigmentsString += pigment.pigmentID;
                pigmentsId += pigment.pigmentID;
            }

            return
                NewPassive<MultiCustomTriggerEffectPassive>(string.Format(id, pigmentsId), string.Format(passiveId, pigmentsId))
                .SetBasicInformation(string.Format(coreName, pigmentsString), passiveSprite)
                .AutoSetDescriptions(string.Format(coreDescription, pigmentsString))
                .SetConnectionEffects(new()
                {
                    new()
                    {
                        immediate = false,
                        doesPopup = true,

                        effect = new PerformEffectTriggerEffect(new()
                        {
                            Effects.GenerateEffect(CreateScriptable<AddHealthColorsNotInOptionsEffect>(x => x.healthColors = addedHealthOptions), 0, Targeting.Slot_SelfSlot)
                        })
                    }
                });
        }

        private static TValue GetOrCreatePassive<TKey, TValue>(IDictionary<TKey, TValue> readFrom, TKey key, Func<TKey, TValue> create)
        {
            if (readFrom.TryGetValue(key, out var value))
                return value;

            return readFrom[key] = create(key);
        }
    }
}
