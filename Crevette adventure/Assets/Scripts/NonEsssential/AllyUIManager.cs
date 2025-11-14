using UnityEngine;
using System.Collections.Generic;

public partial class AllyUIManager : MonoBehaviour
{
    [Header("Références UI")]
    public GameObject allyUIPrefab; // Le prefab contenant le nom + barre de vie
    public Transform uiContainer;   // Le parent dans le Canvas (ex: un Vertical Layout Group)

    // Dictionnaire pour suivre les UI associées à chaque allié
    private Dictionary<GameObject, AllyUI> allyUIDictionary = new Dictionary<GameObject, AllyUI>();

    /// <summary>
    /// Enregistre un nouvel allié et crée son UI.
    /// </summary>
    public void RegisterAlly(GameObject ally, string name, float maxHealth)
    {
        if (allyUIDictionary.ContainsKey(ally)) return;

        GameObject uiElement = Instantiate(allyUIPrefab, uiContainer);
        AllyUI ui = uiElement.GetComponent<AllyUI>();

        if (ui != null)
        {
            ui.SetName(name);
            ui.SetHealth(1f); // 100% au départ
            allyUIDictionary.Add(ally, ui);
        }
        else
        {
            Debug.LogError("Le prefab AllyUIPrefab n'a pas de script AllyUI attaché !");
        }
    }

    /// <summary>
    /// Met à jour la barre de vie d’un allié.
    /// </summary>
    public void UpdateHealth(GameObject ally, float currentHealth, float maxHealth)
    {
        if (allyUIDictionary.TryGetValue(ally, out AllyUI ui))
        {
            float normalizedHealth = Mathf.Clamp01(currentHealth / maxHealth);
            ui.SetHealth(normalizedHealth);
        }
    }
}