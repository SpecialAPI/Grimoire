using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.Effect
{
    public class AddCostByHealthColorEffect : EffectSO
    {
        public int limit = -1;
        public List<string> ignoredAbilities;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (entryVariable <= 0)
                return false;

            foreach(var t in targets)
            {
                if(t == null || !t.HasUnit)
                    continue;

                var u = t.Unit;

                if(!u.IsUnitCharacter)
                    continue;

                var abs = u.Abilities();
                var successful = false;

                foreach(var ab in abs)
                {
                    if (ab == null || ab.cost == null)
                        continue;

                    if(ignoredAbilities != null && ignoredAbilities.Count > 0)
                    {
                        if(ab.ability != null && !string.IsNullOrEmpty(ab.ability.name) && ignoredAbilities.Contains(ab.ability.name))
                            continue;
                    }

                    if(limit >= 0)
                    {
                        if(ab.cost.Length >= limit)
                            continue;
                    }

                    var toAdd = entryVariable;

                    if (limit >= 0)
                        toAdd = Mathf.Min(limit - ab.cost.Length, toAdd);

                    ab.cost = [.. ab.cost, .. new ManaColorSO[toAdd].Populate(caster.HealthColor)];

                    exitAmount += toAdd;
                    successful = true;
                }

                if(successful)
                    CombatManager.Instance.AddUIAction(new CharacterUpdateAllAttacksUIAction(u.ID, [.. abs]));
            }

            return exitAmount > 0;
        }
    }
}
