using Scripts;
using Scripts.Skills;
using UnityEngine;

namespace Skills.C1Poulpe
{
    public class C1PoulpeAttack : Skill
    {
        public C1PoulpeAttack(SkillData data, Hero user) : base(data, user) { }

        public override void Use(BattleSystem system, Hero target)
        {
            if (!CanUse(system)) return;
            if (target == null) return;

            int dmg = user.GetDamageFor(SkillData.Damage);
            user.DealDamage(dmg, target);
            Debug.Log($"{user.Name} attaque {target.Name} avec C1 pour {dmg} dégâts !");
        }
    }
}