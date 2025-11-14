using System.Linq;
using Scripts;
using Scripts.Skills;
using UnityEngine;

namespace Skills.C2Poulpe
{
    public class C2PoulpeAttack : Skill
    {
        public C2PoulpeAttack(SkillData user, Hero data) : base(user, data) { }

        public override void Use(BattleSystem system)
        {
            // Trouver tous les ennemis aveuglés
            var blindTargets = system.GetAllAliveEnemies()
                .Where(h => h.IsAlive() && h.IsEnemy != user.IsEnemy && h.BlindDebuff != null && h.BlindDebuff.Stacks > 0)
                .ToList();

            if (blindTargets.Count == 0)
            {
                Debug.Log("Aucun ennemi n'est aveuglé !");
                system.DontGoNextTurn();
                system.ShowPlayerSkills();
                return;
            }

            foreach (var enemy in blindTargets)
            {
                int stacks = enemy.BlindDebuff.Stacks;

                // Dégâts de base venant du SkillData + bonus par stack
                float damage = SkillData.Damage + (stacks * SkillData.Damage * 0.5f); // +50% par stack par exemple
                int dmg = user.GetDamageFor(Mathf.RoundToInt(damage));

                enemy.TakeDamage(dmg, user);

                Debug.Log($"{user.Name} inflige dégâts à {enemy.Name} ({stacks} stacks d'aveuglement)");

                // Supprime les stacks d'aveuglement après utilisation
                enemy.BlindDebuff.ClearStacks();
                
            }
        }
    }
}