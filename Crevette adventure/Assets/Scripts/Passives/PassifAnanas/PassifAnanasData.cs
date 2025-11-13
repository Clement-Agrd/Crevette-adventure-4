using UnityEngine;

namespace Scripts.Passives
{
    [CreateAssetMenu(menuName = "CrevetteAdventure/Passives/PassiveAnanas", order = 0)]
    public class PassifAnanasData : PassiveData
    {
        [Header("Durée du camouflage en tours")]
        public static int BonusDefData = 10;

        public override Passive CreatePassive(Hero hero)
        {
            return new PassifAnanas(hero, this);
        }
    }
}