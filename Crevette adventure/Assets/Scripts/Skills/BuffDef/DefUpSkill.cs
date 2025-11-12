using Scripts;
using Scripts.Skills;
using UnityEngine;

namespace Skills.DefUp
{
    public class DefUpSkill : Skill
    {
        private int bonusDef;

        public DefUpSkill(SkillData data, Hero user) : base(data, user)
        {
            bonusDef = SkillData.Damage; // on réutilise Damage pour la valeur du buff
        }

        public override void Use(BattleSystem system)
        {
            if (!CanUse(system)) return;
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

        public override void Use(BattleSystem system, Hero target)
        {
            // pas besoin d'une cible
            Use(system);
        }
    }
}