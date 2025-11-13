using Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Vie")]
    [SerializeField] private Image healthFillImage;   // Image pour la barre de vie
    [SerializeField] private TextMeshProUGUI healthText;         // Texte pour afficher PV actuel / max

    [Header("Charge Ultime")]
    [SerializeField] private Image ultiFillImage;     // Image pour la barre d'ulti

    [SerializeField] private float smoothSpeed = 5f;

    private float targetHealthFill = 1f;
    
    public Hero hero;

    /// <summary>
    /// Met à jour la barre de vie et le texte.
    /// </summary>
    public void SetHealth(int current, int max)
    {
        targetHealthFill = (float)current / max;
        healthText.text = $"{current}/{max}";
        UpdateHealthColor(targetHealthFill);
    }
    

    void Update()
    {
        healthFillImage.fillAmount = Mathf.Lerp(healthFillImage.fillAmount, targetHealthFill, Time.deltaTime * smoothSpeed);
        ultiFillImage.fillAmount = Mathf.Lerp(ultiFillImage.fillAmount, (float)hero.UltiCharges/3, Time.deltaTime * smoothSpeed);
    }

    private void UpdateHealthColor(float value)
    {
        healthFillImage.color = Color.Lerp(Color.red, Color.green, value);
    }
}