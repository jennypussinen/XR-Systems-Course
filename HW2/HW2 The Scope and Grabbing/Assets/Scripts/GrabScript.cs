using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{
    public CustomGrab otherHand = null;
    public List<Transform> nearObjects = new List<Transform>();
    public Transform grabbedObject = null;
    public InputActionReference action;

    private bool grabbing = false;
    private Vector3 grabOffset;
    private Quaternion grabRotationOffset;
    private Vector3 previousPosition;
    private Quaternion previousRotation;

    private void Start()
    {
        action.action.Enable();

        // Find the other hand
        foreach (CustomGrab c in transform.parent.GetComponentsInChildren<CustomGrab>())
        {
            if (c != this)
                otherHand = c;
        }

        // Initialize previous position and rotation
        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }

    void Update()
    {
        grabbing = action.action.IsPressed();

        if (grabbing)
        {
            // Grab an object if not already holding one
            if (!grabbedObject)
            {
                grabbedObject = nearObjects.Count > 0 ? nearObjects[0] : otherHand.grabbedObject;

                if (grabbedObject)
                {
                    // Calculate grab offset
                    grabOffset = grabbedObject.position - transform.position;
                    grabRotationOffset = Quaternion.Inverse(transform.rotation) * grabbedObject.rotation;
                }
            }

            if (grabbedObject)
            {
                // Calculate deltas
                Vector3 deltaPosition = transform.position - previousPosition;
                Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(previousRotation);

                if (otherHand.grabbing && otherHand.grabbedObject == grabbedObject)
                {
                    // Combine transformations from both hands
                    Vector3 otherDeltaPosition = otherHand.transform.position - otherHand.previousPosition;
                    Quaternion otherDeltaRotation = otherHand.transform.rotation * Quaternion.Inverse(otherHand.previousRotation);

                    // Combine translations
                    Vector3 combinedDeltaPosition = (deltaPosition + otherDeltaPosition) / 2;
                    grabbedObject.position += combinedDeltaPosition;

                    // Combine rotations
                    Quaternion combinedDeltaRotation = deltaRotation * otherDeltaRotation;
                    grabbedObject.rotation = combinedDeltaRotation * grabbedObject.rotation;
                }
                else
                {
                    // Single-hand manipulation
                    grabbedObject.position = transform.position + grabOffset;
                    grabbedObject.rotation = transform.rotation * grabRotationOffset;
                }
            }
        }
        else if (grabbedObject)
        {
            // Release object when grip is released
            grabbedObject = null;
        }

        // Save current position and rotation for the next frame
        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform t = other.transform;
        if (t && t.tag.ToLower() == "grabbable")
        {
            nearObjects.Add(t);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Transform t = other.transform;
        if (t && t.tag.ToLower() == "grabbable")
        {
            nearObjects.Remove(t);
        }
    }
}
