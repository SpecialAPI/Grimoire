using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.StatusEffect.EffectTypes
{
    public class InsightStatusEffect : StatusEffect_SO
    {
        public override bool IsPositive => false;

        public override void OnTriggerAttached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            var insightData = new InsightUnitData();
            holder.m_ObjectData = insightData;

            if (caller is ITurn t)
            {
                insightData.unit = t;
                insightData.abilities.AddRange(t.GetNextAbilitySlotUsage());

                if (insightData.abilities.Count <= 0)
                    insightData.abilities.Add(-1);

                insightData.abilities.Sort(insightData.AbilityComparison);
            }

            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, CustomTriggers.ModifyUsedEnemyAbilities, caller);
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_02, TriggerCalls.OnTurnFinished.ToString(), caller);
        }

        public override void OnTriggerDettached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, CustomTriggers.ModifyUsedEnemyAbilities, caller);
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_02, TriggerCalls.OnTurnFinished.ToString(), caller);
        }

        public override string DisplayText(StatusEffect_Holder holder)
        {
            var duration = base.DisplayText(holder);

            if (holder.m_ObjectData is not InsightUnitData dat || dat.unit is not EnemyCombat ec || dat.abilities == null || dat.abilities.Count <= 0)
                return duration;

            if (ec.Abilities == null || ec.Abilities.Count <= 0)
                return $"None ({duration})";

            var abNames = new List<string>();
            foreach (var ab in dat.abilities)
            {
                if (ab < 0 || ab >= ec.Abilities.Count)
                    continue;

                abNames.Add(ec.Abilities[ab].ability.GetAbilityLocData().text);
            }

            var abString = abNames.Count <= 0 ? "None" : string.Join(", ", abNames);
            return $"{abString} ({duration})";
        }

        public override void OnEventCall_01(StatusEffect_Holder holder, object sender, object args)
        {
            if (sender is not ITurn t || sender is not IStatusEffector e)
                return;

            if (args is not ModifyUsedEnemyAbilitiesReference absRef)
                return;

            if (holder.m_ObjectData is not InsightUnitData dat || dat.abilities == null || dat.abilities.Count <= 0)
                return;

            var abs = absRef.usedAbilityIDs;
            var oldAbs = abs.ToList();

            abs.Clear();
            abs.AddRange(dat.abilities);

            dat.abilities.Clear();
            dat.abilities.AddRange(oldAbs);

            var prevContent = holder.m_ContentMain;

            if (CanReduceDuration)
            {
                holder.m_ContentMain = Mathf.Max(0, prevContent - 1);

                if (TryRemoveStatusEffect(holder, e))
                    return;
            }

            dat.abilities.Sort(dat.AbilityComparison);
            e.StatusEffectValuesChanged(StatusID, holder.m_ContentMain - prevContent);
        }

        public override void OnEventCall_02(StatusEffect_Holder holder, object sender, object args)
        {
            if (args is not IUnit u || args is not IStatusEffector e || !u.IsUnitCharacter)
                return;

            ReduceDuration(holder, e);
        }
    }

    public class InsightUnitData
    {
        public ITurn unit;
        public List<int> abilities = [];

        public int AbilityComparison(int ab1, int ab2)
        {
            if (unit == null)
                return 0;

            if (ab1 < 0 && ab2 < 0)
                return 0;

            if(ab1 < 0)
                return -1;

            if(ab2 < 0)
                return 1;

            var pr1 = unit.GetTurnPriorityWithAbility(ab1);
            var pr2 = unit.GetTurnPriorityWithAbility(ab2);

            return -pr1.CompareTo(pr2);
        }
    }
}
