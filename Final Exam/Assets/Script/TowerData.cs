using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Tower Defense/Tower")]
public class TowerData : ScriptableObject
{
    public string towerName;
    public GameObject prefab;
    public Sprite icon;
    public int cost = 100;
    public float buildTime = 10f;

    public float damage = 30;
    public float range = 8f;
    public float attackSpeed = 1f;
    public float splashRadius = 0f;
    public float slowAmount = 0.5f;
    public float slowDuration = 3f;
    public float burnDPS = 0f;
    public float burnDuration = 4f;

    public bool canTargetFlying = true;
    public bool isSplash = false;
}
