using Scripts.Buffs;
using UnityEngine;

namespace Scripts.Passives
{
    public class BlindOnHitPassive : Passive
    {
        private BlindOnHitPassiveData data;

        public BlindOnHitPassive(Hero user, BlindOnHitPassiveData data) : base(user)
        {
            this.data = data;
            user.OnAttack += OnHeroAttack;
        }

        private void OnHeroAttack(int dmg, Hero target)
        {
            if (target == null || !target.IsAlive()) return;

            if (target.BlindDebuff == null)
                target.BlindDebuff = new Blind(target);

            for (int i = 0; i < data.StacksPerHit; i++)
                target.BlindDebuff.AddStack();

            Debug.Log($"{user.Name} applique {data.StacksPerHit} stack(s) d'aveuglement à {target.Name} !");
        }
    }
}