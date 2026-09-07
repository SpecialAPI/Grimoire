using Grimoire.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grimoire.FieldEffect
{
    public class MistFE_SO : FieldEffect_SO
    {
        public override void OnSlotEffectorTriggerAttached(FieldEffect_Holder holder)
        {
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, TriggerCalls.OnTurnFinished.ToString(), holder.Effector);
        }

        public override void OnSlotEffectorTriggerDettached(FieldEffect_Holder holder)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, TriggerCalls.OnTurnFinished.ToString(), holder.Effector);
        }

        public override void OnTriggerAttached(FieldEffect_Holder holder, IUnit caller)
        {
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_02, TriggerCalls.CanTurnShowInTimeline.ToString(), caller);

            UpdateEnemyHiddenTurnsCombatAction.UpdateForUnit(caller);
            CombatManager.Instance.AddSubAction(new UpdateEnemyHiddenTurnsCombatAction(caller));
        }

        public override void OnTriggerDettached(FieldEffect_Holder holder, IUnit caller)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_02, TriggerCalls.CanTurnShowInTimeline.ToString(), caller);

            CombatManager.Instance.AddSubAction(new UpdateEnemyHiddenTurnsCombatAction(caller));
        }

        public override void OnEventCall_01(FieldEffect_Holder holder, object sender, object args)
        {
            ReduceDuration(holder);
        }

        public override void OnEventCall_02(FieldEffect_Holder holder, object sender, object args)
        {
            if (args is not BooleanReference boolRef)
                return;

            boolRef.value = false;
        }
    }
}
