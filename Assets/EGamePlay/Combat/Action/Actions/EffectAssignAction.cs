using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using EGamePlay.Combat;

namespace EGamePlay.Combat
{
    public class EffectAssignAbility : Entity, IActionAbility
    {
        public CombatEntity OwnerEntity { get { return GetParent<CombatEntity>(); } set { } }
        public CombatEntity ParentEntity { get => GetParent<CombatEntity>(); }
        public bool Enable { get; set; }


        public bool TryMakeAction(out EffectAssignAction action)
        {
            if (Enable == false)
            {
                action = null;
            }
            else
            {
                action = OwnerEntity.AddChild<EffectAssignAction>();
                action.ActionAbility = this;
                action.Creator = OwnerEntity;
            }
            return Enable;
        }

        //public void TryActivateAbility() => ActivateAbility();

        //public void ActivateAbility() => Enable = true;

        //public void DeactivateAbility() { }

        //public void EndAbility() { }

        //public Entity CreateExecution()
        //{
        //    var execution = OwnerEntity.MakeAction<EffectAssignAction>();
        //    execution.ActionAbility = this;
        //    return execution;
        //}

        //public bool TryMakeAction(out EffectAssignAction abilityExecution)
        //{
        //    if (Enable == false)
        //    {
        //        abilityExecution = null;
        //    }
        //    else
        //    {
        //        abilityExecution = CreateExecution() as EffectAssignAction;
        //    }
        //    return Enable;
        //}
    }

    /// <summary>
    /// ����Ч���ж�
    /// </summary>
    public class EffectAssignAction : Entity, IActionExecution
    {
        /// �������Ч�������ж���Դ����
        public Entity SourceAbility { get; set; }
        /// Ŀ���ж�
        public IActionExecution TargetAction { get; set; }
        public AbilityEffect AbilityEffect { get; set; }
        public AbilityItem AbilityItem { get; set; }
        public Effect EffectConfig => AbilityEffect.EffectConfig;
        /// �ж�����
        public Entity ActionAbility { get; set; }
        /// Ч�������ж�Դ
        public EffectAssignAction SourceAssignAction { get; set; }
        /// �ж�ʵ��
        public CombatEntity Creator { get; set; }
        /// Ŀ�����
        public CombatEntity Target { get; set; }


        /// ǰ�ô���
        private void PreProcess()
        {

        }

        public void ApplyEffectAssign()
        {
            //Log.Debug($"ApplyEffectAssign {EffectConfig}");
            PreProcess();

            AbilityEffect.StartAssignEffect(this);
            //if (AbilityEffect.HasComponent<EffectDamageComponent>()) TryAssignDamage();
            //if (AbilityEffect.HasComponent<EffectCureComponent>() && Target.CurrentHealth.IsFull() == false) TryAssignCure();
            //if (AbilityEffect.HasComponent<EffectAddStatusComponent>()) TryAssignAddStatus();

            PostProcess();

            FinishAction();
        }

        public void FillDatasToAction(IActionExecution action)
        {
            action.SourceAssignAction = this;
            action.Target = Target;
            //action.AbilityEffect = AbilityEffect;
            //action.ExecutionEffect = ExecutionEffect;
            //action.AbilityExecution = AbilityExecution;
            //action.AbilityItem = AbilityItem;
        }

        //private void TryAssignDamage()
        //{
        //    if (OwnerEntity.DamageAbility.TryMakeAction(out var damageAction))
        //    {
        //        FillDatasToAction(damageAction);
        //        damageAction.DamageSource = DamageSource.Skill;
        //        damageAction.ApplyDamage();
        //    }
        //}

        //private void TryAssignCure()
        //{
        //    if (OwnerEntity.CureAbility.TryMakeAction(out var cureAction))
        //    {
        //        FillDatasToAction(cureAction);
        //        cureAction.ApplyCure();
        //    }
        //}

        //private void TryAssignAddStatus()
        //{
        //    if (OwnerEntity.AddStatusAbility.TryMakeAction(out var addStatusAction))
        //    {
        //        FillDatasToAction(addStatusAction);
        //        addStatusAction.SourceAbility = SourceAbility;
        //        addStatusAction.ApplyAddStatus();
        //    }
        //}

        /// ���ô���
        private void PostProcess()
        {
            Creator.TriggerActionPoint(ActionPointType.AssignEffect, this);
            Target.TriggerActionPoint(ActionPointType.ReceiveEffect, this);
        }

        public void FinishAction()
        {
            Entity.Destroy(this);
        }
    }
}