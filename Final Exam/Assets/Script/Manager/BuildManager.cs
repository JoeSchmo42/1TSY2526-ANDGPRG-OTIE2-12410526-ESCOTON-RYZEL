using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;

    Ray ray;
    RaycastHit hit;

    [SerializeField] LayerMask buildableLayer;
    [SerializeField] Transform towerParent;

    public TowerData selectedTowerData;
    GameObject previewTower;
    Renderer previewRenderer;
    Coroutine buildCoroutine;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (selectedTowerData == null) return;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildableLayer))
        {
            if (previewTower == null)
            {
                previewTower = Instantiate(selectedTowerData.prefab, hit.point, Quaternion.identity, towerParent);
                previewRenderer = previewTower.GetComponentInChildren<Renderer>();
                previewRenderer.material.color = Color.cyan;
                previewTower.GetComponent<Collider>().enabled = false;
                foreach (var comp in previewTower.GetComponents<MonoBehaviour>())
                    if (comp != this) comp.enabled = false;
            }

            previewTower.transform.position = new Vector3(
                Mathf.Round(hit.point.x), hit.point.y + 0.1f, Mathf.Round(hit.point.z));

            if (Input.GetMouseButtonDown(0))
            {
                if (GameManager.Instance.SpendGold(selectedTowerData.cost))
                {
                    StartBuild(previewTower, selectedTowerData);
                    selectedTowerData = null;
                    previewTower = null;
                }
            }
        }

        
        if (selectedTowerData == null && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                TowerUpgrade tower = hit.transform.GetComponent<TowerUpgrade>();
                if (tower != null)
                {
                    UI_TowerSelection.Instance.SelectTower(tower);
                    return;
                }
            }

            // Clicked nothing → clear selection
            UI_TowerSelection.Instance.ClearSelection();
        }

        if (Input.GetMouseButtonDown(1)) // Right click cancel
        {
            CancelBuild();
        }
    }

    void StartBuild(GameObject towerObj, TowerData data)
    {
        TowerBuildProgress buildComp = towerObj.AddComponent<TowerBuildProgress>();
        buildComp.StartBuilding(data);
    }

    public void SelectTower(TowerData data)
    {
        CancelBuild();
        selectedTowerData = data;
    }

    public void CancelBuild()
    {
        if (previewTower != null) Destroy(previewTower);
        selectedTowerData = null;
        previewTower = null;
    }
}