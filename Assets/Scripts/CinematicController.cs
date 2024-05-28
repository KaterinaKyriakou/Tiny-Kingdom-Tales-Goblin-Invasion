using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CinematicController : MonoBehaviour
{
    public int sceneBuildIndex;
    private VideoPlayer videoPlayer;

    void Start()
    {
        
        videoPlayer = GetComponent<VideoPlayer>();

        
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    public void SkipCinematic()
    {
        // Load scene when skip button pressed
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Single);
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Load the playable scene when the video ends
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Single);
    }
}
