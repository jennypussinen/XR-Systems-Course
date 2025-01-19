using UnityEngine;
using UnityEngine.InputSystem;

public class QuitScript : MonoBehaviour
{
    public InputActionReference action; // Reference to the input action

    void Start()
    {
        // Enable the input action
        action.action.Enable();

        // Subscribe to the performed event
        action.action.performed += (ctx) =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
#else
            Application.Quit(); // Quit the application in a standalone build
#endif
        };
    }

}
