using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen = false;

    public void SetState(bool open)
    {
        isOpen = open;

        // Optional: Add animations or effects
        gameObject.SetActive(!isOpen); // Disable the door object if open
    }
}