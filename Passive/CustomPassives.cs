using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Grimoire.Misc;
using Grimoire.Effect;

namespace Grimoire.Passive
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

        internal static readonly Sprite UntetheredCoreSprite;
        internal static readonly Sprite SturdySprite;
        internal static readonly Sprite ResilientSprite;
        internal static readonly Sprite VolatileSprite;

        private static readonly Dictionary<int, BasePassiveAbilitySO> GeneratedSturdy = [];
        private static readonly Dictionary<int, BasePassiveAbilitySO> GeneratedResilient = [];
        private static readonly Dictionary<int, BasePassiveAbilitySO> GeneratedVolatile = [];
        private static readonly Dictionary<int, BasePassiveAbilitySO> GeneratedProtected = [];

        static CustomPassives()
        {
            GrimoireProfile.TryInitializeProfile();

            UntetheredCoreSprite = ResourceLoader.LoadSprite("Core_Untethered");
            SturdySprite = ResourceLoader.LoadSprite("Sturdy");
            ResilientSprite = ResourceLoader.LoadSprite("Resilient");
            VolatileSprite = ResourceLoader.LoadSprite("Volatile");

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

                    effect = new BoolHolderSetterTriggerffect(false)
                }
            })
            .AddToDatabase();

            Glossary.CreateAndAddCustom_PassiveToGlossary("Pigment Core", "Unlocks the ability to change the color of this party member/enemy's health through a button next to their health bar.", UntetheredCoreSprite);
            Glossary.CreateAndAddCustom_PassiveToGlossary("Sturdy", "Damage received by this party member/enemy is rounded down to a certain amount.", SturdySprite);
            Glossary.CreateAndAddCustom_PassiveToGlossary("Resilient", "Damage received by this party member/enemy is capped at a certain amount per turn.", ResilientSprite);
            Glossary.CreateAndAddCustom_PassiveToGlossary("Volatile", "Upon this party member/enemy receiving any damage, deal a certain amount of indirect damage to all enemies/party members.", VolatileSprite);
        }

        internal static void Init()
        {
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

        public static BasePassiveAbilitySO ProtectedGenerator(int count)
        {
            return GetOrCreatePassive(GeneratedProtected, count, x =>
            {
                var pa = NewPassive<MultiCustomTriggerEffectPassive>($"Protected_{x}_PA", "Protected")
                    .SetBasicInformation($"Protected ({x})", StatusField.Shield.EffectInfo.icon) // TODO: add unique passive icon
                    .AutoSetDescriptions($"Permanently applies {x} shield to this ally's position.");

                var effect = new TriggerEffectAndTriggerInfo()
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
