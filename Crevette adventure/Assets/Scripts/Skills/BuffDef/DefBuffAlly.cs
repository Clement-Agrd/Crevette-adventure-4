using Scripts;
using Scripts.Skills;
using UnityEngine;

namespace Skills.DefBuffAlly
{
    public class DefBuffAlly : Skill
    {
        public DefBuffAlly(SkillData data, Hero user) : base(data, user) { }

        public override void Use(BattleSystem system, Hero target)
        {
            if (!CanUse()) return;
            if (target == null || !target.IsAlive())
            {
                Debug.LogWarning("Aucune cible valide pour DefBuffAlly !");
                return;
            }

            // Bonus basé sur la défense du lanceur
            int bonusDef = Mathf.CeilToInt(user.Def * SkillData.Damage / 100f); 
            // Si Damage = 50 → 50% de la Def du lanceur

            target.Def += bonusDef;
            Debug.Log($"{user.Name} augmente la défense de {target.Name} de {bonusDef} jusqu'au prochain tour de celui-ci !");

            // Retirer le buff au prochain tour du target
            void RemoveBuff(Hero h)
            {
                if (h == target)
                {
                    target.Def -= bonusDef;
                    Debug.Log($"{target.Name} perd le bonus de défense de {bonusDef}");
                    target.OnTurnStart -= RemoveBuff; // désinscrit l'événement
                }
            }

            target.OnTurnStart += RemoveBuff;
        }

        public override void Use(BattleSystem system)
        {
            // Pas de version sans cible
        }
    }
}