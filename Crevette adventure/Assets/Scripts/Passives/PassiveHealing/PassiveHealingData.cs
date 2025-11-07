using Scripts;
using Scripts.Passives;
using UnityEngine;

namespace Passives.PassiveHealing
{
    [CreateAssetMenu(menuName = "CrevetteAdventure/Passives/Healing")]
    public class PassiveHealingData : PassiveData
    {
        [field: SerializeField, Range(0, 100)]
        public float Heal { get; private set; }
        
        public override Passive CreatePassive(Hero user)
        {
            return new Scripts.Passives.PassiveHealing(user, this);
        }
    }
}