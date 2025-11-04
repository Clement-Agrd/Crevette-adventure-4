using UnityEngine;
using System;


[Serializable]
public class Skill
{
    public string Name;
    public string Description;
    public int MinDamage;
    public int MaxDamage;
    public bool TargetEnemy; // si false → peut viser un allié (ex : soin)

    public Skill(string name, string description, int minDmg, int maxDmg, bool targetEnemy = true)
    {
        Name = name;
        Description = description;
        MinDamage = minDmg;
        MaxDamage = maxDmg;
        TargetEnemy = targetEnemy;
    }

    public void Use(Hero user, Hero target)
    {
        if (!TargetEnemy)
        {
            // Exemple : soin
            int heal = UnityEngine.Random.Range(MinDamage, MaxDamage + 1);
            target.CurrentHealth += heal;
            if (target.CurrentHealth > target.MaxHealth) target.CurrentHealth = target.MaxHealth;
            Debug.Log($"{user.Name} soigne {target.Name} de {heal} HP !");
        }
        else
        {
            // Attaque classique
            int dmg = UnityEngine.Random.Range(MinDamage, MaxDamage + 1);
            target.TakeDamage(dmg);
            Debug.Log($"{user.Name} utilise {Name} sur {target.Name} et inflige {dmg} dégâts !");
        }
    }
}

