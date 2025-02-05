using UnityEngine;

public class MagnifyingLens : MonoBehaviour
{
    public Transform xrCamera; // XR Main Camera (Headset)
    public Transform magnifyingGlass; // Magnifying Lens
    public float maxRadius = 0.5f; // Max movement distance from magnifying lens
    public float attractionSpeed = 5f; // Speed of attraction towards the lens

    void Update()
    {
        if (xrCamera == null || magnifyingGlass == null) return;

        // Compute direction from XR Camera to the magnifying lens
        Vector3 directionToLens = xrCamera.position - magnifyingGlass.position;

        // Calculate target position based on the camera's position
        Vector3 targetPosition = xrCamera.position;

        // Clamp the position to stay within the max radius around the magnifying lens
        Vector3 offset = targetPosition - magnifyingGlass.position;
        if (offset.magnitude > maxRadius)
        {
            targetPosition = magnifyingGlass.position + offset.normalized * maxRadius;
        }

        // Smoothly move the magnifying lens toward the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * attractionSpeed);

        // Ensure the lens always faces the magnifying glass
        transform.LookAt(magnifyingGlass.position);

        // Match the z-rotation of the magnifying lens to the magnifying glass
        Vector3 currentRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, magnifyingGlass.rotation.eulerAngles.z);
    }
}


