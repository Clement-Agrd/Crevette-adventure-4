using System;
using UnityEngine;

namespace Scripts
{
    [Serializable]
    public class Skill
    {
        public string Name;
        public string Description;
        public int Damage;
        public bool TargetEnemy; // si false → peut viser un allié (ex : soin)

        public Skill(string name, string description, int Dmg, bool targetEnemy = true)
        {
            Name = name;
            Description = description;
            Damage = Dmg;
            TargetEnemy = targetEnemy;
        }

        public void Use(Hero user, Hero target)
        {
            if (!TargetEnemy)
            {
                // Exemple : soin
                int heal = Damage;
                target.Heal(heal);
                Debug.Log($"{user.Name} soigne {target.Name} de {heal} HP !");
            }
            else
            {
                // Attaque classique
                int dmg = user.Atk *Damage/target.Def;
                target.TakeDamage(dmg, user);
                Debug.Log($"{user.Name} utilise {Name} sur {target.Name} et inflige {dmg} dégâts !");
            }
        }
    }
}

