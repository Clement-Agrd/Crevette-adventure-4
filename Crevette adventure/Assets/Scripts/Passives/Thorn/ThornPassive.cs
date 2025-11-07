using UnityEngine;

namespace Scripts.Passives
{
    public class ThornPassive: Passive
    {
        private ThornPassiveData data;

        public ThornPassive(Hero user, ThornPassiveData data) : base(user)
        {
            this.data = data;
            user.OnDamaged += HeroOnDamaged;
        }

        private void HeroOnDamaged(int dmg, Hero hero)
        {
            int returnDmg = Mathf.CeilToInt(data.ReturnDamage * hero.Def);
            hero.TakeDamage(returnDmg, hero);
        }
    }
}