using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public BattleUI battleUI;
    private List<Hero> allHeros = new List<Hero>();
    private int currentTurnIndex = 0;
    private Hero current;

    void Start()
    {
        // --- Héros ---
        Hero Crevette = new Hero("Crevette", 35, 10, false);
        Crevette.AddSkill(new Skill("Attaque", "Une attaque normale", 5, 8));
        Crevette.AddSkill(new Skill("Frappe puissante", "Une attaque plus forte", 8, 12));
        Crevette.AddSkill(new Skill("Soin léger", "Soigne un allié", 5, 10, false));

        Hero Oursin = new Hero("Oursin", 25, 8, false);
        Oursin.AddSkill(new Skill("Boule de feu", "Inflige des dégâts de feu", 7, 12));
        Oursin.AddSkill(new Skill("Soin magique", "Restaure la vie d'un allié", 6, 10, false));
        
        Hero Crabe = new Hero("Crabe", 20, 6, false);
        Crabe.AddSkill(new Skill("Coup de dague", "Attaque rapide", 4, 7));
        
        Hero Poulpe = new Hero("Poulpe", 20, 6, false);
        Poulpe.AddSkill(new Skill("Coup de dague", "Attaque rapide", 4, 7));
       
        Hero Seche = new Hero("Seche", 20, 6, false);
        Seche.AddSkill(new Skill("Coup de dague", "Attaque rapide", 4, 7));
        
        Hero Bernard = new Hero("Bernard", 20, 6, false);
        Bernard.AddSkill(new Skill("Coup de dague", "Attaque rapide", 4, 7));

        // --- Enemies --- 
        Hero Banane = new Hero("Banane", 30, 7, true);
        Banane.AddSkill(new Skill("Coup brutal", "Attaque puissante", 6, 10));

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
