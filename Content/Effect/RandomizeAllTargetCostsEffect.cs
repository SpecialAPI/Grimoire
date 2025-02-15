using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.Effect
{
    public class RandomizeAllTargetCostsEffect : EffectSO
    {
        public List<ManaColorSO> pool;

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
                foreach(var ab in abs)
                {
                    if(ab == null)
                        continue;

                    var newCost = new ManaColorSO[ab.cost.Length];

                    for(var i = 0; i < newCost.Length; i++)
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
