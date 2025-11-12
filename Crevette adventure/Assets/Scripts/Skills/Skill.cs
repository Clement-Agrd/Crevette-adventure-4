using UnityEngine;

namespace Scripts.Skills
{
    public class Skill
    {
        public SkillData SkillData { get; set; }
        public Hero user { get; set; }

        public Skill(SkillData data, Hero user)
        {
            this.user = user;
            SkillData = data;
        }

        /// <summary>
        /// Vérifie si la compétence peut être utilisée (charges ulti, etc.)
        /// Retourne true si OK, false sinon.
        /// </summary>
        protected bool CanUse(BattleSystem system)
        {
            if (SkillData.IsUltimate && user.UltiCharges < Hero.MaxUltiCharges)
            {
                Debug.LogWarning($"{user.Name} n'a pas assez de charges pour utiliser {SkillData.Title} !");
                system.DontGoNextTurn();
                system.ShowPlayerSkills(); // ✅ on redonne la main au joueur
                return false;
            }

            // Si c'est une ulti, consomme les charges
            if (SkillData.IsUltimate)
            {
                user.ConsumeUltiCharges();
            }

            return true;
        }

        /// <summary>
        /// Utilisation sans cible (ex: AOE)
        /// </summary>
        public virtual void Use(BattleSystem system)
        {
            if (!CanUse(system)) return; // Stoppe si pas autorisé
            // Logique par défaut (si besoin)
        }

        /// <summary>
        /// Utilisation avec cible
        /// </summary>
        public virtual void Use(BattleSystem system, Hero target)
        {
            if (!CanUse(system)) return; // Stoppe si pas autorisé
            // Logique par défaut (si besoin)
        }
    }
}