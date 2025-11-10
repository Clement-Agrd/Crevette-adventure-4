using Scripts.Skills;
using UnityEngine;
namespace Scripts
{
    [CreateAssetMenu(menuName = "CrevetteAdventure/Skill", order = 0)]
    public class SkillData : ScriptableObject
    {
        [field: SerializeField]
        public string Title { get; private set; }
        public SkillEnum skillType;
        
        [field:  SerializeField]
        public string Description { get; private set; }
        [field: SerializeField]

        public bool TargetEnemy { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        
    }
    
}