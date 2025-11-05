using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TeamSelectManager : MonoBehaviour
{
    [Header("UI")]
    public Transform availableCharactersContainer; // Où apparaissent les boutons de personnages
    public GameObject characterButtonPrefab;       // Prefab de bouton de perso (image + texte)
    public Transform selectedTeamContainer;        // Où afficher les portraits choisis
    public Button fightButton;

    [Header("Personnages disponibles pour le joueur")]
    public List<Sprite> playerCharacterPortraits; // Liste de tous les personnages jouables
    public List<string> playerCharacterNames;

    [Header("Équipe adverse fixe")]
    public List<Sprite> enemyCharacterPortraits; // Les 4 portraits adverses fixes
    public List<string> enemyCharacterNames;

    private List<int> selectedPlayerIndices = new List<int>();
    private const int maxTeamSize = 4;

    void Start()
    {
        PopulateCharacterButtons();
        fightButton.onClick.AddListener(OnFight);
        UpdateTeamDisplay();
    }

    void PopulateCharacterButtons()
    {
        for (int i = 0; i < playerCharacterPortraits.Count; i++)
        {
            int index = i;
            GameObject btnObj = Instantiate(characterButtonPrefab, availableCharactersContainer);
            btnObj.GetComponentInChildren<Image>().sprite = playerCharacterPortraits[i];
            btnObj.GetComponentInChildren<Text>().text = playerCharacterNames[i];
            btnObj.GetComponent<Button>().onClick.AddListener(() => OnCharacterSelected(index));
        }
    }

    void OnCharacterSelected(int index)
    {
        if (selectedPlayerIndices.Contains(index))
        {
            selectedPlayerIndices.Remove(index);
        }
        else
        {
            if (selectedPlayerIndices.Count >= maxTeamSize)
            {
                Debug.Log("Équipe déjà complète !");
                return;
            }
            selectedPlayerIndices.Add(index);
        }
        UpdateTeamDisplay();
    }

    void UpdateTeamDisplay()
    {
        foreach (Transform child in selectedTeamContainer)
            Destroy(child.gameObject);

        foreach (int idx in selectedPlayerIndices)
        {
            GameObject imgObj = new GameObject("SelectedPortrait", typeof(Image));
            imgObj.transform.SetParent(selectedTeamContainer);
            imgObj.GetComponent<Image>().sprite = playerCharacterPortraits[idx];
        }

        fightButton.interactable = selectedPlayerIndices.Count == maxTeamSize;
    }

    void OnFight()
    {
        if (selectedPlayerIndices.Count < maxTeamSize)
        {
            Debug.Log("Équipe incomplète !");
            return;
        }

        // Sauvegarde l’équipe du joueur
        for (int i = 0; i < maxTeamSize; i++)
            PlayerPrefs.SetInt("PlayerTeam_" + i, selectedPlayerIndices[i]);

        // Sauvegarde l’équipe ennemie fixe
        for (int i = 0; i < enemyCharacterPortraits.Count && i < maxTeamSize; i++)
            PlayerPrefs.SetInt("EnemyTeam_" + i, i);

        SceneManager.LoadScene("CombatScene");
    }
}