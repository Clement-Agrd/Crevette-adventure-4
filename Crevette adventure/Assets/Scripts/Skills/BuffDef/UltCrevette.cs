using System.Collections.Generic;
using Scripts;
using Scripts.Skills;
using Scripts.Buffs;
using UnityEngine;

namespace Skills.UltCrevette
{
    public class UltCrevette : Skill
    {
        public UltCrevette(SkillData data, Hero user) : base(data, user) {}

        public override void Use(BattleSystem system)
        {
            if (!CanUse(system)) return;

            int bonusStat = SkillData.Damage;
            int duration = 2; // ✅ dure 2 tours

            List<Hero> targets = user.IsEnemy
                ? system.GetAllAliveEnemies()
                : system.GetAllAliveHeroes();

            foreach (Hero target in targets)
            {
                var buff = new StatBuff(bonusStat, bonusStat, duration);
                target.AddBuff(buff);
                Debug.Log($"{user.Name} accorde +{bonusStat} ATK/DEF à {target.Name} pendant {duration} tours !");
            }
        }

        public override void Use(BattleSystem system, Hero target) { }
    }
}