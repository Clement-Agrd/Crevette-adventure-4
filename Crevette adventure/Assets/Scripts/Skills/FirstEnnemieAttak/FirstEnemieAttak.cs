using Scripts;
using Scripts.Skills;
using UnityEngine;

namespace Skills.Estoc
{
    public class FirstEnemieAttak : Skill
    {
        public FirstEnemieAttak(SkillData data, Hero user) : base(data, user)
        {
            
        }

        public override void Use(BattleSystem system)
        {
            Hero targetHero = user.IsEnemy ? system.GetFirstAliveHero() : system.GetFirstAliveEnemy(user);
            int totalDamage = user.GetDamageFor(SkillData.Damage);
            
            targetHero.TakeDamage(totalDamage, user, false);
            Debug.Log($"{user.Name} evoie damage {SkillData.Title} sur {targetHero.Name}");
        }

        public override void Use(BattleSystem system, Hero target)
        {
            Use(system);
        }
    }
}