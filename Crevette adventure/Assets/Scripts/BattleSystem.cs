using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public BattleUI battleUI;
    public SkillDatabase skillDB; // référence dans l’inspecteur

    private List<Hero> allHeros = new List<Hero>();
    private int currentTurnIndex = 0;
    private Hero current;

    void Start()
    {
        // --- Héros ---
        
        Hero Crevette = new Hero("Crevette", 35, 10, 10, 20, false);
        Crevette.AddSkill(skillDB.GetSkillByName("Attaque"));
        Crevette.AddSkill(skillDB.GetSkillByName("Frappe puissante"));
        Crevette.AddSkill(skillDB.GetSkillByName("Soin léger"));

        Hero Oursin = new Hero("Oursin",35, 10, 10, 20, false);
        Oursin.AddSkill(skillDB.GetSkillByName("Boule de feu"));
        Oursin.AddSkill(skillDB.GetSkillByName("Soin magique"));
        
        Hero Crabe = new Hero("Crabe", 35, 10, 10, 20, false);
        Crabe.AddSkill(skillDB.GetSkillByName("Soin magique"));
        
        Hero Poulpe = new Hero("Poulpe", 35, 10, 10, 20, false);
        Poulpe.AddSkill(skillDB.GetSkillByName("Soin léger"));
       
        Hero Seche = new Hero("Seche", 35, 10, 10, 20, false);
        Seche.AddSkill(skillDB.GetSkillByName("Soin léger"));
        
        Hero Bernard = new Hero("Bernard", 35, 10, 10, 20, false);
        Bernard.AddSkill(skillDB.GetSkillByName("Soin léger"));

        // --- Enemies --- 
        Hero Banane = new Hero("Banane", 35, 2, 10, 25, true);
        Banane.AddSkill(skillDB.GetSkillByName("Soin léger"));

        allHeros.AddRange(new[] { Crevette, Oursin, Crabe, Poulpe, Seche, Bernard, Banane });

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
        battleUI.OnSkillSelected += HandleSkillChoice;
    }

    void HandleSkillChoice(Skill chosenSkill)
    {
        battleUI.HideAll();
        battleUI.OnSkillSelected -= HandleSkillChoice;

        // Choisir une cible (simple pour l’instant)
        Hero target = chosenSkill.TargetEnemy
            ? allHeros.FirstOrDefault(c => c.IsEnemy && c.IsAlive())
            : current; // auto-soin simple

        if (target != null)
        {
            chosenSkill.Use(current, target);
        }

        NextTurn();
    }

    void EnemyAction(Hero enemy)
    {
        Skill chosenSkill = enemy.Skills[Random.Range(0, enemy.Skills.Count)];
        Hero target = chosenSkill.TargetEnemy
            ? allHeros.FirstOrDefault(c => !c.IsEnemy && c.IsAlive())
            : enemy;
        chosenSkill.Use(enemy, target);
        Invoke(nameof(NextTurn), 1.5f);
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
}
