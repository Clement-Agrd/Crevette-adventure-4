using System.Collections.Generic;
using System.Linq;
using Scripts.Skills;
using UnityEngine;
using Random = UnityEngine.Random;
using System;

namespace Scripts
{

    public class BattleSystem : MonoBehaviour
    {
        
        [SerializeField] private Transform[] allyPositions;
        [SerializeField] private Transform[] enemyPositions;
        [SerializeField] private GameObject heroPrefab;

        public BattleUI battleUI;
        private List<Hero> allHeros = new List<Hero>();
        private Dictionary<Hero, GameObject> heroObjects = new Dictionary<Hero, GameObject>();
        private int currentTurnIndex = 0;
        public Hero current;
      

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

            int allyIndex = 0;
            int enemyIndex = 0;

            foreach (Hero hero in allHeros)
            {
                Transform spawnPoint;
                if (hero.IsEnemy)
                {
                    spawnPoint = enemyPositions[enemyIndex];
                    enemyIndex++;
                }
                else
                {
                    spawnPoint = allyPositions[allyIndex];
                    allyIndex++;
                }

                GameObject heroObj = Instantiate(heroPrefab, spawnPoint.position, Quaternion.identity);
                heroObjects.Add(hero, heroObj); // ✅ AJOUT ICI

                var renderer = heroObj.GetComponentInChildren<SpriteRenderer>();
                if (renderer != null)
                {
                    renderer.sprite = hero.Portrait;
                }

                heroObj.name = hero.Name;
            }

            // ✅ AJOUT ICI
            FindObjectOfType<HeroUIManager>().Initialize(heroObjects);

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
            
            // ✅ Déclenche l'événement
            current.TriggerTurnStart();
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

        
       
        public Hero GetFirstAliveHero() => allHeros.FirstOrDefault(c => !c.IsEnemy && c.IsAlive());
        public Hero GetFirstAliveEnemy(Hero user) => allHeros.FirstOrDefault(c => c.IsEnemy && c.IsAlive() && c != user);

        
        void EnemyAction(Hero enemy)
        {
            ISkill chosenSkill = enemy.Skills[Random.Range(0, enemy.Skills.Count)];
            chosenSkill.Use(this);
            Debug.Log($"{enemy.Name} attaque avec {chosenSkill.SkillData.Title}");
            Invoke(nameof(NextTurn), 1.5f);
        }

    }
}
