
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private float smoothSpeed = 5f;

    private float targetFill = 1f;

    public void SetMana(int current, int max)
    {
        targetFill = (float)current / max;
        UpdateColor(targetFill);
    }

    void Update()
    {
        fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, targetFill, Time.deltaTime * smoothSpeed);
    }

    private void UpdateColor(float value)
    {
        // Bleu -> Violet (plus mana = bleu, moins mana = violet)
        Color startColor = new Color(0.5f, 0f, 1f); // Violet
        Color endColor = new Color(0f, 0.5f, 1f);   // Bleu
        fillImage.color = Color.Lerp(startColor, endColor, value);
    }
}

