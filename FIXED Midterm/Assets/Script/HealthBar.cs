using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            slider.maxValue = 5f;
            SetHealth(GameManager.Instance.playerHealth);
            GameManager.OnHealthChanged += OnHealthChangedHandler;
        }
    }

    private void OnDestroy()
    {
        GameManager.OnHealthChanged -= OnHealthChangedHandler;
    }

    private void OnHealthChangedHandler()
    {
        SetHealth(GameManager.Instance.playerHealth);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}