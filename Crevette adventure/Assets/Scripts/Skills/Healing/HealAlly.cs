using Scripts;
using Scripts.Skills;
using UnityEngine;

namespace Skills.HealAlly
{
    public class HealAlly : Skill
    {
        public HealAlly(SkillData data, Hero user) : base(data, user) {}

        // Ici, le soin est ciblé → donc c'est cette version de Use() qui sera appelée
        public override void Use(BattleSystem system, Hero target)
        {
            if (!CanUse(system)) return;
            if (target == null || !target.IsAlive())
            {
                Debug.LogWarning("Aucune cible valide pour le soin !");
                return;
            }

            int healAmount = SkillData.Damage;
            target.HealanAlly(healAmount, target, user);
            Debug.Log($"{user.Name} soigne {target.Name} pour {healAmount} HP avec {SkillData.Title}");
        }

        // Pas de version "sans cible" car ton système ne l’utilise que pour les skills non ciblés
        public override void Use(BattleSystem system)
        {
            // Rien ici
        }
    }
}