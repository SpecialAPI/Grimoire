using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.Content.FieldEffects.EffectTypes
{
    public class ThunderstormFieldEffect : FieldEffect_SO
    {
        public override bool IsPositive => true;

        public override bool CanBeRemoved(FieldEffect_Holder holder)
        {
            return true;
        }

        public override void OnTriggerAttached(FieldEffect_Holder holder, IUnit caller)
        {
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, TriggerCalls.OnWillApplyDamage.ToString(), caller);
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_02, TriggerCalls.OnBeingDamaged.ToString(), caller);
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_04, TriggerCalls.OnDeath.ToString(), caller);
        }

        public override void OnTriggerDettached(FieldEffect_Holder holder, IUnit caller)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, TriggerCalls.OnWillApplyDamage.ToString(), caller);
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_02, TriggerCalls.OnBeingDamaged.ToString(), caller);
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_04, TriggerCalls.OnDeath.ToString(), caller);
        }

        public override void OnSlotEffectorTriggerAttached(FieldEffect_Holder holder)
        {
            IntegerReference intRef = new(0);
            CombatManager.Instance.PostNotification("OnApplyThunderstorm", null, intRef);

            if (intRef.value > 0)
            {
                int oldContent = holder.m_ContentMain;

                holder.m_ContentMain += intRef.value;
                holder.Effector.FieldEffectValuesChanged(_FieldID, false, holder.m_ContentMain - oldContent);
            }

            CombatManager.Instance.AddObserver(holder.OnEventTriggered_03, "OnApplyThunderstorm");
        }

        public override void OnSlotEffectorTriggerDettached(FieldEffect_Holder holder)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_03, "OnApplyThunderstorm");
        }

        public override void OnEventCall_01(FieldEffect_Holder holder, object sender, object args)
        {
            if (args is not DamageDealtValueChangeException context)
                return;

            context.AddModifier(new ThunderstormValueModifier(true, holder.m_ContentMain));
        }

        public override void OnEventCall_02(FieldEffect_Holder holder, object sender, object args)
        {
            if (args is not DamageReceivedValueChangeException context || !context.directDamage)
                return;

            context.AddModifier(new ThunderstormValueModifier(false, holder.m_ContentMain));
        }

        public override void OnEventCall_03(FieldEffect_Holder holder, object sender, object args)
        {
            if (args is IntegerReference intRef)
                intRef.value += holder.m_ContentMain;

            holder.Effector.RemoveFieldEffect(_FieldID);
        }
        public override void OnEventCall_04(FieldEffect_Holder holder, object sender, object args)
        {
            if (args is not DeathReference deathRef || !deathRef.HasKiller)
                return;

            CombatManager.Instance.AddSubAction(new TransferThunderstormAction(deathRef.killer.ID, deathRef.killer.IsUnitCharacter, this, holder.Effector, holder.m_ContentMain));
        }
    }

    public class ThunderstormValueModifier(bool dmgDealt, int toStorm) : IntValueModifier(dmgDealt ? 19 : 71)
    {
        public override int Modify(int value)
        {
            if (!dmgDealt && value <= 0)
                return value;

            return value + toStorm;
        }
    }

    public class TransferThunderstormAction(int id, bool character, FieldEffect_SO fieldEffect, IFieldSlotEffector effector, int amount) : CombatAction
    {
        public override IEnumerator Execute(CombatStats stats)
        {
            IUnit unit = character ?
              stats.TryGetCharacterOnField(id) :
              stats.TryGetEnemyOnField(id);

            if (unit == null || !unit.IsAlive)
                yield break;

            var slot = Random.Range(unit.SlotID, unit.SlotID + unit.Size);
            effector.RemoveFieldEffect(fieldEffect._FieldID);
            stats.combatSlots.ApplyFieldEffect(slot, character, fieldEffect, amount);
        }
    }
}
