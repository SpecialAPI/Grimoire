using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UIElements.UIR;

namespace Grimoire.Content.Effect
{
    public class RandomizeAndChangeCostAmountEffect : EffectSO
    {
        public List<ManaColorSO> pool;
        public bool increase;
        public int minAmount;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (pool == null || pool.Count <= 0)
                return false;

            foreach(var t in targets)
            {
                if(t == null || !t.HasUnit)
                    continue;

                var u = t.Unit;

                if(!u.IsUnitCharacter)
                    continue;

                var abs = u.Abilities();
                foreach (var ab in abs)
                {
                    if (ab == null)
                        continue;

                    var min = Mathf.Min(minAmount, ab.cost.Length);
                    var change = increase ? entryVariable : -entryVariable;
                    var amt = Mathf.Max(ab.cost.Length + change, min, 0);

                    var newCost = new ManaColorSO[amt];

                    for (var i = 0; i < newCost.Length; i++)
                        newCost[i] = pool[Random.Range(0, pool.Count)];

                    ab.cost = newCost;
                    exitAmount += newCost.Length;
                }

                CombatManager.Instance.AddUIAction(new CharacterUpdateAllAttacksUIAction(u.ID, [.. abs]));
            }

            return exitAmount > 0;
        }
    }
}
