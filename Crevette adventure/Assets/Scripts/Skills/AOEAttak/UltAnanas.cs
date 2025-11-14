using Scripts;
using Scripts.Skills;
using System.Collections.Generic;
using Scripts.Buffs;
using UnityEngine;

namespace Skills.UltAnanas
{
    public class UltAnanas : Skill
    {
        public UltAnanas(SkillData data, Hero user) : base(data, user)
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
                int debuffAmount = SkillData.Damage;
                int duration = 2;
                var debuff = new StatBuff(-debuffAmount, 0, duration);
                target.AddBuff(debuff);

                Debug.Log($"🔻 {user.Name} réduit l'Atk de {target.Name} de {debuffAmount} pendant pour son prochain tour tours !");
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