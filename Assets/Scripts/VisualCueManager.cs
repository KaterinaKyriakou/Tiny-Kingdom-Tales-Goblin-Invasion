using UnityEngine;

public class VisualCueManager : MonoBehaviour
{
    [Header("Visual Cue Sprites")]
    [SerializeField] private Sprite exclamationSprite; // "!" sprite
    [SerializeField] private Sprite interactSprite; // "E" sprite

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ensure the visual cue is initially set correctly
        UpdateVisualCue(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            UpdateVisualCue(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            UpdateVisualCue(false);
        }
    }

    private void UpdateVisualCue(bool inRange)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = inRange ? interactSprite : exclamationSprite;
        }
    }
}
