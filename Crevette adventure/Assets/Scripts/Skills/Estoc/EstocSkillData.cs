using Scripts;
using Scripts.Skills;
using UnityEngine;

namespace Skills.Estoc
{
    [CreateAssetMenu(menuName = "CrevetteAdventure/Skills/Estoc")]
    public class EstocSkillData : SkillData
    {
        [field: SerializeField]
        public int Damage { get; private set; }
        public override ISkill CreateSkill(Hero user)
        {
            return new EstocSkill(this, user);
        }
    }
}