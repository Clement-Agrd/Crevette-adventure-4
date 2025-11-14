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
                // --- 1) Récupération de la liste correcte selon l'équipe ---
                var possibleTargets = user.IsEnemy
                    ? system.GetAllAliveHeroes()   // Ennemi → cible un héros
                    : system.GetAllAliveEnemies(); // Héros → cible un ennemi

                // On ignore les camouflés
                possibleTargets = possibleTargets
                    .Where(t => !t.IsCamouflaged)
                    .ToList();

                if (possibleTargets.Count == 0)
                {
                    Debug.Log("Aucune cible disponible (camouflage ? morts ?)");
                    break;
                }

                // --- 2) Gestion de la Provocation (Taunt) ---
                var taunting = possibleTargets.FirstOrDefault(t => t.IsTaunting);
                if (taunting != null)
                {
                    possibleTargets = new() { taunting };
                }

                // --- 3) Sélection d'une cible aléatoire ---
                Hero chosenTarget = possibleTargets[Random.Range(0, possibleTargets.Count)];

                // --- 4) Application des dégâts ---
                int damage = user.GetDamageFor(SkillData.Damage);
                chosenTarget.TakeDamage(damage, user);

                Debug.Log($"{user.Name} attaque {chosenTarget.Name} ({i + 1}/{stacks})");
            }
        }

        public override void Use(BattleSystem system, Hero target)
        {
            Use(system);
        }
    }
}
