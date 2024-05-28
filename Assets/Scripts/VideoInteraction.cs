using UnityEngine;
using UnityEngine.Video;

public class VideoInteraction : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string playerTag = "Player"; // Tag to identify the player
    private bool playerInRange = false; // Flag to indicate if player is in range
    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
        videoPlayer.gameObject.SetActive(false); // Initially disable the video player
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Player entered trigger zone.");
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Player exited trigger zone.");
            playerInRange = false;
        }
    }

    private void Update()
    {
        // Check if player is in range and pressed the 'E' key
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        // Play video
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        videoPlayer.gameObject.SetActive(false);
        
    }
}
