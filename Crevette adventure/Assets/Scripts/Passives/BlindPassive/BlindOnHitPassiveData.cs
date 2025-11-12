using UnityEngine;

namespace Scripts.Passives
{
    [CreateAssetMenu(menuName = "CrevetteAdventure/Passives/Blind On Hit", order = 0)]
    public class BlindOnHitPassiveData : PassiveData
    {
        [Header("Nombre de stacks appliqués par attaque")]
        public int StacksPerHit = 1;

        public override Passive CreatePassive(Hero hero)
        {
            return new BlindOnHitPassive(hero, this);
        }
    }
}