using Scripts;
using Scripts.Skills;
using Scripts.Buffs;
using UnityEngine;

namespace Skills.ATKBuffAlly
{
    public class ATKBuffAlly : Skill
    {
        public ATKBuffAlly(SkillData data, Hero user) : base(data, user) { }

        public override void Use(BattleSystem system, Hero target)
        {
            if (!CanUse(system)) return;
            if (target == null || !target.IsAlive())
            {
                Debug.LogWarning("❌ Aucune cible valide pour DefBuffAlly !");
                return;
            }
            
            int bonusAtk = Mathf.CeilToInt(user.Atk * SkillData.Damage / 100f); 
            int duration = 2; 
            
            var buff = new StatBuff(0, bonusAtk, duration);
            target.AddBuff(buff);

            Debug.Log($"🛡️ {user.Name} augmente l'Atk de {target.Name} de {bonusAtk} pour son prochain tour !");
        }

        public override void Use(BattleSystem system)
        {
            // Ce skill est ciblable, donc cette version ne fait rien
        }
    }
}