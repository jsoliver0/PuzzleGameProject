using System.Collections;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public enum PlateType { Time, Weighted, Toggle }
    public PlateType plateType;
    public float activationTime = 3f; // Time for TimePressurePlate
    public Animator animator;
    public GameObject controlledDoor;

    private SpriteRenderer spriteRenderer;
    private bool isActive = false;
    private int objectsOnPlate = 0;

    // Track the running coroutine
    private Coroutine deactivateCoroutine;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is missing on " + gameObject.name);
            return; // Stop execution to avoid further errors
        }

        if (controlledDoor == null)
        {
            Debug.LogError("Controlled door is not assigned in " + gameObject.name);
        }
    }

    private void Update()
    {
        // Update the animator based on the state
        if (animator != null)
        {
            animator.SetBool("Active", isActive);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something stepped on me: " + other.gameObject.name);

        if (other.CompareTag("Player") || other.CompareTag("Crate"))
        {
            objectsOnPlate++;
            ActivatePlate();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Crate"))
        {
            Debug.Log("Pressure Plate Exited by: " + other.gameObject.name);
            objectsOnPlate--;
            // Only deactivate the plate if there are no objects on it for weighted plates
            if (plateType != PlateType.Time)
            {
                DeactivatePlate();
            }
        }
    }

    void ActivatePlate()
    {
        if (plateType == PlateType.Toggle)
        {
            isActive = !isActive;
        }
        else if (plateType == PlateType.Weighted)
        {
            isActive = objectsOnPlate > 0;
        }
        else if (plateType == PlateType.Time)
        {
            if (!isActive) // Only start the timer if it's not already active
            {
                isActive = true;
                deactivateCoroutine = StartCoroutine(DeactivateAfterTime());
            }
        }

        UpdateDoorState(isActive);
    }

    void DeactivatePlate()
    {
        Debug.Log("Plate Type: " + plateType);
        // Toggle plates should not auto-reset
        if (plateType == PlateType.Toggle) return;

        // Weighted plates should only reset if no objects are on them
        if (plateType == PlateType.Weighted && objectsOnPlate > 0) return;

        isActive = false;

        // Debug to check if this function is running
        Debug.Log("Pressure Plate Deactivated");

        UpdateDoorState(false);
    }

    IEnumerator DeactivateAfterTime()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(activationTime);

        // Once the time is up, deactivate the plate
        isActive = false;

        // If the plate has no objects, deactivate the door
        UpdateDoorState(false);

        Debug.Log("Pressure Plate Deactivated after " + activationTime + " seconds");
    }

    private void UpdateDoorState(bool state)
    {
        if (controlledDoor != null)
        {
            controlledDoor.GetComponent<Door>().SetState(state);
        }
    }
}