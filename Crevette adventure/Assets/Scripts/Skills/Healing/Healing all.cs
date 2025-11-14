using System.Collections.Generic;
using Scripts;
using Scripts.Skills;
using UnityEngine;

namespace Skills.HealAllAlly
{
    public class HealAllAlly : Skill
    {
        public HealAllAlly(SkillData data, Hero user) : base(data, user) {}
        
        public override void Use(BattleSystem system)
        {
            if (!CanUse(system)) return;
            List<Hero> targets = user.IsEnemy 
                ? system.GetAllAliveEnemies()  
                : system.GetAllAliveHeroes();


            int healAmount = SkillData.Damage;
            foreach (Hero target in targets)
            {
                target.Heal(healAmount, target);
                target.GainUltiCharge();
                Debug.Log($"{user.Name} soigne {target.Name} pour {healAmount} HP et lui donnent une charge d'ulti ({target.UltiCharges}/3)!");
            }
        }

        // Pas de version "sans cible" car ton système ne l’utilise que pour les skills non ciblés
        public override void Use(BattleSystem system,  Hero target)
        {
            // Rien ici
        }
    }
}