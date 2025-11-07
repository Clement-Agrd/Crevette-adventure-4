using System;
using System.Collections.Generic;
using UnityEngine;

public class Hero
{
    public event Action<int, Hero> OnDamaged;
    public string Name;
    public int CurrentHealth;
    public int MaxHealth;
    public int Atk;
    public int Def;
    public int Speed;
    public bool IsEnemy;
    public List<Skill> Skills = new List<Skill>();
    public Passive Passive { get; private set; }
    public Hero(string name, int maxHealth, int atk,int def, int speed, bool isEnemy)
    {
        Name = name;
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        Atk = atk;
        Def = def;
        Speed = speed;
        IsEnemy = isEnemy;
    }

    public bool IsAlive() => CurrentHealth > 0;

    public void TakeDamage(int dmg, Hero from)
    {
        CurrentHealth -= dmg;
        if (CurrentHealth < 0) CurrentHealth = 0;
        Debug.Log($"{Name} subit {dmg} dégâts ! (HP restant : {CurrentHealth}/{MaxHealth})");
        OnDamaged?.Invoke(dmg, from);
    }

    public void AddSkill(Skill skill)
    {
        Skills.Add(skill);
    }
}