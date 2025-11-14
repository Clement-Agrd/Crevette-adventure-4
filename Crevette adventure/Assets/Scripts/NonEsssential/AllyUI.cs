using UnityEngine;
using UnityEngine.UI;
using TMPro;

public partial class AllyUI : MonoBehaviour
{
    [Header("Références")]
    public TextMeshProUGUI nameText;
    public Image healthFill;

    /// <summary>
    /// Met à jour le nom du personnage.
    /// </summary>
    public void SetName(string allyName)
    {
        if (nameText != null)
            nameText.text = allyName;
    }

    /// <summary>
    /// Met à jour la barre de vie (entre 0 et 1).
    /// </summary>
    public void SetHealth(float normalizedValue)
    {
        if (healthFill != null)
            healthFill.fillAmount = normalizedValue;
    }
}