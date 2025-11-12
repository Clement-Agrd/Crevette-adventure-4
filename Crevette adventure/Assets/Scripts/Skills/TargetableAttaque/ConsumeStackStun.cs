using Scripts;
using Scripts.Passives;
using Scripts.Skills;
using UnityEngine;

namespace Skills.ConsumeStackStun
{
    public class ConsumeStackStun : Skill
    {
        public ConsumeStackStun(SkillData data, Hero user) : base(data, user) { }

        public override void Use(BattleSystem system, Hero target)
        {
            if (!CanUse()) return;
            if (target == null || !target.IsAlive())
            {
                Debug.LogWarning("Aucune cible valide pour ConsumeStackStun !");
                return;
            }

            // Cherche le passif StackDamagePassive du lanceur
            var stackPassive = user.Passive as StackDamagePassive;
            if (stackPassive == null)
            {
                Debug.LogWarning($"{user.Name} n'a pas le passif StackDamagePassive !");
                return;
            }


            // Vérifie s'il y a des stacks sur la cible
            int stacks = stackPassive.GetStacksOnTarget(target); // il faudra exposer une méthode publique pour récupérer le nb de stacks
            if (stacks <= 0)
            {
                Debug.Log($"{target.Name} n'a pas de stacks sur lui !");
                return;
            }

            // Consomme toutes les stacks
            stackPassive.ConsumeStacksOnTarget(target);

            // Applique le stun
            target.Stun();
            Debug.Log($"{user.Name} consomme {stacks} stacks pour étourdir {target.Name} !");
        }

        public override void Use(BattleSystem system)
        {
            // pas de version sans cible
        }
    }
}