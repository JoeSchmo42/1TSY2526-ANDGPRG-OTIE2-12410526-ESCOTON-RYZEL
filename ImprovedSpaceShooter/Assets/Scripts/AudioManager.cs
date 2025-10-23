using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioClip deathSFX;
    public AudioClip gameOverSFX;
    public AudioClip levelCompleteSFX;
    public AudioClip bgmClip;
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Range(0f, 1f)]
    public float bgmVolume = 0.5f;
    [Range(0f, 1f)]
    public float sfxVolume = 0.7f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Ensure AudioSources are set up
        if (bgmSource == null)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.loop = true;
        }
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {
        // Assign and play BGM
        if (bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            Debug.Log($"BGM assigned: {bgmClip.name}");
        }
        else
        {
            Debug.LogWarning("No BGM clip assigned in AudioManager");
        }
        UpdateVolumes();
        PlayBGM();
    }

    public void PlayDeathSFX(Vector3 position)
    {
        if (deathSFX != null)
        {
            AudioSource.PlayClipAtPoint(deathSFX, position, sfxVolume);
        }
    }

    public void TriggerGameOver()
    {
        if (gameOverSFX != null)
        {
            sfxSource.PlayOneShot(gameOverSFX, sfxVolume);
        }
    }

    public void PlayLevelCompleteSFX()
    {
        if (levelCompleteSFX != null)
        {
            sfxSource.PlayOneShot(levelCompleteSFX, sfxVolume);
        }
    }

    public void PlayBGM()
    {
        if (bgmSource.clip != null && !bgmSource.isPlaying)
        {
            bgmSource.Play();
            Debug.Log("BGM started playing");
        }
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        UpdateVolumes();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        UpdateVolumes();
    }

    void UpdateVolumes()
    {
        bgmSource.volume = bgmVolume;
        sfxSource.volume = sfxVolume;
    }
}