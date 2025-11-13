using System.Linq;
using Scripts;
using Scripts.Passives;
using Scripts.Skills;
using UnityEngine;

namespace Skills.StackAttack
{
    public class StackAttackSkill : Skill
    {
        public StackAttackSkill(SkillData data, Hero user) : base(data, user) { }

        public override void Use(BattleSystem system)
        {
            if (!CanUse(system)) return;

            // Récupère le passif de stack
            var stackPassive = user.Passive as GainStackPassive;
            if (stackPassive == null)
            {
                Debug.LogWarning("Pas de GainStackPassive trouvé !");
                return;
            }

            int stacks = stackPassive.CurrentStacks;

            if (stacks <= 0)
            {
                Debug.Log($"{user.Name} n'a aucune stack, aucune attaque !");
                return;
            }

            for (int i = 0; i < stacks; i++)
            {
                Hero chosenTarget;
                if (user.IsEnemy)
                {
                    var allies = system.GetAllAliveHeroes();
                    if (allies.Count == 0) break;
                    chosenTarget = allies[Random.Range(0, allies.Count)];
                }
                else
                {
                    var enemies = system.GetAllAliveEnemies();
                    if (enemies.Count == 0) break;
                    chosenTarget = enemies[Random.Range(0, enemies.Count)];
                }

                int damage = user.GetDamageFor(SkillData.Damage);
                chosenTarget.TakeDamage(damage, user);

                Debug.Log($"{user.Name} attaque {chosenTarget.Name} pour {damage} dégâts ({i+1}/{stacks})");
            }
        }

        public override void Use(BattleSystem system, Hero target)
        {
            Use(system);
        }
    }
}