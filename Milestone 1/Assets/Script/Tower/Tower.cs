using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Material towerMaterial;


    public void Buildable()
    {
		towerMaterial.color = Color.green;

	}

    public void NonBuildable()
    {
		towerMaterial.color = Color.red;
	}

    public void Build()
    {
		towerMaterial.color = Color.white;
	}
}
