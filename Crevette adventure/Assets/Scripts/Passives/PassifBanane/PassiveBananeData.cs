using UnityEngine;

namespace Scripts.Passives
{
    [CreateAssetMenu(menuName = "CrevetteAdventure/Passives/Alternating ATK/DEF", order = 0)]
    public class PassiveBananeData : PassiveData
    {
        [Header("Valeur des buffs")]
        public int AtkBonus = 10;
        public int DefBonus = 10;

        [Header("Durée des buffs en tours")]
        public int Duration = 2;

        public override Passive CreatePassive(Hero hero)
        {
            return new PassiveBanane(hero, this);
        }
    }
}