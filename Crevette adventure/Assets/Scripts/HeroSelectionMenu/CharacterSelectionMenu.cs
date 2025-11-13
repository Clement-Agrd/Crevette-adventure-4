
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using HeroSelectionMenu;
using Scripts;

public class CharacterSelectionMenu : MonoBehaviour
{
    [Header("Bouton Jouer")]
    public Button playButton; // Bouton pour lancer le jeu

    [SerializeField] private Transform container;
    [SerializeField] private GameObject heroTogglePrefab;
    
    private List<HeroData> selectedCharacter = new();

    void Start()
    {
        // Vérifie que les éléments sont bien assignés
        if (playButton != null)
            playButton.onClick.AddListener(OnPlayButtonClicked);

        UpdateSelectedCharactersUI();

        HeroData[] allHeroes = Resources.LoadAll<HeroData>("Heroes");

        foreach (Transform child in container)
            Destroy(child.gameObject);
        
        foreach (HeroData hero in allHeroes)
        {
            if(hero.IsEnemy)
                continue;
            
            GameObject instance = Instantiate(heroTogglePrefab, container);
            HeroToggle toggle = instance.GetComponent<HeroToggle>();

            if (toggle != null)
            {
                toggle.SetData(this, hero);
            }
        }
    }

   
    public bool SelectHero(HeroData hero)
    {
        if (selectedCharacter.Count >= 4)
            return false; // Sélection refusée

        selectedCharacter.Add(hero);
        UpdateSelectedCharactersUI();
        return true; // Sélection acceptée
    }


    public void UnselectHero(HeroData hero)
    {
        selectedCharacter.Remove(hero);
        UpdateSelectedCharactersUI();
    }

    void UpdateSelectedCharactersUI()
    {
        if (playButton != null)
            playButton.interactable = selectedCharacter.Count == 4;
    }

    void OnPlayButtonClicked()
    {
        // Tu peux stocker les indices sélectionnés ici si besoin
        // Exemple : PlayerPrefs.SetInt("SelectedCharacter1", selectedCharacterIndices[0]);

        BattleSystem.heroes = selectedCharacter.ToArray();
        
        SceneManager.LoadScene("CombatScene"); // Remplace par le nom réel de ta scène
    }
}
