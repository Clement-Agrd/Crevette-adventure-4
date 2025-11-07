using Scripts;
using Scripts.Skills;

namespace Skills.Estoc
{
    public class EstocSkill : Skill<EstocSkillData>
    {
        public EstocSkill(EstocSkillData data, Hero user) : base(data, user)
        {
            
        }

        public override void Use(BattleSystem system)
        {
            Hero targetHero = user.IsEnemy ? system.GetFirstAliveHero() : system.GetFirstAliveEnemy(user);
            int totalDamage = user.GetDamageFor(Data.Damage);
            
            targetHero.TakeDamage(totalDamage, user, false);
        }
    }
}