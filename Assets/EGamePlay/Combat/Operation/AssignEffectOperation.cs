using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace EGamePlay.Combat
{
    /// <summary>
    /// ����Ч���ж�
    /// </summary>
    public class AssignEffectOperation : CombatOperation
    {
        //Ч������
        public EffectType EffectType { get; set; }
        //Ч����ֵ
        public string EffectValue { get; set; }


        private void BeforeAssign()
        {

        }

        public void ApplyAssignOperation()
        {
            BeforeAssign();

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