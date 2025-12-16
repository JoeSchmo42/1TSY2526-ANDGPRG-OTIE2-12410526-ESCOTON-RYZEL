using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int cheeseCount = 0;
    public int totalCheese = 5;
    public int currentLevel = 1;
    public int maxLevels = 5;
    public int playerHealth = 5;
    public AudioClip hitSound;
    public AudioClip deathSound;
    public static event Action OnHealthChanged;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.Log($"GameManager initialized. Total cheese: {totalCheese}, Health: {playerHealth}, Level: {currentLevel}");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource && clip)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void CollectCheese()
    {
        cheeseCount++;
        Debug.Log($"Cheese collected. Current count: {cheeseCount}/{totalCheese}");
        if (cheeseCount >= totalCheese)
        {
            Debug.Log($"Level {currentLevel} complete!");
            currentLevel++;
            cheeseCount = 0;
            playerHealth = 5;
            OnHealthChanged?.Invoke();
            if (currentLevel > maxLevels)
            {
                Debug.Log("Congratulations! You won the game!");
                Time.timeScale = 0f;
                GameObject winPanel = GameObject.Find("WinPanel");
                if (winPanel)
                {
                    winPanel.SetActive(true);
                }
            }
            else
            {
                Debug.Log($"Moving to Level {currentLevel}");
                SceneManager.LoadScene("Level" + currentLevel);
            }
        }
    }

    public void TakeDamage()
    {
        playerHealth--;
        PlaySound(hitSound);
        OnHealthChanged?.Invoke();
        Debug.Log($"Player hit! Health remaining: {playerHealth}/5");
        if (playerHealth <= 0)
        {
            PlaySound(deathSound);
            Time.timeScale = 0f;
            GameObject player = GameObject.FindWithTag("Player");
            if (player)
            {
                Destroy(player);
            }
            GameObject gameOverPanel = GameObject.Find("GameOverPanel");
            if (gameOverPanel)
            {
                gameOverPanel.SetActive(true);
            }
            Debug.Log("GAME OVER!");
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        cheeseCount = 0;
        playerHealth = 5;
        OnHealthChanged?.Invoke();
        SceneManager.LoadScene("Level" + currentLevel);
    }

    public void RestartGame()
    {
        currentLevel = 1;
        Time.timeScale = 1f;
        cheeseCount = 0;
        playerHealth = 5;
        OnHealthChanged?.Invoke();
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}