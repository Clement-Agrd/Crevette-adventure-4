using Scripts;
using Scripts.Skills;
using System.Linq;
using UnityEngine;

namespace Skills.DoubleHit
{
    public class DoubleHit : Skill
    {
        public DoubleHit(SkillData data, Hero user) : base(data, user) { }

        public override void Use(BattleSystem system)
        {
            if (!CanUse(system)) return;
            // Récupère le premier ennemi vivant
            Hero target = user.IsEnemy
                ? system.GetAllAliveHeroes().FirstOrDefault()
                : system.GetAllAliveEnemies().FirstOrDefault();

            if (target == null)
            {
                Debug.Log($"{user.Name} n'a aucune cible pour {SkillData.Title} !");
                return;
            }

            int damage = user.GetDamageFor(SkillData.Damage);

            // Frappe deux fois en utilisant DealDamage pour déclencher OnAttack
            for (int i = 0; i < 2; i++)
            {
                user.DealDamage(damage, target);
                Debug.Log($"{user.Name} frappe {target.Name} ({i + 1}/2) avec {SkillData.Title} !");
            }
        }

        public override void Use(BattleSystem system, Hero target)
        {
            // Ignore la cible passée → attaque toujours le premier
            Use(system);
        }
    }
}