using UnityEngine;
using System.Collections.Generic;
using Scripts;

public class HeroUIManager : MonoBehaviour
{
    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Canvas uiCanvas;

    private Dictionary<Hero, HealthBar> heroBars = new Dictionary<Hero, HealthBar>();
    private Dictionary<Hero, GameObject> heroObjects;

    public void Initialize(Dictionary<Hero, GameObject> heroObjects)
    {
        this.heroObjects = heroObjects;

        foreach (var pair in heroObjects)
        {
            Hero hero = pair.Key;
            CreateHealthBar(pair.Value, hero);
        }
    }

    private void CreateHealthBar(GameObject hero, Hero herodata)
    {
        GameObject barObj = Instantiate(healthBarPrefab, hero.transform.position + new Vector3(0, 5, 0),
            Quaternion.identity);
        HealthBar bar = barObj.GetComponent<HealthBar>();
        heroBars.Add(herodata, bar);
        barObj.transform.SetParent(uiCanvas.transform);
        barObj.transform.position = hero.transform.position + new Vector3(0, 5, 0);

        bar.SetHealth(herodata.CurrentHealth, herodata.MaxHealth);

        herodata.OnDamaged += (dmg, from) => bar.SetHealth(herodata.CurrentHealth, herodata.MaxHealth);
        herodata.OnHealed += (heal, from) => bar.SetHealth(herodata.CurrentHealth, herodata.MaxHealth);
    }


    void LateUpdate()
    {
        foreach (var pair in heroBars)
        {
            Hero hero = pair.Key;
            GameObject heroObj = heroObjects[hero];
            HealthBar bar = pair.Value;

            Vector3 screenPos = Camera.main.WorldToScreenPoint(heroObj.transform.position + Vector3.up * 2f);
            Vector2 uiPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                uiCanvas.GetComponent<RectTransform>(),
                screenPos,
                uiCanvas.worldCamera,
                out uiPos
            );

            bar.GetComponent<RectTransform>().anchoredPosition = uiPos;
        }
    }
}