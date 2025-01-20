using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPositionSwitcher : MonoBehaviour
{
    // Reference to the player's XR Origin (or the GameObject holding the camera)
    public Transform playerTransform;

    // External viewing point position (assign in Inspector)
    public Transform externalViewPoint;

    // Room position (the starting point, set this to (0,0,0) or wherever you want)
    public Transform roomTransform;

    // To track the state of the position (inside room or external view)
    private bool isExternalView = false;

    // The InputActionReference for your action (e.g., button press)
    public InputActionReference action;

    private void OnEnable()
    {
        // Enable the input action when the script is enabled
        if (action != null && action.action != null)
        {
            action.action.Enable();
        }
    }

    private void OnDisable()
    {
        // Disable the input action when the script is disabled
        if (action != null && action.action != null)
        {
            action.action.Disable();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Ensure room position is assigned (can be set in the Inspector)
        if (roomTransform == null)
        {
            Debug.LogError("Room transform is not assigned!");
        }

        if (externalViewPoint == null)
        {
            Debug.LogError("External Viewpoint is not assigned!");
        }

        // Set the initial position to be the room position (0, 0, 0)
        if (playerTransform != null)
        {
            playerTransform.position = roomTransform.position;  // Set room position at start
        }

        Debug.Log("Room Position: " + roomTransform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the action is triggered (e.g., button press)
        if (action != null && action.action != null && action.action.triggered)
        {
            TogglePlayerPosition();
            Debug.Log("Button pressed: Toggling position");
        }
    }

    // Method to toggle the player's position
    private void TogglePlayerPosition()
    {
        // Logging current position
        Debug.Log("Current position: " + playerTransform.position);

        // Toggle the player's position
        if (isExternalView)
        {
            // If currently at external view, move back to the room
            playerTransform.position = roomTransform.position;
            Debug.Log("Moved to Room Position");
        }
        else
        {
            // If inside the room, move to the external viewing point
            playerTransform.position = externalViewPoint.position;
            Debug.Log("Moved to External View");
        }

        // Alternate the state
        isExternalView = !isExternalView;
    }
}
