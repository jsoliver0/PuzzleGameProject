using UnityEngine;
using System.Collections;

public class TimedPressurePlate : MonoBehaviour
{
    [Header("Door to Control")]
    public GameObject controlledDoor;

    [Header("Timer Settings")]
    public float activeDuration = 5f; // Duration in seconds
    private bool isActive = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Crate"))
        {
            if (!isActive)
            {
                StartCoroutine(ActivateForTime());
            }
        }
    }

    private IEnumerator ActivateForTime()
    {
        isActive = true;
        UpdateDoorState(true);

        // Optional: Change pressure plate appearance
        GetComponent<SpriteRenderer>().color = Color.green;

        yield return new WaitForSeconds(activeDuration);

        isActive = false;
        UpdateDoorState(false);

        // Revert appearance
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void UpdateDoorState(bool state)
    {
        if (controlledDoor != null)
        {
            controlledDoor.GetComponent<Door>().SetState(state);
        }
    }
}