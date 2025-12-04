using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    public int level = 1;
    TowerAttack attack;

    void Start() => attack = GetComponent<TowerAttack>();

    public void Upgrade()
    {
        if (level >= 2) return;
        if (GameManager.Instance.gold < 100) return;

        level = 2;
        GameManager.Instance.SpendGold(100);
        attack.damage *= 2;
        attack.range += 2;
        transform.localScale *= 1.3f;
    }
}
