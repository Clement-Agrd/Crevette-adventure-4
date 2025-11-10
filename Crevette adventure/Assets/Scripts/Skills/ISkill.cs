namespace Scripts.Skills
{
    public interface ISkill
    {
        SkillData SkillData { get; }
        void Use(BattleSystem system);
    }
}