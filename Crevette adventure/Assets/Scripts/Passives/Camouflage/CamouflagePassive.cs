using UnityEngine;

namespace Scripts.Passives
{
    public class CamouflagePassive : Passive
    {
        private CamouflagePassiveData data;
        private bool firstTrigger = true;

        public CamouflagePassive(Hero user, CamouflagePassiveData data) : base(user)
        {
            this.data = data;
            user.OnTurnStart += OnTurnStart;
        }

        private void OnTurnStart(Hero hero)
        {
            if (!firstTrigger) return;
            firstTrigger = false;

            hero.ApplyCamouflage(data.Duration);
            Debug.Log($"{hero.Name} commence le combat camouflé pour {data.Duration} tours !");
        }
    }
}