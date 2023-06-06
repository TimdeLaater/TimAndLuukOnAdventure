using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the target object (the cheese)
    public float distance; // Desired distance between the camera and the target
    public float smoothSpeed = 0.125f; // Smoothing factor for camera movement

    private Vector3 offset; // Offset position of the camera relative to the target

    private void Start()
    {
        // Calculate the initial offset based on the starting distance
        offset = transform.position - target.position;
        offset = offset.normalized * distance;
    }

    private void LateUpdate()
    {
        // Calculate the desired position for the camera
        Vector3 desiredPosition = target.position + offset;

        // Use smoothing to gradually move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the camera's position to the smoothed position
        transform.position = smoothedPosition;

        // Make the camera look at the target (the cheese)
        transform.LookAt(target);
    }
}
