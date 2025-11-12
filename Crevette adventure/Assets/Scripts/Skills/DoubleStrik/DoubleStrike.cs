using Scripts;
using Scripts.Skills;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Skills.DoubleStrike
{
    public class DoubleStrike : Skill
    {
        public DoubleStrike(SkillData data, Hero user) : base(data, user)
        {
        }

        public override void Use(BattleSystem system)
        {
            if (!CanUse()) return;
            // ✅ Récupère la liste de cibles selon si c’est un ennemi ou un allié
            List<Hero> targets = user.IsEnemy
                ? system.GetAllAliveHeroes()  // l’ennemi frappe les alliés
                : system.GetAllAliveEnemies(); // le héros frappe les ennemis

            // ✅ Prend les 2 premiers
            var firstTwoTargets = targets.Take(3).ToList();

            if (firstTwoTargets.Count == 0)
            {
                Debug.Log($"{user.Name} n’a aucune cible pour {SkillData.Title}");
                return;
            }

            int totalDamage = user.GetDamageFor(SkillData.Damage);

            foreach (var target in firstTwoTargets)
            {
                target.TakeDamage(totalDamage, user, false);
                Debug.Log($"{user.Name} frappe {target.Name} avec {SkillData.Title} pour {totalDamage} dégâts !");
            }
        }

        public override void Use(BattleSystem system, Hero target)
        {
            Use(system); // même effet que sans cible directe
        }
    }
}