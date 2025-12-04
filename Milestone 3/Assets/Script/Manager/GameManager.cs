using UnityEngine;
using UnityEngine.UI;   

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Core")]
    public GameObject core;
    public int coreLives = 20;               

    [Header("Gold")]
    public int gold = 500;
    public Text goldText;                    

    [Header("Wave UI")]
    public Text waveText;                    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGoldUI();
        UpdateWaveUI(1);                     // Start at wave 1
    }

    
    public void UpdateGoldUI()
    {
        goldText.text = gold.ToString();
    }

    
    public void UpdateWaveUI(int currentWave)
    {
        waveText.text = "Wave " + currentWave;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateGoldUI();
    }

    public bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            UpdateGoldUI();
            return true;
        }
        return false;
    }

    public void DamageCore(int damage = 1)
    {
        coreLives -= damage;

        if (coreLives <= 0)
        {
            coreLives = 0;
            Debug.Log("GAME OVER - Core destroyed!");

        }
    }

    public GameObject Core => core;
}
