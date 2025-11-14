using System.Collections.Generic;
using System.Linq;
using Scripts;
using Scripts.Skills;
using UnityEngine;

namespace Skills.UltCerise
{
    public class UltCerise : Skill
    {
        public UltCerise(SkillData data, Hero user) : base(data, user) {}
        
        public override void Use(BattleSystem system)
        {
            if (!CanUse(system)) return;
            List<Hero> targets = user.IsEnemy 
                ? system.GetAllAliveEnemies()  
                : system.GetAllAliveHeroes();
            
            var firstTwoTargets = targets.Take(2).ToList();

            if (firstTwoTargets.Count == 0)
            {
                Debug.Log($"{user.Name} n’a aucune cible pour {SkillData.Title}");
                return;
            }


            int healAmount = SkillData.Damage;
            foreach (Hero target in firstTwoTargets)
            {
                target.Heal(healAmount, target);
                Debug.Log($"{user.Name} soigne {target.Name} pour {healAmount} HP !");
            }
        }

        // Pas de version "sans cible" car ton système ne l’utilise que pour les skills non ciblés
        public override void Use(BattleSystem system,  Hero target)
        {
            // Rien ici
        }
    }
}