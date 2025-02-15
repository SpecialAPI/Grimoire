using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Grimoire.Content.StatusEffect.EffectTypes
{
    public class WeakenedStatusEffect : StatusEffect_SO
    {
        public override bool IsPositive => false;

        public override void OnTriggerAttached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            holder.m_ObjectData = caller;

            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, CustomTriggers.ModifyAbilityRank, caller);
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_02, TriggerCalls.OnWillApplyDamage.ToString(), caller);
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_03, TriggerCalls.OnTurnFinished.ToString(), caller);

            if (caller is CharacterCombat cc)
                cc.SetUpDefaultAbilities(true);
        }

        public override void OnTriggerDettached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, CustomTriggers.ModifyAbilityRank, caller);
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_02, TriggerCalls.OnWillApplyDamage.ToString(), caller);
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_03, TriggerCalls.OnTurnFinished.ToString(), caller);

            if (caller is CharacterCombat cc)
                cc.SetUpDefaultAbilities(true);
        }

        public override void OnEventCall_01(StatusEffect_Holder holder, object sender, object args)
        {
            if (args is not IntegerReference intRef)
                return;

            var totalAmount = holder.StatusContent + holder.Restrictor;

            if (totalAmount <= 0)
                return;

            intRef.value -= totalAmount;
        }

        public override void OnEventCall_02(StatusEffect_Holder holder, object sender, object args)
        {
            if (args is not DamageDealtValueChangeException ex)
                return;

            if (sender is not IUnit unit || unit.IsUnitCharacter)
                return;

            var totalAmount = holder.StatusContent + holder.Restrictor;

            if (totalAmount <= 0)
                return;

            var mult = 1f / (1f + 0.25f * totalAmount);
            var reduction = 1f - mult;

            ex.AddModifier(new PercentageValueModifier(true, Mathf.RoundToInt(100f * reduction), false));
        }

        public override bool TryAddContent(StatusEffect_Holder holder, int content, int restrictor)
        {
            var res = base.TryAddContent(holder, content, restrictor);

            if (res && (content != 0 || restrictor != 0) && holder.m_ObjectData is CharacterCombat cc)
                cc.SetUpDefaultAbilities(true);

            return res;
        }

        public override bool TryIncreaseContent(StatusEffect_Holder holder, int amount)
        {
            if (!base.TryIncreaseContent(holder, amount))
                return false;

            if (amount != 0 && holder.m_ObjectData is CharacterCombat cc)
                cc.SetUpDefaultAbilities(true);

            return true;
        }

        public override int JustRemoveAllContent(StatusEffect_Holder holder)
        {
            if (holder.StatusContent == 0)
                return base.JustRemoveAllContent(holder);

            var res = base.JustRemoveAllContent(holder);

            if (holder.m_ObjectData is CharacterCombat cc)
                cc.SetUpDefaultAbilities(true);

            return res;
        }

        public override void ReduceDuration(StatusEffect_Holder holder, IStatusEffector effector)
        {
            if (!CanReduceDuration)
                return;

            var oldContent = holder.StatusContent;
            holder.m_ContentMain = Mathf.Max(0, oldContent - 1);

            if (!TryRemoveStatusEffect(holder, effector) && oldContent != holder.StatusContent)
            {
                effector.StatusEffectValuesChanged(_StatusID, holder.StatusContent - oldContent);

                if (holder.m_ObjectData is CharacterCombat cc)
                    cc.SetUpDefaultAbilities(true);
            }
        }

        public override void OnEventCall_03(StatusEffect_Holder holder, object sender, object args)
        {
            if (sender is not IStatusEffector effector)
                return;

            ReduceDuration(holder, effector);
        }
    }
}
