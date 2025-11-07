using Scripts.Passives;
using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(menuName = "CrevetteAdventure/Passives/Thorn")]
    public class ThornPassiveData : PassiveData
    {
        [field: SerializeField, Range(0, 1)]
        public float ReturnDamage { get; private set; }
        
        public override Passive CreatePassive(Hero user)
        {
            return new ThornPassive(user, this);
        }
    }
}