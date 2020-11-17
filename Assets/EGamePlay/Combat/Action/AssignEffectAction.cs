using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using EGamePlay.Combat.Status;

namespace EGamePlay.Combat
{
    /// <summary>
    /// ����Ч���ж�
    /// </summary>
    public class AssignEffectAction : CombatAction
    {
        public Effect Effect { get; set; }
        //Ч������
        public EffectType EffectType { get; set; }
        //Ч����ֵ
        public string EffectValue { get; set; }


        private void BeforeAssign()
        {

        }

        public void ApplyAssignEffect()
        {
            BeforeAssign();
            if (Effect is DamageEffect damageEffect)
            {

            }
            if (Effect is AddStatusEffect addStatusEffect)
            {
                StatusAbilityEntity status = EntityFactory.CreateWithParent<StatusAbilityEntity>(Target, addStatusEffect.AddStatus);
                status.Caster = Creator;
                status.AddComponent<StatusLifeTimeComponent>();
                status.TryActivateAbility();
            }
            AfterAssign();
        }

        private void AfterAssign()
        {

        }
    }

    public enum EffectType
    {
        DamageAffect = 1,
        NumericModify = 2,
        StatusAttach = 3,
        BuffAttach = 4,
    }
}