using Scripts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // <- nécessaire pour les événements

namespace HeroSelectionMenu
{
    public class HeroToggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] 
        private Image image;

        private CharacterSelectionMenu manager;
        private HeroData heroData;

        public void SetData(CharacterSelectionMenu menu, HeroData data)
        {
            manager = menu;
            heroData = data;

            image.sprite = data.Portrait;
        }

        public void OnSelect(bool isOn)
        {
            if (isOn)
            {
                bool success = manager.SelectHero(heroData);
                if (!success)
                    GetComponent<Toggle>().isOn = false;
            }
            else
            {
                manager.UnselectHero(heroData);
            }
        }

        // ---------------- Tooltip ----------------
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (heroData != null)
                HeroTooltip.Instance.Show(heroData.Description);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HeroTooltip.Instance.Hide();
        }
    }
}