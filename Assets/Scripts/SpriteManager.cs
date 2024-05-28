using UnityEngine;

public class DungeonEntranceSpriteManager : MonoBehaviour
{
    public int dungeonNumber;
    public Sprite completedSprite; 

    private string dungeonCompletedKey;

    private void Start()
    {
        // Construct the key for checking dungeon completion status
        string dungeonCompletedKey = "Dungeon0" + dungeonNumber + "Completed";
    
        // Check if the dungeon is completed and update the sprite 
        if (PlayerPrefs.GetInt(dungeonCompletedKey, 0) == 1)
        {
            UpdateSprite();
        }
    }

    private void UpdateSprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && completedSprite != null)
        {
            spriteRenderer.sprite = completedSprite;
            
        }
    }
}

