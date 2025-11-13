using System.Collections.Generic;
using System.Linq;
using Scripts.Skills;
using UnityEngine;
using Random = UnityEngine.Random;
using System;
using UnityEditor.Experimental.GraphView;

namespace Scripts
{

    public class BattleSystem : MonoBehaviour
    {
        
        [SerializeField] private Transform[] allyPositions;
        [SerializeField] private Transform[] enemyPositions;
        [SerializeField] private GameObject heroPrefab;
        [SerializeField] private HeroUIManager heroUIManager;

        public BattleUI battleUI;
        private List<Hero> allHeros = new List<Hero>();
        private Dictionary<Hero, GameObject> heroObjects = new Dictionary<Hero, GameObject>();
        private int currentTurnIndex = 0;
        public Hero current;
        public bool CanNextTurn { get; private set; }= true;
        
      

        public static HeroData[] heroes;
        
        

        void Start()
        {
            allHeros = new List<Hero>();

            // Ajout des héros sélectionnés par le joueur
            for (var i = 0; i < heroes.Length; i++)
            {
                HeroData heroData = heroes[i];
                Hero hero = heroData.CreateHero();
                allHeros.Add(hero);
            }

            // ✅ Ajout des ennemis (4 par exemple)
            HeroData[] allEnemies = Resources.LoadAll<HeroData>("Heroes")
                .Where(h => h.IsEnemy)
                .Take(4) // Limite à 4 ennemis
                .ToArray();

            foreach (HeroData enemyData in allEnemies)
            {
                Hero enemy = enemyData.CreateHero();
                allHeros.Add(enemy);
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
                heroObjects.Add(hero, heroObj);

                var renderer = heroObj.GetComponentInChildren<SpriteRenderer>();
                if (renderer != null)
                {
                    renderer.sprite = hero.Portrait;
                }

                heroObj.name = hero.Name;
            }

            heroUIManager.Initialize(heroObjects);

            allHeros = allHeros.OrderByDescending(c => c.Speed).ToList();
            StartTurn();
        }


        public void StartTurn()
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
            current.GainUltiCharge();
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

        public void ShowPlayerSkills()
        {
            battleUI.ShowSkills(current.Skills);
            battleUI.OnSkillSelected += HandleSkillChoice; // ✅ écoute du clic
        }

        
       
        public void HandleSkillChoice(Skill skill)
        {
            // Masque les boutons existants et désinscrit l'événement
            battleUI.HideAll();
            battleUI.OnSkillSelected -= HandleSkillChoice;

            List<Hero> possibleTargets = new List<Hero>();

            // Détermine les cibles selon le type de skill
            if (skill.SkillData.AffectEnemies)
            {
                // Skills offensifs → sélectionne les ennemis vivants non camouflés
                possibleTargets = allHeros
                    .Where(h => h.IsAlive() 
                                && h.IsEnemy != current.IsEnemy 
                                && !h.IsCamouflaged) // ⛔ ignore les héros camouflés
                    .ToList();

                // ✅ Vérifie si un ennemi est en Taunt → devient la seule cible
                Hero taunting = possibleTargets.FirstOrDefault(h => h.IsTaunting);
                if (taunting != null)
                {
                    possibleTargets = new List<Hero> { taunting };
                }
            }
            else
            {
                // Skills de soutien → sélectionne les alliés vivants
                possibleTargets = allHeros.Where(h => h.IsAlive() && h.IsEnemy == current.IsEnemy).ToList();
            }

            // Si le skill nécessite de choisir une cible
            if (skill.SkillData.TargetEnemy)
            {
                // Affiche les boutons de sélection
                battleUI.ShowTargets(possibleTargets);

                // Événement appelé quand une cible est choisie
                void OnTargetChosen(Hero target)
                {
                    battleUI.HideAll();
                    battleUI.OnTargetSelected -= OnTargetChosen;

                    skill.Use(this, target);
                    Debug.Log($"{current.Name} utilise {skill.SkillData.Title} sur {target.Name}");

                    if (CanNextTurn)
                    {
                        NextTurn();
                    }
                    else
                    {
                        CanNextTurn = true;
                    }
                }

                battleUI.OnTargetSelected += OnTargetChosen;
            }
            else
            {
                // Skill non ciblable → exécution directe
                skill.Use(this);
                if (CanNextTurn)
                {
                    NextTurn();
                }
                else
                {
                    CanNextTurn = true;
                }
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

        public void DontGoNextTurn()
        {
            CanNextTurn = false;
        }
        
        public Hero GetWeakestHero()
        {
            return allHeros
                .Where(h => !h.IsEnemy && h.IsAlive())
                .OrderBy(h => h.CurrentHealth)
                .FirstOrDefault();
        }

        public Hero GetWeakestEnemyAlly(Hero currentEnemy)
        {
            return allHeros
                .Where(h => h.IsEnemy && h.IsAlive() && h != currentEnemy)
                .OrderBy(h => h.CurrentHealth)
                .FirstOrDefault();
        }


    }
}
