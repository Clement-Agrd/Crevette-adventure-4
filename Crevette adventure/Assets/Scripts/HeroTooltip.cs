using UnityEngine;
using TMPro;

public class HeroTooltip : MonoBehaviour
{
    public static HeroTooltip Instance;

    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private TMP_Text tooltipText; // <-- Utilise TMP_Text

    private void Awake()
    {
        Instance = this;
        tooltipPanel.SetActive(false);
    }

    private void Update()
    {
        if (tooltipPanel.activeSelf)
        {
            Vector2 position;
        }
    }

    public void Show(string description)
    {
        tooltipText.text = description;
        tooltipPanel.SetActive(true);
    }

    public void Hide()
    {
        tooltipPanel.SetActive(false);
    }
}