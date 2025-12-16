using UnityEngine;

public class LockedCameraPanner : MonoBehaviour
{
    public float panSpeed = 30f;           // How fast it moves
    public float edgeSize = 20f;          // How close to screen edge to start panning (pixels)

    public float panLimitX = 15f;   // Can move ±15 units left/right from center
    public float panLimitZ = 15f;   // Can move ±15 units forward/back from center

    // The exact center position you want
    private Vector3 centerPosition = new Vector3(56.7f, 11f, 36.7f);

    void Start()
    {
        // Force camera to the exact position at the start (in case you moved it by accident)
        transform.position = centerPosition;
    }

    void Update()
    {
        Vector3 move = Vector3.zero;

        // Arrow keys
        if (Input.GetKey(KeyCode.LeftArrow)) move.x -= 1f;
        if (Input.GetKey(KeyCode.RightArrow)) move.x += 1f;
        if (Input.GetKey(KeyCode.UpArrow)) move.z += 1f;
        if (Input.GetKey(KeyCode.DownArrow)) move.z -= 1f;

        // Mouse at screen edges
        if (Input.mousePosition.x < edgeSize) move.x -= 1f;
        if (Input.mousePosition.x > Screen.width - edgeSize) move.x += 1f;
        if (Input.mousePosition.y < edgeSize) move.z -= 1f;
        if (Input.mousePosition.y > Screen.height - edgeSize) move.z += 1f;

        
        Vector3 desiredPos = transform.position + move * panSpeed * Time.deltaTime;

        
        desiredPos.x = Mathf.Clamp(desiredPos.x, centerPosition.x - panLimitX, centerPosition.x + panLimitX);
        desiredPos.y = centerPosition.y;
        desiredPos.z = Mathf.Clamp(desiredPos.z, centerPosition.z - panLimitZ, centerPosition.z + panLimitZ);

        transform.position = desiredPos;
    }
}