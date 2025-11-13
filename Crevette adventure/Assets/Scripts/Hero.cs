using System;
using System.Collections.Generic;
using Scripts.Passives;
using Scripts.Skills;
using Scripts.Buffs;
using Skills.Estoc;
using Skills.Frappe;
using Skills.AOEAttak;
using Skills.DoubleStrike;
using UnityEngine;
using Skills.HealAlly;
using Skills.DefUp;
using Skills.DefBuffAlly;
using Skills.Taunt;
using Skills.DoubleHit;
using Skills.ConsumeStackStun;
using Skills.Camouflage;
using Skills.AssassinationStrike;
using Skills.C1Poulpe;
using Skills.C2Poulpe;
using Skills.UltPoulpe;


namespace Scripts
{
    public enum SkillEnum
    {
        FirstEnemieAttak,
        TargetAttak,
        AOEAttak,
        DoubleStrike,
        HealAlly,
        DefUpSkill,
        DefBuffAlly,
        TauntSkill,
        DoubleHit,
        ConsumeStackStun,
        Camouflage,
        AssassinationStrike,
        C1Poulpe,
        C2Poulpe,
        UltPoulpe,
    }

    public class Hero
    {
        public event Action<Hero> OnTurnStart;
        public event Action<int, Hero> OnHealed;
        public event Action<int, Hero> OnAttack;
        public event Action<int, Hero> OnDamaged;
        
        
        public string Name { get; private set; }
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }
        public int Atk { get; private set; }
        public int Def { get; set; }
        public int Speed { get; private set; }
        
        public int UltiCharges { get; private set; } = 0;
       
        public const int MaxUltiCharges = 3;
        public bool IsEnemy{ get; private set; }
        
        public bool IsTaunting { get; private set; } = false;
        public bool IsStunned { get; private set; } = false;
        public Passive Passive { get; private set; }
        public Sprite Portrait { get; private set; }

        // ✅ Instanciation correcte de chaque type de compétence
        private Dictionary<SkillEnum, Func<Skill>> skillFactory;

        public List<Skill> Skills = new();
        
        public Buffs.BuffCamouflage CamouflageBuff { get; private set; }

        public bool IsCamouflaged => CamouflageBuff != null && CamouflageBuff.IsActive;
        
        public Buffs.Blind BlindDebuff { get; set; } = null;

        public Hero(HeroData data)
        {
            Name = data.Name;
            MaxHealth = data.MaxHealth;
            CurrentHealth = MaxHealth;
            Atk = data.Atk;
            Def = data.Def;
            Speed = data.Speed;
            IsEnemy = data.IsEnemy;
            Portrait = data.Portrait;

            // ✅ Nouveau : création dynamique des skills
            skillFactory = new Dictionary<SkillEnum, Func<Skill>>()
            {
                {SkillEnum.FirstEnemieAttak, () => new FirstEnemieAttak(null, null)},
                {SkillEnum.TargetAttak, () => new TargetAttak(null, null)},
                {SkillEnum.AOEAttak, () => new AOEAttak(null, null)},
                {SkillEnum.DoubleStrike, () => new DoubleStrike(null, null)},
                {SkillEnum.HealAlly, () => new HealAlly(null, null)},
                {SkillEnum.DefUpSkill, () => new DefUpSkill(null, null)},
                {SkillEnum.DefBuffAlly, () => new DefBuffAlly(null, null)},
                {SkillEnum.TauntSkill, () => new TauntSkill(null, null)},
                {SkillEnum.DoubleHit, () => new DoubleHit(null, null)},
                {SkillEnum.ConsumeStackStun, () => new ConsumeStackStun(null, null)},
                {SkillEnum.Camouflage, () => new Camouflage(null, null)},
                {SkillEnum.AssassinationStrike, () => new AssassinationStrike(null, null)},
                {SkillEnum.C1Poulpe, () => new C1PoulpeAttack(null, null)},
                {SkillEnum.C2Poulpe, () => new C2PoulpeAttack(null, null)},
                {SkillEnum.UltPoulpe, () => new UltPoulpeAttack(null, null)},

            };

            foreach (SkillData skillData in data.Skills)
            {
                Skill skill = skillFactory[skillData.skillType](); 
                skill.SkillData = skillData;
                skill.user = this;
                Skills.Add(skill);
            }

            Passive = data.Passive.CreatePassive(this);
        }

        public bool IsAlive() => CurrentHealth > 0;
        public int GetDamageFor(int dmg)
        {
            int finalDmg = dmg * Atk;
            if (CamouflageBuff != null)
                finalDmg = CamouflageBuff.ApplyDamageBonus(finalDmg);
            return finalDmg;
        }   
        public int GetDamageScaleDefFor(int dmg) => dmg * Def;

        public void TakeDamage(int dmg, Hero from, bool ignoreDef = false)
        {
            int finalDamage;

            if (ignoreDef)
                finalDamage = dmg;
            else
                finalDamage = Mathf.Max(Mathf.CeilToInt(dmg / (float)Def)); 

            CurrentHealth -= finalDamage;
            if (CurrentHealth < 0) CurrentHealth = 0;
            Debug.Log($"{Name} subit {finalDamage} dégâts ! (HP restant : {CurrentHealth}/{MaxHealth})");
            OnDamaged?.Invoke(finalDamage, from);
        }

        public void TriggerTurnStart() => OnTurnStart?.Invoke(this);

        public void Heal(int heal, Hero hero)
        {
            CurrentHealth += heal;
            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;
            OnHealed?.Invoke(heal, hero); // ✅ Informe l’UI qu’on a été soigné
            Debug.Log($"{Name} Gagne {heal} Hp ! (HP restant : {CurrentHealth}/{MaxHealth})");
        }
        public void Taunt(int duration = 2)
        {
            IsTaunting = true;
            Debug.Log($"{Name} provoque les ennemis !");

            void RemoveTaunt(Hero h)
            {
                if (h == this)
                {
                    IsTaunting = false;
                    Debug.Log($"{Name} n'est plus en provocation.");
                    this.OnTurnStart -= RemoveTaunt;
                }
            }

            this.OnTurnStart += RemoveTaunt; // retire le taunt au prochain tour
        }
        
        public void Stun()
        {
            IsStunned = true;
            Debug.Log($"{Name} est étourdi !");

            void RemoveStun(Hero h)
            {
                if (h == this)
                {
                    IsStunned = false;
                    Debug.Log($"{Name} n'est plus étourdi !");
                    this.OnTurnStart -= RemoveStun;
                }
            }

            this.OnTurnStart += RemoveStun;
        }
        // Méthode utilitaire pour attaquer et déclencher l’événement
        public void DealDamage(int dmg, Hero target)
        {
            target.TakeDamage(dmg, this, false);
            OnAttack?.Invoke(dmg, target);
        }
        

        public void GainUltiCharge()
        {
            if (UltiCharges < MaxUltiCharges)
            {
                UltiCharges++;
                Debug.Log($"{Name} gagne une charge d'ulti ({UltiCharges}/{MaxUltiCharges})");
            }
        }

        public void ConsumeUltiCharges()
        {
            UltiCharges = 0;
        }
        
        public void ApplyCamouflage(int duration)
        {
            CamouflageBuff = new Buffs.BuffCamouflage(this, duration);
            Debug.Log($"{Name} entre en camouflage pour {duration} tours !");
        }

        
    }
}
