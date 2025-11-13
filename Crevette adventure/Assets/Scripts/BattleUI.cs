using System;
using System.Collections.Generic;
using Scripts.Skills;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class BattleUI : MonoBehaviour
    {
        public GameObject buttonPrefab; // un prefab de bouton (ex: ton ancien AttackButton)
        public Transform buttonContainer; // un GameObject vide pour les placer
        private List<Button> currentButtons = new List<Button>();

        public event Action<Skill> OnSkillSelected;
        public event Action<Hero> OnTargetSelected;

        public void ShowSkills(List<Skill> skills)
        {
            ClearButtons();

            foreach (Skill skill in skills)
            {
                GameObject btnObj = Instantiate(buttonPrefab, buttonContainer);
                Button btn = btnObj.GetComponent<Button>();
                TMP_Text btnText = btn.GetComponentInChildren<TMP_Text>();
                btnText.text = skill.SkillData.Title;

                btn.onClick.AddListener(() => OnSkillSelected?.Invoke(skill));
                currentButtons.Add(btn);
            }
        }

        public void HideAll()
        {
            ClearButtons();
        }

        void ClearButtons()
        {
            foreach (Button btn in currentButtons)
            {
                Destroy(btn.gameObject);
            }
            currentButtons.Clear();
        }
        
        

        public void ShowTargets(List<Hero> targets)
        {
            ClearButtons();

            foreach (Hero target in targets)
            {
                GameObject btnObj = Instantiate(buttonPrefab, buttonContainer);
                Button btn = btnObj.GetComponent<Button>();
                TMP_Text btnText = btn.GetComponentInChildren<TMP_Text>();
                btnText.text = target.Name;

                btn.onClick.AddListener(() => OnTargetSelected?.Invoke(target));
                currentButtons.Add(btn);
            }
        }


    }
}


