using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] LayerMask groundMask = -1;
    [SerializeField] float buildableOffsetY = 2f;
    [SerializeField] GameObject[] prefabTowers; // Assign in inspector: [0] Arrow prefab, [1] Ice prefab, [2] Fire prefab, [3] Cannon prefab

    private GameObject dragTower;
    private Tower tempTowerObj;

    Ray ray;
    RaycastHit hit;

    private void CreatePreviewTower(int index)
    {
        // Destroy previous preview if exists
        if (dragTower != null)
        {
            Destroy(dragTower);
            dragTower = null;
            tempTowerObj = null;
        }

        if (index < 0 || index >= prefabTowers.Length || prefabTowers[index] == null)
        {
            Debug.LogWarning($"Invalid tower index {index} or prefab missing.");
            return;
        }

        GameObject tempTower = Instantiate(prefabTowers[index]);
        dragTower = tempTower;
        tempTowerObj = tempTower.GetComponent<Tower>();

        if (tempTowerObj == null)
        {
            Debug.LogError($"Prefab {prefabTowers[index].name} missing Tower component!");
            Destroy(tempTower);
            dragTower = null;
            return;
        }
    }

    public void SpawnArrow() => CreatePreviewTower(0);
    public void SpawnIce() => CreatePreviewTower(1);
    public void SpawnFire() => CreatePreviewTower(2);
    public void SpawnCannon() => CreatePreviewTower(3);

    Vector3 SnapToGrid(Vector3 snapPos)
    {
        return new Vector3(Mathf.Round(snapPos.x),
                           snapPos.y,
                           Mathf.Round(snapPos.z));
    }

    void Update()
    {
        if (dragTower == null)
            return;

        // Cancel preview with right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(dragTower);
            dragTower = null;
            tempTowerObj = null;
            return;
        }

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            Debug.DrawLine(Camera.main.transform.position, hit.point, Color.blue);
            dragTower.transform.position = SnapToGrid(hit.point);

            if (hit.point.y > buildableOffsetY)
            {
                tempTowerObj.Buildable();

                if (Input.GetMouseButtonDown(0))
                {
                    tempTowerObj.Build();
                    dragTower = null;
                    tempTowerObj = null;
                    return;
                }
            }
            else
            {
                tempTowerObj.NonBuildable();
            }
        }
    }
}
