using UnityEngine;

public class DungeonExit : MonoBehaviour
{
    public int dungeonNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Mark the dungeon as completed
            string dungeonCompletedKey = "Dungeon0" + dungeonNumber + "Completed"; //construting the PlayerPrefs Key
            PlayerPrefs.SetInt(dungeonCompletedKey, 1);
            PlayerPrefs.Save();

            // Display
            Debug.Log("Dungeon0" + dungeonNumber + " marked as completed.");
        }
    }
}

