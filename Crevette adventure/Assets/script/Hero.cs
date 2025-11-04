using System.Collections.Generic;
using UnityEngine;

public class Hero
{
    public string Name;
    public int CurrentHealth;
    public int MaxHealth;
    public int Speed;
    public bool IsEnemy;
    public List<Skill> Skills = new List<Skill>();

    public Hero(string name, int maxHealth, int speed, bool isEnemy)
    {
        Name = name;
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        Speed = speed;
        IsEnemy = isEnemy;
    }

    public bool IsAlive() => CurrentHealth > 0;

    public void TakeDamage(int dmg)
    {
        CurrentHealth -= dmg;
        if (CurrentHealth < 0) CurrentHealth = 0;
        Debug.Log($"{Name} subit {dmg} dégâts ! (HP restant : {CurrentHealth}/{MaxHealth})");
    }

    public void AddSkill(Skill skill)
    {
        Skills.Add(skill);
    }
}