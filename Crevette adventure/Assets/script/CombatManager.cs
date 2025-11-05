using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour
{
    [Header("UI Containers")]
    public Transform playerTeamContainer;
    public Transform enemyTeamContainer;
    public GameObject portraitPrefab;

    [Header("Personnages")]
    public List<Sprite> playerCharacterPortraits;
    public List<Sprite> enemyCharacterPortraits;

    private const int teamSize = 4;

    void Start()
    {
        // Charge l’équipe du joueur
        for (int i = 0; i < teamSize; i++)
        {
            int playerIndex = PlayerPrefs.GetInt("PlayerTeam_" + i);
            CreatePortrait(playerTeamContainer, playerCharacterPortraits[playerIndex]);
        }

        // Charge l’équipe ennemie (fixe)
        for (int i = 0; i < enemyCharacterPortraits.Count && i < teamSize; i++)
        {
            CreatePortrait(enemyTeamContainer, enemyCharacterPortraits[i]);
        }
    }

    void CreatePortrait(Transform parent, Sprite sprite)
    {
        GameObject imgObj = Instantiate(portraitPrefab, parent);
        imgObj.GetComponent<Image>().sprite = sprite;
    }
}