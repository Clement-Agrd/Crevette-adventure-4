using Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace HeroSelectionMenu
{
    public class HeroToggle : MonoBehaviour
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
                {
                    // Si la sélection échoue, on désactive le toggle
                    GetComponent<Toggle>().isOn = false;
                }
            }
            else
            {
                manager.UnselectHero(heroData);
            }
        }

    }
}