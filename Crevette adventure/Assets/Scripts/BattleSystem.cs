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

            // Récupère le composant qui affiche l'image
                var renderer = heroObj.GetComponentInChildren<SpriteRenderer>();
                if (renderer != null)
                {
                    renderer.sprite = hero.Portrait;
                }

                
                
                // Optionnel : afficher le nom
                heroObj.name = hero.Name;
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

        
       
        public void HandleSkillChoice(Skill skill)
        {
            battleUI.HideAll();
            battleUI.OnSkillSelected -= HandleSkillChoice;

            // Liste des cibles possibles
            List<Hero> possibleTargets = allHeros
                .Where(h => h.IsAlive() && (skill.SkillData.TargetEnemy ? h.IsEnemy : !h.IsEnemy))
                .ToList();
            if (skill.SkillData.TargetEnemy)
                // Affiche les cibles
            {
                battleUI.ShowTargets(possibleTargets);
                battleUI.OnTargetSelected += (target) =>
                {
                    battleUI.HideAll();
                    battleUI.OnTargetSelected -= null;

                    skill.Use(this, target);
                    Debug.Log($"{current.Name} utilise {skill.SkillData.Title} sur {target.Name}");

                    NextTurn();
                };
            }
            else
            {
                skill.Use(this);

                NextTurn(); 
            }
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
       
        public List<Hero> GetAllAliveHeroes() =>
            allHeros.Where(c => !c.IsEnemy && c.IsAlive()).ToList();

        public List<Hero> GetAllAliveEnemies() =>
            allHeros.Where(c => c.IsEnemy && c.IsAlive()).ToList();

        
        void EnemyAction(Hero enemy)
        {
            Skill chosenSkill = enemy.Skills[Random.Range(0, enemy.Skills.Count)];
            chosenSkill.Use(this,enemy);
            Debug.Log($"{enemy.Name} attaque avec {chosenSkill.SkillData.Title}");
            Invoke(nameof(NextTurn), 1.5f);
        }

    }
}
