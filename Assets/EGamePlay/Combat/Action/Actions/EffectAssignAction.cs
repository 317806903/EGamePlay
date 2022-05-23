using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using EGamePlay.Combat;

namespace EGamePlay.Combat
{
    public class EffectAssignAbility : ActionAbility<EffectAssignAction>
    {

    }

    /// <summary>
    /// ����Ч���ж�
    /// </summary>
    public class EffectAssignAction : ActionExecution
    {
        //�������Ч�������ж���Դ����
        public AbilityEntity SourceAbility { get; set; }
        public Effect EffectConfig => AbilityEffect.EffectConfig;


        //ǰ�ô���
        private void PreProcess()
        {

        }

        public void ApplyEffectAssign()
        {
            //Log.Debug($"ApplyEffectAssign {EffectConfig}");
            PreProcess();

            if (EffectConfig is DamageEffect) TryAssignDamage();
            if (EffectConfig is CureEffect && Target.CurrentHealth.IsFull() == false) TryAssignCure();
            if (EffectConfig is AddStatusEffect) TryAssignAddStatus();

            PostProcess();

            ApplyAction();
        }

        private void FillDatasToAction(ActionExecution action)
        {
            action.Target = Target;
            action.AbilityEffect = AbilityEffect;
            action.ExecutionEffect = ExecutionEffect;
            action.AbilityExecution = AbilityExecution;
            action.AbilityItem = AbilityItem;
        }

        private void TryAssignDamage()
        {
            if (OwnerEntity.DamageAbility.TryMakeAction(out var damageAction))
            {
                FillDatasToAction(damageAction);
                damageAction.DamageSource = DamageSource.Skill;
                damageAction.ApplyDamage();
            }
        }

        private void TryAssignCure()
        {
            if (OwnerEntity.CureAbility.TryMakeAction(out var cureAction))
            {
                FillDatasToAction(cureAction);
                cureAction.ApplyCure();
            }
        }

        private void TryAssignAddStatus()
        {
            if (OwnerEntity.AddStatusAbility.TryMakeAction(out var addStatusAction))
            {
                FillDatasToAction(addStatusAction);
                addStatusAction.SourceAbility = SourceAbility;
                addStatusAction.ApplyAddStatus();
            }
        }

        //���ô���
        private void PostProcess()
        {
            Creator.TriggerActionPoint(ActionPointType.AssignEffect, this);
            Target.TriggerActionPoint(ActionPointType.ReceiveEffect, this);
        }
    }
}