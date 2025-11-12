using Scripts;
using Scripts.Skills;

namespace Skills.Frappe
{
    public class TargetAttak : Skill
    {
        public TargetAttak(SkillData data, Hero user) : base(data, user)
        {
        }

        public override void Use(BattleSystem system)
        {
            
        }

        public override void Use(BattleSystem system, Hero target)
        {
            if (!CanUse()) return;
            int totalDamage = user.GetDamageFor(SkillData.Damage);
            target.TakeDamage(totalDamage, user, false);
        }

    }
}