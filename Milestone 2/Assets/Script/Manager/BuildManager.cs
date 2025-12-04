using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;

    [Header("Settings")]
    public LayerMask groundLayer;
    public Transform towerParent;

    [Header("Tower Prefabs (order matters)")]
    public GameObject[] towerPrefabs; // 0=Arrow, 1=Ice, 2=Fire, 3=Cannon

    private GameObject currentDragTower;
    private Tower currentTowerScript;

    private void Awake() => Instance = this;

    public void StartPlacingTower(int index)
    {
        int[] costs = { 50, 80, 100, 150 };
        if (GameManager.Instance.gold < costs[index]) return;

        currentDragTower = Instantiate(towerPrefabs[index]);
        currentTowerScript = currentDragTower.GetComponent<Tower>();
        currentTowerScript.SetNonBuildable();
    }

    void Update()
    {
        if (currentDragTower == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 500f, groundLayer))
        {
            Vector3 pos = hit.point;
            pos.y = 0.5f;
            currentDragTower.transform.position = pos;

            bool canBuild = hit.collider.CompareTag("Buildable");

            if (canBuild)
            {
                currentTowerScript.SetBuildable();
                if (Input.GetMouseButtonDown(0))
                {
                    PlaceTower();
                }
            }
            else
            {
                currentTowerScript.SetNonBuildable();
            }

            if (Input.GetMouseButtonDown(1))
                Cancel();
        }
    }

    void PlaceTower()
    {
        currentDragTower.transform.SetParent(towerParent);
        currentTowerScript.SetBuilt();
        currentDragTower.AddComponent<TowerAttack>();   // makes it shoot bang bang
        currentDragTower = null;
        currentTowerScript = null;
    }

    void Cancel()
    {
        Destroy(currentDragTower);
        currentDragTower = null;
        currentTowerScript = null;
    }
}