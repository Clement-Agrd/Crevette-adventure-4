using Scripts;
using Scripts.Skills;
using System.Collections.Generic;
using UnityEngine;

namespace Skills.Camouflage
{
    public class Camouflage : Skill
    {
        public Camouflage(SkillData data, Hero user) : base(data, user)
        {
        }

        public override void Use(BattleSystem system)
        {
            if (!CanUse(system)) return;
            user.ApplyCamouflage(2);
            Debug.Log($"{user.Name} utilise {SkillData.Title} et devient invisible pour 2 tours !");
            List<Hero> targets = user.IsEnemy 
                ? system.GetAllAliveHeroes()  
                : system.GetAllAliveEnemies();

            int totalDamage = user.GetDamageFor(SkillData.Damage);

            foreach (Hero target in targets)
            {
                target.TakeDamage(totalDamage, user, false);
                Debug.Log($"{user.Name} inflige {totalDamage} dégâts à {target.Name} avec {SkillData.Title}");
            }
        }

        public override void Use(BattleSystem system, Hero target)
        {
            Use(system);
        }
    }
}