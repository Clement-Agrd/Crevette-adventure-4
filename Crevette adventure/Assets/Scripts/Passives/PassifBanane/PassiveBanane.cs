using Scripts.Buffs;
using UnityEngine;

namespace Scripts.Passives
{
    public class PassiveBanane : Passive
    {
        private PassiveBananeData data;
        private bool giveAtk = true; // commence par ATK

        public PassiveBanane(Hero user, PassiveBananeData data) : base(user)
        {
            this.data = data;
            user.OnTurnStart += OnTurnStartHandler;
        }

        private void OnTurnStartHandler(Hero h)
        {
            if (h != user) return;

            if (giveAtk)
            {
                var buff = new StatBuff(data.AtkBonus, 0, data.Duration);
                user.AddBuff(buff);
                Debug.Log($"{user.Name} reçoit un buff ATK de {data.AtkBonus} pour 1 tour(s) !");
            }
            else
            {
                var buff = new StatBuff(0, data.DefBonus, data.Duration);
                user.AddBuff(buff);
                Debug.Log($"{user.Name} reçoit un buff DEF de {data.DefBonus} pour 1 tour(s) !");
            }

            giveAtk = !giveAtk; // alterne pour le prochain tour
        }
    }
}