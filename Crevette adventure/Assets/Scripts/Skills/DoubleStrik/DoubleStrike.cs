using Scripts;
using Scripts.Skills;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Skills.DoubleStrike
{
    public class DoubleStrike : Skill
    {
        public DoubleStrike(SkillData data, Hero user) : base(data, user) {}

        public override void Use(BattleSystem system)
        {
            Debug.Log("DoubleStrike.Use() appelée !");

            // ✅ Sélectionne les deux premières cibles valides selon le camp du lanceur
            List<Hero> targets = user.IsEnemy
                ? system.GetAllAliveHeroes().Take(2).ToList()   // l'ennemi attaque les 2 premiers héros
                : system.GetAllAliveEnemies().Take(2).ToList(); // le héros attaque les 2 premiers ennemis

            if (targets.Count == 0)
            {
                Debug.Log("Aucune cible disponible !");
                return;
            }

            int totalDamage = user.GetDamageFor(SkillData.Damage);

            foreach (Hero target in targets)
            {
                target.TakeDamage(totalDamage, user, false);
                Debug.Log($"{user.Name} frappe {target.Name} avec {SkillData.Title} pour {totalDamage} dégâts !");
            }
        }

        public override void Use(BattleSystem system, Hero target)
        {
            // Pas besoin d'une cible spécifique : on touche automatiquement les 2 premiers
            Use(system);
        }
    }
}