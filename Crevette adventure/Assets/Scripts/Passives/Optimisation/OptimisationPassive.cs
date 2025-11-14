using System;
using Scripts.Passives;
using UnityEngine;

namespace Scripts.UltiChargeOnHeal
{
    public class UltiChargeOnHealPassive : Passive
    {
        private UltiChargeOnHealPassiveData data;

        public UltiChargeOnHealPassive(Hero user, UltiChargeOnHealPassiveData data) : base(user)
        {
            this.data = data;
            user.OnHealedally+= HandleHealDealt;
        }

        private void HandleHealDealt(int amount, Hero healedHero)
        {
            if (healedHero == null || !healedHero.IsAlive()) return;
            
            Debug.Log($"{healedHero.Name} gagne 1 charge d'ulti grâce au soin de {user.Name} !");
        }
        
    }
}