using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    public int level = 1;
    public const int MAX_LEVEL = 5;
    TowerAttack attack;

    void Start()
    {
        attack = GetComponent<TowerAttack>();
    }

    public void Upgrade()
    {
        if (level >= MAX_LEVEL) return;
        int cost = level * 150;
        if (!GameManager.Instance.SpendGold(cost)) return;

        level++;
        ApplyUpgrade();
        transform.localScale *= 1.15f;

        UI_TowerSelection.Instance.Refresh();
    }

    void ApplyUpgrade()
    {
        float multiplier = 1f + (level - 1) * 0.4f;

        attack.data.damage = Mathf.RoundToInt(attack.data.damage * 1.4f);
        attack.data.range += 1f;
        attack.data.attackSpeed *= 0.9f;

        if (attack.data.splashRadius > 0) attack.data.splashRadius += 0.8f;
        if (attack.data.slowDuration > 0) attack.data.slowDuration += 1f;
        if (attack.data.burnDPS > 0) attack.data.burnDPS *= 1.5f;
    }

    void Update()
    {
        Renderer r = GetComponentInChildren<Renderer>();
        int cost = level * 150;
        if (level < 5 && GameManager.Instance.gold >= cost)
            r.material.color = new Color(0, 1, 0, 0.8f);  // Green glow
        else
            r.material.color = Color.white;
    }
}
