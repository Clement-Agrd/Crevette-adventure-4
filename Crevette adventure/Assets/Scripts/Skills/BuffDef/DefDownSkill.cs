using System.Collections.Generic;
using System.Linq;
using Scripts;
using Scripts.Skills;
using Scripts.Buffs;
using UnityEngine;

namespace Skills.DefDown
{
    public class DefDownSkill : Skill
    {
        public DefDownSkill(SkillData data, Hero user) : base(data, user) { }

        // Version ciblée sur un seul ennemi
        public override void Use(BattleSystem system, Hero target)
        {

        }

        // Version non ciblée (pour le cas où ce serait une attaque de zone)
        public override void Use(BattleSystem system)
        {
            if (!CanUse(system)) return;
            List<Hero> targets = user.IsEnemy 
                ? system.GetAllAliveHeroes()  
                : system.GetAllAliveEnemies();

            var firstTwoTargets = targets.Take(2).ToList();

            if (firstTwoTargets.Count == 0)
            {
                Debug.Log("Aucun ennemi valide pour DefDownSkill !");
                return;
            }

            foreach (var enemy in firstTwoTargets)
            {
                int debuffAmount = SkillData.Damage;
                int duration = 2;
                var debuff = new StatBuff(0, -debuffAmount, duration);
                enemy.AddBuff(debuff);

                Debug.Log($"🔻 {user.Name} réduit la DEF de {enemy.Name} de {debuffAmount} pendant {duration} tours !");
            }
        }
    }
}