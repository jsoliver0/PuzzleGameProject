using UnityEngine;

public class WeightedPressurePlate : MonoBehaviour
{
    [Header("Door to Control")]
    public GameObject controlledDoor;
    private int objectsOnPlate = 0; // Count of objects currently on the plate

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Crate"))
        {
            objectsOnPlate++;
            UpdateDoorState();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Crate"))
        {
            objectsOnPlate--;
            UpdateDoorState();
        }
    }

    private void UpdateDoorState()
    {
        bool isActive = objectsOnPlate > 0;

        if (controlledDoor != null)
        {
            controlledDoor.GetComponent<Door>().SetState(isActive);
        }

        // Optional: Change pressure plate appearance
        GetComponent<SpriteRenderer>().color = isActive ? Color.green : Color.red;
    }
}