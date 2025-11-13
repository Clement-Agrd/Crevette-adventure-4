using Scripts;
using Scripts.Skills;
using Scripts.Buffs;
using UnityEngine;

namespace Skills.DefUp
{
    public class DefUpSkill : Skill
    {
        public DefUpSkill(SkillData data, Hero user) : base(data, user) {}

        public override void Use(BattleSystem system)
        {
            if (!CanUse(system)) return;

            int bonusDef = SkillData.Damage; // On réutilise le champ Damage pour la valeur du buff
            int duration = 1; // ✅ Buff dure 1 tour

            var buff = new StatBuff(0, bonusDef, duration);
            user.AddBuff(buff);

            Debug.Log($"{user.Name} augmente sa DEF de {bonusDef} pour {duration} tour(s) !");
        }

        public override void Use(BattleSystem system, Hero target)
        {
            // Skill non ciblé → utilise la version simple
            Use(system);
        }
    }
}