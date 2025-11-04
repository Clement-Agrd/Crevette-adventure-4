using UnityEngine;

using UnityEngine;

public class heros
{
    public string Name;
    public int CurrentHealth;
    public int MaxHealth;
    public int Speed; 
    public bool IsEnemy;

    public heros(string name, int maxHealth, int speed, bool isEnemy)
    {
        Name = name;
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        Speed = speed;
        IsEnemy = isEnemy;
    }

    public bool IsAlive()
    {
        return CurrentHealth > 0;
    }

    public void TakeDamage(int dmg)
    {
        CurrentHealth -= dmg;
        if (CurrentHealth < 0) CurrentHealth = 0;
        Debug.Log($"{Name} subit {dmg} dégâts ! (HP restant : {CurrentHealth}/{MaxHealth})");
    }
}
