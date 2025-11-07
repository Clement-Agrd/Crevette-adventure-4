using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(menuName = "CrevetteAdventure/Hero", order = 0)]
    public class HeroData : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }
        [field: SerializeField]
        public int MaxHealth { get; private set; }
        [field: SerializeField]
        public int Atk { get; private set; }
        [field: SerializeField]
        public int Def { get; private set; }
        [field: SerializeField]
        public int Speed { get; private set; }
        [field: SerializeField]
        public bool IsEnemy { get; private set; }
        
        [field: SerializeField]
        public PassiveData Passive { get; private set; }
        
        [field: SerializeField]
        public List<SkillData> Skills { get; private set; }
        

        public Hero CreateHero()
        {
            return new Hero(this);
        }
    }
}