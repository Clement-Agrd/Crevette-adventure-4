using System;
using System.Collections.Generic;
using Scripts.Passives;
using Scripts.Skills;
using UnityEngine;

namespace Scripts
{
    public class Hero
    {
        public event Action<int, Hero> OnDamaged;
        public event Action<Hero> OnTurnStart;
        public string Name { get; private set; }
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }
        public int Atk { get; private set; }
        public int Def { get; private set; }
        public int Speed { get; private set; }
        public bool IsEnemy{ get; private set; }
        public Passive Passive { get; private set; }
        
    
        public List<ISkill> Skills = new();
        public Hero(HeroData data)
        {
            Name = data.Name;
            MaxHealth = data.MaxHealth;
            CurrentHealth = MaxHealth;
            Atk = data.Atk;
            Def = data.Def;
            Speed = data.Speed;
            IsEnemy = data.IsEnemy;

            foreach (SkillData skillData in data.Skills)
            {
                ISkill skill = skillData.CreateSkill(this); 
                Skills.Add(skill);
            }
            
            Passive = data.Passive.CreatePassive(this);
        }

        public bool IsAlive() => CurrentHealth > 0;


        public int GetDamageFor(int dmg) => dmg * Atk;

        public void TakeDamage(int dmg, Hero from, bool ignoreDef = false)
        {
            if (ignoreDef)
                CurrentHealth -= dmg;
            else
                CurrentHealth -= Mathf.CeilToInt(dmg / Def);
            
            if (CurrentHealth < 0) CurrentHealth = 0;
            Debug.Log($"{Name} subit {dmg / Def} dégâts ! (HP restant : {CurrentHealth}/{MaxHealth})");
            OnDamaged?.Invoke(dmg, from);
        }
        
        public void TriggerTurnStart()
        {
            OnTurnStart?.Invoke(this);
        }

        

        public void Heal(int heal, Hero hero)
        {
            CurrentHealth += heal;
            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;
            Debug.Log($"{Name} Gagne { heal } Hp ! (HP restant : {CurrentHealth}/{MaxHealth})");
        }
        
    }
}