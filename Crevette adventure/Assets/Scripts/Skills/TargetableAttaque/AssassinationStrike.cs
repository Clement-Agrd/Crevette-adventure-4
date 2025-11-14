using System.Linq;
using Scripts;
using Scripts.Passives;
using Scripts.Skills;
using UnityEngine;

namespace Skills.AssassinationStrike
{
    public class AssassinationStrike : Skill
    {
        public AssassinationStrike(SkillData data, Hero user) : base(data, user) { }

        public override void Use(BattleSystem system, Hero target)
        {
            if (!CanUse(system)) return;
            if (target == null) return;

            int damage = user.GetDamageFor(SkillData.Damage);

            // Inflige les dégâts via DealDamage pour que les passifs s'activent
            user.DealDamage(damage, target);
            Debug.Log($"{user.Name} utilise {SkillData.Title} sur {target.Name} pour {damage} dégâts !");

            // Vérifie si la cible est morte
            if (!target.IsAlive())
            {
                Debug.Log($"{target.Name} est mort ! Transfert des stacks de {user.Name}...");

                var stackPassive = user.Passive as StackDamagePassive;
                if (stackPassive != null)
                {
                    // Récupère le prochain ennemi vivant derrière la cible
                    Hero nextTarget = system.GetAllAliveEnemies().Where(h => h != target).FirstOrDefault();
                    if (nextTarget != null)
                    {
                        stackPassive.TransferStacks(target, nextTarget);
                    }
                }
            }

        }

        public override void Use(BattleSystem system)
        {
            // Nécessite une cible → non utilisé
        }
    }
}
