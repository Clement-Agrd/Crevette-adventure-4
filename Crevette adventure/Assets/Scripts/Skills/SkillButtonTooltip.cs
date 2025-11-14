using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SkillButtonTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text tooltipText; // Le texte du tooltip
    public GameObject tooltipPanel; // Le panel du tooltip
    private string description;

    public void SetDescription(string desc)
    {
        description = desc;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipPanel.SetActive(true);
        tooltipText.text = description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipPanel.SetActive(false);
    }
}