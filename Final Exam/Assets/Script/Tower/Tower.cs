using UnityEngine;

public class Tower : MonoBehaviour
{

    private Renderer rend;

    private void Awake() => rend = GetComponentInChildren<Renderer>();

    public void SetBuildable() => rend.material.color = Color.green;
    public void SetNonBuildable() => rend.material.color = Color.red;
    public void SetBuilt() => rend.material.color = Color.white;
}
