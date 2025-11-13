using Scripts;
using Scripts.Skills;
using Scripts.Buffs;
using UnityEngine;

namespace Skills.DefBuffAlly
{
    public class DefBuffAlly : Skill
    {
        public DefBuffAlly(SkillData data, Hero user) : base(data, user) { }

        public override void Use(BattleSystem system, Hero target)
        {
            if (!CanUse(system)) return;
            if (target == null || !target.IsAlive())
            {
                Debug.LogWarning("❌ Aucune cible valide pour DefBuffAlly !");
                return;
            }

            // 🧮 Calcul du bonus basé sur la DEF du lanceur
            int bonusDef = Mathf.CeilToInt(user.Def * SkillData.Damage / 100f); 
            int duration = 1; // ✅ dure 1 tour par défaut

            // 📈 Crée un buff DEF temporaire
            var buff = new StatBuff(0, bonusDef, duration);
            target.AddBuff(buff);

            Debug.Log($"🛡️ {user.Name} augmente la DEF de {target.Name} de {bonusDef} pour {duration} tour(s) !");
        }

        public override void Use(BattleSystem system)
        {
            // Ce skill est ciblable, donc cette version ne fait rien
        }
    }
}