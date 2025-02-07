using UnityEngine;

public class TogglePressurePlate : MonoBehaviour
{
    [Header("Door to Control")]
    public GameObject controlledDoor; // Assign door to open/close
    private bool isActive = false; // Current state

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the Player or a Crate is stepping on the plate
        if (collision.CompareTag("Player") || collision.CompareTag("Crate"))
        {
            isActive = !isActive; // Toggle the state
            UpdateDoorState();
        }
    }

    private void UpdateDoorState()
    {
        if (controlledDoor != null)
        {
            controlledDoor.GetComponent<Door>().SetState(isActive);
        }

        // Optional: Change pressure plate appearance
        GetComponent<SpriteRenderer>().color = isActive ? Color.green : Color.red;
    }
}
