using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionDun : MonoBehaviour
{
    public int sceneBuildIndex;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;
    public int dungeonNumber; // Dungeon number for constructing the PlayerPrefs key
    public bool checkIsCleared = true; // enabling/disabling the isCleared logic

    public GameObject visualCue; // Reference to the visual cue GameObject

    private bool canChangeScene = false;
    private string dungeonCompletedKey; // The constructed PlayerPrefs key to check if the dungeon is cleared

    private void Awake()
    {
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
                
        if (visualCue != null)
        {
            visualCue.SetActive(false);
        }
        // Construct the dungeon completed key
        dungeonCompletedKey = "Dungeon0" + dungeonNumber + "Completed";
    }

    private void Update()
    {
        if (canChangeScene && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(FadeCo());
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerStorage.initialValue = playerPosition;
            canChangeScene = true;
            
            // Enable visual cue if the dungeon is not cleared
            if (!IsDungeonCleared() && visualCue != null)
            {
                visualCue.SetActive(true);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            canChangeScene = false;
            
            // Disable visual cue when the player exits the collider
            if (visualCue != null)
            {
                visualCue.SetActive(false);
            }
        }
    }

    private bool IsDungeonCleared()
    {
        if (!checkIsCleared)
        {
            return false; // Always return false if isCleared logic is disabled
        }
        // Check if the dungeon is cleared 
        return PlayerPrefs.GetInt(dungeonCompletedKey, 0) == 1;
    }

    private IEnumerator FadeCo()
    {
        if (!IsDungeonCleared()) // Check if the dungeon is NOT cleared before changing the scene
        {
            if (fadeInPanel != null)
            {
                GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            }
            yield return new WaitForSeconds(fadeWait);
        
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Single);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
        else
        {
            Debug.Log("Dungeon is cleared. Cannot change scene.");
        }
    }
}

