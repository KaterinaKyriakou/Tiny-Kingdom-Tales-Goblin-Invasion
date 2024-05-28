using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIchase : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distanceBetween;
    private Animator animation;
    private Vector3 initialPosition; // Variable to store the object's initial position
    private bool isReturning = false; // Flag to indicate if the object is returning to its initial position
    private float returnTimer = 0f; // Timer to track the time elapsed since the player moved out of the objects range

    private void Start()
    {
        animation = GetComponent<Animator>();
        initialPosition = transform.position; // Store the initial position when the object is instantiated
    }

    private void Update()
    {
        animation.SetBool("run", false);
        float distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        // Check if the player's position is to the left of the AI's position
        if (player.transform.position.x < transform.position.x){
            // Flip the AI's scale on the x-axis (left)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }else{
            // Reset the AI's scale on the x-axis (right)
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        // Check if the player is within the chase distance
        if (distance < distanceBetween){
            // Move towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            animation.SetBool("run", true);

            // Reset the return timer
            returnTimer = 0f;
            isReturning = false;
        }else{
            // Player is out of range, start return timer
            returnTimer += Time.deltaTime;
            animation.SetBool("run", false);

            // If 2 seconds have passed, move back to the initial position
            if (returnTimer >= 2f && !isReturning){
                transform.position = Vector2.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);
                animation.SetBool("run", true);
            }
            if(transform.position == initialPosition){
                animation.SetBool("run", false);
            }
        }// end if and else of checking if the NPC is chacing or not
    }//end update
}// end total
