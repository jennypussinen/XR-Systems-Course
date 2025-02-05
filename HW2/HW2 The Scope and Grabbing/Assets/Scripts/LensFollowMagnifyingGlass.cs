using UnityEngine;

public class MagnifyingLens : MonoBehaviour
{
    public Transform xrCamera; // XR Main Camera (Headset)
    public Transform magnifyingGlass; // Magnifying Lens
    public float maxRadius = 0.5f; // Max movement distance from XR Camera
    public float attractionSpeed = 5f; // Speed of attraction towards the lens

    void Update()
    {
        if (xrCamera == null || magnifyingGlass == null) return;

        // Compute direction from XR Camera to the magnifying lens
        Vector3 directionToLens = magnifyingGlass.position - xrCamera.position;
        Vector3 targetPosition = magnifyingGlass.position; // Camera should be closest to the lens

        // Clamp position within a sphere around the XR Camera
        Vector3 offset = targetPosition - xrCamera.position;
        if (offset.magnitude > maxRadius)
        {
            targetPosition = xrCamera.position + offset.normalized * maxRadius;
        }

        // Smoothly move the secondary camera towards the target within the sphere
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * attractionSpeed);

        // Make sure the magnifying camera always looks at the magnifying glass
        transform.LookAt(magnifyingGlass.position);
    }
}
