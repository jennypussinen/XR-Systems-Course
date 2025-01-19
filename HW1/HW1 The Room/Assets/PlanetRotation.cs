using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    public float rotationSpeed = 10f; // Speed of rotation

    void Update()
    {
        // Rotate the planet around its Y-axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}

