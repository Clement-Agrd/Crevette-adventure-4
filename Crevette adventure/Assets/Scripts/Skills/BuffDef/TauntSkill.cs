using Scripts;
using Scripts.Skills;
using UnityEngine;

namespace Skills.Taunt
{
    public class TauntSkill : Skill
    {

        public TauntSkill(SkillData data, Hero user) : base(data, user)
        {
            
        }

        public override void Use(BattleSystem system)
        {
            if (!CanUse(system)) return;
            int bonusDef;
            bonusDef = SkillData.Damage; // on réutilise Damage pour la valeur du buff
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
        
        public override void Use(BattleSystem system, Hero target)
        {
            Use(system); // pas besoin de cible
        }
    }
}