using UnityEngine;

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    private List<heros> allheross = new List<heros>();
    private int currentTurnIndex = 0;

    void Start()
    {
        // Exemple de setup de combat
        allheross.Add(new heros("Crevette", 30, 10, false));
        allheross.Add(new heros("Oursin", 30, 10, false));
        allheross.Add(new heros("Crabe", 30, 10, false));
        allheross.Add(new heros("Poulpe", 30, 10, false));
        allheross.Add(new heros("Sèche", 30, 10, false));
        allheross.Add(new heros("Bernard", 30, 10, false));
        allheross.Add(new heros("Citron", 30, 10, true));
        

        // Tri des tours par vitesse décroissante
        allheross = allheross.OrderByDescending(c => c.Speed).ToList();

        StartTurn();
    }

    void StartTurn()
    {
        if (IsBattleOver()) return;

        heros current = allheross[currentTurnIndex];
        if (!current.IsAlive())
        {
            NextTurn();
            return;
        }

        Debug.Log($"C'est le tour de {current.Name} !");
        
        if (current.IsEnemy)
        {
            EnemyAction(current);
        }
        else
        {
            // Dans une vraie version, on attendra une entrée du joueur ici
            PlayerAction(current);
        }
    }

    void PlayerAction(heros player)
    {
        // Pour le test : attaquer un ennemi au hasard
        heros target = allheross.FirstOrDefault(c => c.IsEnemy && c.IsAlive());
        if (target != null)
        {
            target.TakeDamage(Random.Range(5, 10));
        }
        NextTurn();
    }

    void EnemyAction(heros enemy)
    {
        heros target = allheross.FirstOrDefault(c => !c.IsEnemy && c.IsAlive());
        if (target != null)
        {
            target.TakeDamage(Random.Range(3, 7));
        }
        NextTurn();
    }

    void NextTurn()
    {
        currentTurnIndex++;
        if (currentTurnIndex >= allheross.Count)
        {
            currentTurnIndex = 0; // nouvelle manche
        }

        Invoke(nameof(StartTurn), 1f); // délai entre les tours
    }

    bool IsBattleOver()
    {
        bool allPlayersDead = !allheross.Any(c => !c.IsEnemy && c.IsAlive());
        bool allEnemiesDead = !allheross.Any(c => c.IsEnemy && c.IsAlive());

        if (allPlayersDead)
        {
            Debug.Log("Tous les héros sont morts... Défaite !");
            return true;
        }

        if (allEnemiesDead)
        {
            Debug.Log("Victoire ! Tous les ennemis sont vaincus !");
            return true;
        }

        return false;
    }
}

