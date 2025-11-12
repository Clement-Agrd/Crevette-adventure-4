using UnityEngine;

namespace Scripts.Passives
{
    [CreateAssetMenu(menuName = "CrevetteAdventure/Passives/Camouflage Passive", order = 0)]
    public class CamouflagePassiveData : PassiveData
    {
        [Header("Durée du camouflage en tours")]
        public int Duration = 2;

        public override Passive CreatePassive(Hero hero)
        {
            return new CamouflagePassive(hero, this);
        }
    }
}