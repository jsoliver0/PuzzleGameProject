using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite openSprite;  // Assign the open door sprite
    public Sprite closedSprite; // Assign the closed door sprite

    private SpriteRenderer spriteRenderer;
    private bool isOpen = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = closedSprite; // Set default to closed
    }

    public void SetState(bool open)
    {
        isOpen = open;

        // Change the sprite based on door state
        spriteRenderer.sprite = isOpen ? openSprite : closedSprite;

        // Disable collider when door is open, enable when closed
        GetComponent<Collider2D>().enabled = !isOpen;
    }

}
