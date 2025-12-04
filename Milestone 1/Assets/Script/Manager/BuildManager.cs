using UnityEngine;

public class BuildingManager : MonoBehaviour
{

    Ray ray;
    RaycastHit hit;

    float buildableOffsetY = 2;

    [SerializeField] GameObject[] prefabTowers; // all the possible tower avaible in your selection

    [SerializeField] GameObject dragTower;      // the that is create and looking for where to be build

    [SerializeField] Tower tempTowerObj;

    
    public void SpawnTower()
    {
        GameObject tempTower = (GameObject)Instantiate(prefabTowers[0]);    // change the index for diff towers
        dragTower = tempTower;
        tempTowerObj = tempTower.GetComponent<Tower>();

    }

	Vector3 SnapToGrid(Vector3 snapPos)
    {
        return new Vector3(Mathf.Round(snapPos.x),
							snapPos.y,
                            Mathf.Round(snapPos.z));
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(dragTower == null)
            return; 

        if(Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(Camera.main.transform.position, hit.point);
            dragTower.transform.position = SnapToGrid(hit.point);

            if (hit.point.y > buildableOffsetY)
            {
                tempTowerObj.Buildable();

                if (Input.GetMouseButtonDown(0))
                {
                    tempTowerObj.Build();
                    dragTower = null;
                    tempTowerObj=null;
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
