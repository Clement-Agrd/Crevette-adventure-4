using System;
using UnityEngine;
using Scripts.Skills;

namespace Scripts.Passives
{
    public class StackDamagePassive : Passive
    {
        private StackDamagePassiveData data;

        private Hero lastTarget;
        private int currentStacks = 0;

        public StackDamagePassive(Hero user, StackDamagePassiveData data) : base(user)
        {
            this.data = data;
            // 🔹 On écoute les attaques du lanceur, pas les dégâts subis
            user.OnAttack += OnHeroAttacked;
        }

        public void OnHeroAttacked(int dmg, Hero target)
        {
            if (target == null) return;
            

            // Calcule le bonus de dégâts
            int bonusDmg = Mathf.CeilToInt(dmg * data.StackMultiplier * currentStacks);

            // Applique le bonus directement à la cible
            target.TakeDamage(bonusDmg, user, false); // ignoreDef = true si tu veux que le bonus soit pur
            Debug.Log($"{user.Name} inflige {bonusDmg} bonus à {target.Name} ({currentStacks} stacks) !");
            
            // Si la cible change, reset stacks
            if (lastTarget != target && target.IsAlive())
            {
                lastTarget = target;
                currentStacks = 0;
            }

            currentStacks++;
        }

        // Méthodes pour les skills qui consomment les stacks
        public int GetStacksOnTarget(Hero target)
        {
            if (target == lastTarget)
                return currentStacks;
            return 0;
        }

        public void ConsumeStacksOnTarget(Hero target)
        {
            if (target == lastTarget)
            {
                currentStacks--;
                lastTarget = null;
            }
        }
        public void TransferStacks(Hero from, Hero to)
        {
            if (from != lastTarget || currentStacks <= 0) return;

            int stacksToTransfer = currentStacks;
            lastTarget = to;       // le nouvel ennemi devient la cible du passif
            currentStacks = stacksToTransfer;

            Debug.Log($"{stacksToTransfer} stacks transférées de {from.Name} à {to.Name} !");
        }
    }
}