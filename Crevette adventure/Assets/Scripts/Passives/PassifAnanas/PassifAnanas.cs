using UnityEngine;

namespace Scripts.Passives
{
    public class PassifAnanas : Passive
    {

        private PassifAnanasData data;
        private bool firstTrigger = true;

        public PassifAnanas(Hero user, PassifAnanasData data) : base(user)
        {
            this.data = data;
            user.OnTurnStart += OnTurnStart;
        }
        

        private void OnTurnStart(Hero hero)
        {
            int bonusDef;
            bonusDef = PassifAnanasData.BonusDefData; // on réutilise Damage pour la valeur du buff
            user.Taunt();
            // Applique le buff immédiatement
            user.Def += bonusDef;
            Debug.Log($"{user.Name} augmente sa défense de {bonusDef} jusqu'au prochain tour !");

            // Écoute le prochain tour pour retirer le buff
            void OnNextTurn(Hero h)
            {
                if (h == user)
                {
                    user.Def -= bonusDef;
                    Debug.Log($"{user.Name} perd le bonus de défense de {bonusDef}");
                    user.OnTurnStart -= OnNextTurn; // désinscrit l'événement
                }
            }

            user.OnTurnStart += OnNextTurn;
        }
    }
}