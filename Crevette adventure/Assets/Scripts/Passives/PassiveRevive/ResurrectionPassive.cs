using UnityEngine;

namespace Scripts.Passives
{
    public class ResurrectionPassive : Passive
    {
        private ResurrectionPassiveData data;
        private bool hasRevived = false;

        public ResurrectionPassive(Hero user, ResurrectionPassiveData data) : base(user)
        {
            this.data = data;
            user.OnDamaged += OnDamagedHandler;
        }

        private void OnDamagedHandler(int dmg, Hero from)
        {
            if (hasRevived) return;

            if (user.CurrentHealth <= 0)
            {
                hasRevived = true;
                int reviveHp = Mathf.CeilToInt(user.MaxHealth * data.RevivePercent / 100f);
                user.CurrentHealth = reviveHp;
                Debug.Log($"{user.Name} ressuscite avec {reviveHp}/{user.MaxHealth} PV grâce à son passif !");
            }
        }
    }
}