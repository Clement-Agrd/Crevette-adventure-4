using UnityEngine;

namespace Scripts.Passives
{
    [CreateAssetMenu(menuName = "CrevetteAdventure/Passives/Resurrection Mid HP", order = 0)]
    public class ResurrectionPassiveData : PassiveData
    {
        [Header("Pourcentage de PV lors de la résurrection")]
        [Range(1, 100)]
        public int RevivePercent = 50;

        public override Passive CreatePassive(Hero hero)
        {
            return new ResurrectionPassive(hero, this);
        }
    }
}