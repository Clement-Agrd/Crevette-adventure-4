
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CharacterSelectionMenu : MonoBehaviour
{
    [Header("Boutons des personnages")]
    public List<Button> characterButtons; // Boutons cliquables pour chaque personnage

    [Header("Slots de sélection")]
    public List<Image> selectedCharacterSlots; // Emplacements pour afficher les personnages choisis

    [Header("Bouton Jouer")]
    public Button playButton; // Bouton pour lancer le jeu

    private List<int> selectedCharacterIndices = new List<int>();

    void Start()
    {
        // Vérifie que les éléments sont bien assignés
        if (playButton != null)
            playButton.onClick.AddListener(OnPlayButtonClicked);

        for (int i = 0; i < characterButtons.Count; i++)
        {
            int index = i;
            if (characterButtons[i] != null)
                characterButtons[i].onClick.AddListener(() => OnCharacterButtonClicked(index));
        }

        UpdateSelectedCharactersUI();
    }

    void OnCharacterButtonClicked(int index)
    {
        if (selectedCharacterIndices.Contains(index))
        {
            selectedCharacterIndices.Remove(index);
        }
        else if (selectedCharacterIndices.Count < selectedCharacterSlots.Count)
        {
            selectedCharacterIndices.Add(index);
        }

        UpdateSelectedCharactersUI();
    }

    void UpdateSelectedCharactersUI()
    {
        for (int i = 0; i < selectedCharacterSlots.Count; i++)
        {
            if (i < selectedCharacterIndices.Count)
            {
                int characterIndex = selectedCharacterIndices[i];
                Image characterImage = characterButtons[characterIndex].GetComponent<Image>();

                if (characterImage != null)
                {
                    selectedCharacterSlots[i].sprite = characterImage.sprite;
                    selectedCharacterSlots[i].enabled = true;
                }
            }
            else
            {
                selectedCharacterSlots[i].sprite = null;
                selectedCharacterSlots[i].enabled = false;
            }
        }

        if (playButton != null)
            playButton.interactable = selectedCharacterIndices.Count > 0;
    }

    void OnPlayButtonClicked()
    {
        // Tu peux stocker les indices sélectionnés ici si besoin
        // Exemple : PlayerPrefs.SetInt("SelectedCharacter1", selectedCharacterIndices[0]);

        SceneManager.LoadScene("CombatScene"); // Remplace par le nom réel de ta scène
    }
}
