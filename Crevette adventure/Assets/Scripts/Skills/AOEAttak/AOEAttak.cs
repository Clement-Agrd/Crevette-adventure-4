using Scripts;
using Scripts.Skills;
using System.Collections.Generic;
using UnityEngine;

namespace Skills.AOEAttak
{
    public class AOEAttak : Skill
    {
        public AOEAttak(SkillData data, Hero user) : base(data, user)
        {
        }

        public override void Use(BattleSystem system)
        {
            if (!CanUse(system)) return;
            List<Hero> targets = user.IsEnemy 
                ? system.GetAllAliveHeroes()  
                : system.GetAllAliveEnemies();

            int totalDamage = user.GetDamageFor(SkillData.Damage);

            foreach (Hero target in targets)
            {
                target.TakeDamage(totalDamage, user, false);
                Debug.Log($"{user.Name} attaque {target.Name} avec {SkillData.Title}");
            }
        }

        public override void Use(BattleSystem system, Hero target)
        {
            Use(system);
        }
    }
}