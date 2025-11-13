using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private float smoothSpeed = 5f;

    private float targetFill = 1f;

    public void SetHealth(int current, int max)
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
        // Vert -> Rouge
        fillImage.color = Color.Lerp(Color.red, Color.green, value);
    }
}