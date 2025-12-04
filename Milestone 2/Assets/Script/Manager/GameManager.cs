using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject core;
    public GameObject Core => core;

    public int gold = 300;
    public Text goldText;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        goldText.text = gold.ToString();   // Shows only "500", "1230", etc. – no "Gold: "
    }

    public void AddGold(int amount) => gold += amount;
    public void SpendGold(int amount) => gold -= amount;
}
