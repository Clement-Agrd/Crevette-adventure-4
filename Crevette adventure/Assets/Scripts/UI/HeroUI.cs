
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : MonoBehaviour
{
    [SerializeField] private Image healthFill;
    [SerializeField] private Image ultiFill;
    [SerializeField] private Image portraitImage;
    [SerializeField] private float smoothSpeed = 5f;

    private float targetHealthFill = 1f;
    private float targetUltiFill = 1f;

    public void SetHealth(int current, int max)
    {
        targetHealthFill = (float)current / max;
        healthFill.color = Color.Lerp(Color.red, Color.green, targetHealthFill);
    }

    public void SetUlti(int current, int max)
    {
        targetUltiFill = (float)current / max;
        ultiFill.color = Color.Lerp(Color.blue, Color.cyan, targetUltiFill);
    }

    public void SetPortrait(Sprite portrait)
    {
        if (portraitImage != null)
            portraitImage.sprite = portrait;
    }

    void Update()
    {
        healthFill.fillAmount = Mathf.Lerp(healthFill.fillAmount, targetHealthFill, Time.deltaTime * smoothSpeed);
        ultiFill.fillAmount = Mathf.Lerp(ultiFill.fillAmount, targetUltiFill, Time.deltaTime * smoothSpeed);
    }
}
