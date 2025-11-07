using Scripts;
using Scripts.Skills;
using UnityEngine;

namespace Skills.Estoc
{
    [CreateAssetMenu(menuName = "AC/Skills/Estoc")]
    public class EstocData : SkillData
    {
        [field: SerializeField]
        public int Damage { get; private set; }
        public override ISkill CreateSkill(Hero user)
        {
            throw new System.NotImplementedException();
        }
    }
}