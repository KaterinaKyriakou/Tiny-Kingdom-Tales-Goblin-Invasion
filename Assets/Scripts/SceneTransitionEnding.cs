using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionEnding : MonoBehaviour
{
    public int sceneBuildIndex;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;

    private void Awake()
    {
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerStorage.initialValue = playerPosition;
            StartCoroutine(FadeCo());
        }
    }

    private bool CheckAllDungeonsCompleted()
    {
        // Check if all dungeons are completed
        return PlayerPrefs.GetInt("Dungeon01Completed", 0) == 1 &&
               PlayerPrefs.GetInt("Dungeon02Completed", 0) == 1 &&
               PlayerPrefs.GetInt("Dungeon03Completed", 0) == 1 &&
               PlayerPrefs.GetInt("Dungeon04Completed", 0) == 1;
    }

    public IEnumerator FadeCo()
    {
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
        }
        yield return new WaitForSeconds(fadeWait);

        // Check if all dungeons are completed
        if (CheckAllDungeonsCompleted())
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Single);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
        else
        {
            Debug.Log("All dungeons must be completed before proceeding.");
        }
    }
}
