using UnityEngine;
using Scripts.Passives;

namespace Scripts.Passives
{
    [CreateAssetMenu(menuName = "CrevetteAdventure/Passives/StackDamage")]
    public class StackDamagePassiveData : PassiveData
    {
        [field: SerializeField, Range(0f, 1f)]
        public float StackMultiplier { get; private set; } = 0.1f; // 10% par stack par défaut

        public override Passive CreatePassive(Hero user)
        {
            return new StackDamagePassive(user, this);
        }
    }
}