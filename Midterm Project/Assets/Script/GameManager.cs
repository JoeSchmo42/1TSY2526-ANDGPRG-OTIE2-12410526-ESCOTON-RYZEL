using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int cheeseCount = 0;
    public int totalCheese = 5;
    public int currentLevel = 1;
    public int playerHealth = 5;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log($"GameManager initialized. Total cheese: {totalCheese}, Health: {playerHealth}, Level: {currentLevel}");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectCheese()
    {
        cheeseCount++;
        Debug.Log($"Cheese collected. Current count: {cheeseCount}/{totalCheese}");
        if (cheeseCount >= totalCheese)
        {
            Debug.Log($"Level {currentLevel} complete! Moving to Level {currentLevel + 1}");
            currentLevel++;
            cheeseCount = 0;
            string nextScene = "Level" + currentLevel;
            if (SceneManager.GetSceneByName(nextScene).IsValid())
            {
                SceneManager.LoadScene(nextScene);
            }
            else
            {
                Debug.Log("No more levels! Game won!");
                Application.Quit(); // End game if no more levels
            }
        }
    }

    public void TakeDamage()
    {
        playerHealth--;
        Debug.Log($"Player hit! Health remaining: {playerHealth}/5");
        if (playerHealth <= 0)
        {
            Debug.Log("GAME OVER! Quitting game...");
            Application.Quit(); // End the game
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in Editor
#endif
        }
    }
}