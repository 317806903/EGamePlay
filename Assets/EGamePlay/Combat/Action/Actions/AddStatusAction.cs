using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using EGamePlay.Combat;

namespace EGamePlay.Combat
{
    public class AddStatusActionAbility : EffectActionAbility<AddStatusAction>
    {

    }

    /// <summary>
    /// ʩ��״̬�ж�
    /// </summary>
    public class AddStatusAction : ActionExecution<AddStatusActionAbility>
    {
        public AbilityEntity SourceAbility { get; set; }
        public AddStatusEffect AddStatusEffect => AbilityEffect.EffectConfig as AddStatusEffect;
        public StatusAbility Status { get; set; }


        //ǰ�ô���
        private void PreProcess()
        {

        }

        public void ApplyAddStatus()
        {
            PreProcess();

            var statusConfig = AddStatusEffect.AddStatus;
            if (statusConfig.CanStack == false)
            {
                if (Target.HasStatus(statusConfig.ID))
                {
                    var status = Target.GetStatus(statusConfig.ID);
                    var statusLifeTimer = status.GetComponent<StatusLifeTimeComponent>().LifeTimer;
                    statusLifeTimer.MaxTime = AddStatusEffect.Duration / 1000f;
                    statusLifeTimer.Reset();
                    return;
                }
            }

            Status = Target.AttachStatus<StatusAbility>(statusConfig);
            Status.OwnerEntity = Creator;
            Status.Level = SourceAbility.Level;
            //Log.Debug($"ApplyEffectAssign AddStatusEffect {Status}");

            if (statusConfig.EnabledLogicTrigger)
            {
                Status.ProccessInputKVParams(AddStatusEffect.Params);
            }

            Status.AddComponent<StatusLifeTimeComponent>();
            Status.TryActivateAbility();

            PostProcess();

            ApplyAction();
        }

        //���ô���
        private void PostProcess()
        {
            Creator.TriggerActionPoint(ActionPointType.PostGiveStatus, this);
            Target.TriggerActionPoint(ActionPointType.PostReceiveStatus, this);
        }
    }
}