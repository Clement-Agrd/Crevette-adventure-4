using System.Collections.Generic;
using System.Linq;
using Scripts.Skills;
using UnityEngine;

namespace Scripts
{
    public class BattleSystem : MonoBehaviour
    {
        public BattleUI battleUI;
        private List<Hero> allHeros = new List<Hero>();
        private int currentTurnIndex = 0;
        private Hero current;

        [SerializeField] private HeroData[] heroes;
        
        void Start()
        {
            allHeros = new List<Hero>();
            for (var i = 0; i < heroes.Length; i++)
            {
                HeroData heroData = heroes[i];
                Hero hero = heroData.CreateHero();
                
                allHeros.Add(hero);
            }

            // Tri par vitesse
            allHeros = allHeros.OrderByDescending(c => c.Speed).ToList();

            StartTurn();
        }

        void StartTurn()
        {   
            if (IsBattleOver()) return;

            current = allHeros[currentTurnIndex];
            if (!current.IsAlive())
            {
                NextTurn();
                return;
            }

            Debug.Log($"C'est le tour de {current.Name} !");

            
            if (current.IsEnemy)
            {
                battleUI.HideAll();
                EnemyAction(current);
            }
            else
            {
                ShowPlayerSkills();
            }

        }
        void ShowPlayerSkills()
        {
            battleUI.ShowSkills(current.Skills);
            battleUI.OnSkillSelected += HandleSkillChoice; // ✅ écoute du clic
        }

        void HandleSkillChoice(ISkill chosenSkill)
        {
            // Nettoyer l'UI
            battleUI.HideAll();
            battleUI.OnSkillSelected -= HandleSkillChoice;

            // Exécuter la compétence
            chosenSkill.Use(this); // ✅ appelle la logique interne du skill
            Debug.Log($"{current.Name} utilise {chosenSkill.SkillData.Title}");

            // Passer au tour suivant
            NextTurn();
        }



        void NextTurn()
        {
            currentTurnIndex++;
            if (currentTurnIndex >= allHeros.Count)
                currentTurnIndex = 0;

            Invoke(nameof(StartTurn), 1f);
        }

        bool IsBattleOver()
        {
            bool allPlayersDead = !allHeros.Any(c => !c.IsEnemy && c.IsAlive());
            bool allEnemiesDead = !allHeros.Any(c => c.IsEnemy && c.IsAlive());

            if (allPlayersDead)
            {
                Debug.Log("Tous les héros sont morts... 💀 Défaite !");
                battleUI.HideAll();
                return true;
            }

            if (allEnemiesDead)
            {
                Debug.Log("Victoire 🎉 !");
                battleUI.HideAll();
                return true;
            }

            return false;
        }

        
        public Hero GetFirstEnemyHero()
        {
            return allHeros.FirstOrDefault(c => c.IsEnemy && c.IsAlive());
        }
        
        void EnemyAction(Hero enemy)
        {
            ISkill chosenSkill = enemy.Skills[Random.Range(0, enemy.Skills.Count)];
            chosenSkill.Use(this);
            Debug.Log($"{enemy.Name} attaque avec {chosenSkill.SkillData.Title}");
            Invoke(nameof(NextTurn), 1.5f);
        }


    }
}
