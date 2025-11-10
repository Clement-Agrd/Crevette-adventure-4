using Passives.PassiveHealing;
using UnityEngine;

namespace Scripts.Passives
{
    public class PassiveHealing : Passive
    {
        private PassiveHealingData data;

        public PassiveHealing(Hero user, PassiveHealingData data) : base(user)
        {
            this.data = data;
            user.OnTurnStart += HeroOnStartTurn;
        }


        private void HeroOnStartTurn(Hero hero)
        {
            int SelfHealing = Mathf.CeilToInt(data.Heal);
            hero.Heal(SelfHealing, hero);
        }
    }
}