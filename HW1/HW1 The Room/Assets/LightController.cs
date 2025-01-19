using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class LightController : MonoBehaviour
{
    public Light pointLight;  // Reference to the Light component
    public InputActionReference action;  // Reference to the input action

    private void OnEnable()
    {
        // Enable the input action when the object is enabled
        action.action.Enable();
    }

    private void OnDisable()
    {
        // Disable the input action when the object is disabled
        action.action.Disable();
    }

    void Start()
    {
        // If pointLight is not assigned in the inspector, find it automatically
        if (pointLight == null)
        {
            pointLight = GetComponent<Light>();
        }
    }

    void Update()
    {
        // Check if the input action is triggered
        if (action.action.triggered)
        {
            // Change the light color when the action is triggered
            ChangeLightColor();
        }
    }

    void ChangeLightColor()
    {
        // Example: Change the light color to a random value
        pointLight.color = new Color(Random.value, Random.value, Random.value);
    }
}
