using System.Linq;
using Scripts;
using Scripts.Skills;
using UnityEngine;

namespace Skills.UltPoulpe
{
    public class UltPoulpeAttack : Skill
    {
        public UltPoulpeAttack(SkillData data, Hero user) : base(data, user) { }

        public override void Use(BattleSystem system)
        {
            if (!CanUse(system)) return;
            var targets = system.GetAllAliveEnemies();
            int dmg = user.GetDamageFor(SkillData.Damage);

            foreach (var target in targets)
            {
                user.DealDamage(dmg, target);
                Debug.Log($"{user.Name} inflige des dégâts à {target.Name} avec l'Ult !");
            }

            Debug.Log($"{user.Name} rejoue son tour !");
            system.DontGoNextTurn();
            system.ShowPlayerSkills(); // ✅ on redonne la main au joueur
            user.ConsumeUltiCharges();
        }
    }
}