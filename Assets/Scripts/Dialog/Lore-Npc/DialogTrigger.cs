using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Input")]
    [SerializeField] private string triggerInput;

    [Header("Panel")]
    [SerializeField] private GameObject Panel;

    private bool playerInRange;
    private bool DialogPlaying = false;
    
    private void Update()
    {
        if(playerInRange && (DialogPlaying == true) && Input.GetKeyDown(KeyCode.E) ){
            DialogPlaying = false;
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    
        }
    }

    private void Awake()
    {
        playerInRange = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
            DialogPlaying = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
         if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
            Panel.SetActive(false);
        }
    }
}