using Scripts.Skills;
using UnityEngine;
namespace Scripts
{
    public abstract class SkillData : ScriptableObject
    {
        [field: SerializeField]
        public string Title { get; private set; }
        [field:  SerializeField]
        public string Description { get; private set; }
        [field:  SerializeField]
        public int Damage { get; private set; }
        [field:  SerializeField]
        public bool TargetEnemy { get; private set; }
        
        public abstract ISkill CreateSkill(Hero user);
    }
    
}