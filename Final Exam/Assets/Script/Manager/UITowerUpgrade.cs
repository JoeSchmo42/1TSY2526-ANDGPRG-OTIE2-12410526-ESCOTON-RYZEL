using UnityEngine;
using UnityEngine.UI;

public class UI_TowerSelection : MonoBehaviour
{
    public static UI_TowerSelection Instance;

    [Header("UI References - Drag from your existing UI")]
    public Text towerInfoText;        
    public Button upgradeButton;      
    public Text upgradeButtonText;    

    private TowerUpgrade currentSelectedTower;

    private void Awake()
    {
        Instance = this;
        ClearSelection();
    }

    public void SelectTower(TowerUpgrade tower)
    {
        currentSelectedTower = tower;

        TowerAttack attack = tower.GetComponent<TowerAttack>();
        string towerName = attack.data.towerName;

        towerInfoText.text = $"{towerName} Tower\nLevel: {tower.level}";

        // Calculate next upgrade cost
        int nextCost = tower.level * 150;

        if (tower.level >= 5)
        {
            towerInfoText.text += "\n(MAX LEVEL)";
            upgradeButton.interactable = false;
        }
        else if (GameManager.Instance.gold >= nextCost)
        {
            towerInfoText.text += $"\nUpgrade Cost: {nextCost}";
            upgradeButton.interactable = true;
        }
        else
        {
            towerInfoText.text += $"\n<color=red>Not enough gold ({nextCost} needed)</color>";
            upgradeButton.interactable = false;
        }

        // Hook button click
        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(() => tower.Upgrade());
    }

    public void ClearSelection()
    {
        currentSelectedTower = null;
        towerInfoText.text = "Selected/build Tower Info or skill info goes here if(selected tower is null hide this panel also add gold or cooldown here information here.";
        upgradeButton.interactable = false;
    }

    
    public void Refresh()
    {
        if (currentSelectedTower != null)
            SelectTower(currentSelectedTower);
    }
}
