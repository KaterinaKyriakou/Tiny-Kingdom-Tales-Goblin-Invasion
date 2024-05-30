using UnityEngine;

public class VisualCueManager : MonoBehaviour
{
    [Header("Visual Cue Sprites")]
    [SerializeField] private Sprite exclamationSprite; // "!" 
    [SerializeField] private Sprite interactSprite; // "E" 

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
