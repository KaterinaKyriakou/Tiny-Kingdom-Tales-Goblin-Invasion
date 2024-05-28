using UnityEngine;

public class PlayerPrefsResetter : MonoBehaviour
{
    private void Awake()
    {
        // Reset dungeon completion status 
        ResetDungeonCompletionStatus("Dungeon01Completed");
        ResetDungeonCompletionStatus("Dungeon02Completed");
        ResetDungeonCompletionStatus("Dungeon03Completed");
        ResetDungeonCompletionStatus("Dungeon04Completed");
    }

    private void ResetDungeonCompletionStatus(string key)
    {
        // Check if the key exists in PlayerPrefs
        if (PlayerPrefs.HasKey(key))
        {
            // Reset the completion status to false (0)
            PlayerPrefs.SetInt(key, 0);
            PlayerPrefs.Save();
        }
    }
}
