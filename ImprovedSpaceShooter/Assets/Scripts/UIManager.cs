using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Text gameOverText;

    void Start()
    {
        // Initialize sliders with current volumes
        if (bgmSlider != null)
        {
            bgmSlider.value = AudioManager.Instance.bgmVolume;
            bgmSlider.onValueChanged.AddListener(AudioManager.Instance.SetBGMVolume);
        }
        if (sfxSlider != null)
        {
            sfxSlider.value = AudioManager.Instance.sfxVolume;
            sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
        }
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
    }

    public void ShowGameOver()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
        }
    }
}