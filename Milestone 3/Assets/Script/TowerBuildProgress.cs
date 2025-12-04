using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerBuildProgress : MonoBehaviour
{
    public Slider progressSlider;
    TowerData data;

    public void StartBuilding(TowerData towerData)
    {
        data = towerData;

        GameObject canvas = new GameObject("BuildCanvas");
        canvas.transform.SetParent(transform);
        Canvas c = canvas.AddComponent<Canvas>();
        c.renderMode = RenderMode.WorldSpace;
        canvas.transform.localScale = Vector3.one * 0.02f;
        canvas.transform.localPosition = new Vector3(0, 3, 0);

        GameObject sliderObj = new GameObject("Slider");
        sliderObj.transform.SetParent(canvas.transform);
        progressSlider = sliderObj.AddComponent<Slider>();
        progressSlider.minValue = 0;
        progressSlider.maxValue = 1;
        progressSlider.value = 0;
        RectTransform rt = progressSlider.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(4, 0.5f);

        StartCoroutine(BuildRoutine());
    }

    IEnumerator BuildRoutine()
    {
        float timer = 0;
        while (timer < data.buildTime)
        {
            timer += Time.deltaTime;
            progressSlider.value = timer / data.buildTime;
            yield return null;
        }

        FinishBuilding();
    }

    void FinishBuilding()
    {
        Destroy(progressSlider.transform.parent.gameObject);

        GetComponentInChildren<Renderer>().material.color = Color.white;
        GetComponent<Collider>().enabled = true;
        foreach (var comp in GetComponents<MonoBehaviour>())
            if (comp != this) comp.enabled = true;

        gameObject.AddComponent<TowerAttack>().Initialize(data);
        gameObject.AddComponent<TowerUpgrade>();
        Destroy(this);
    }
}