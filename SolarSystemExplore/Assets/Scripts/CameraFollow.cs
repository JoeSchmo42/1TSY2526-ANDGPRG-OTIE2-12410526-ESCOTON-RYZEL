using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Player cube
    public Vector3 offset = new Vector3(0, 5, -10); // Camera offset from player
    public float smoothSpeed = 0.125f; // Smoothing factor

    void LateUpdate()
    {
        if (target == null) return;

        // Desired position
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);
        // Smoothly interpolate to desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Look at the player
        transform.LookAt(target);
    }
}