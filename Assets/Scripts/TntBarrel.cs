using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TntBarrel : MonoBehaviour
{
    private AudioManager audioManager;
    private Animator animator;
    private bool isOut = false;
    private bool isExploding = false;

    public Transform player;
    public float chaseRadius = 5.0f;
    public float speed;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
        InvokeRepeating("ToggleAnimation", 1.60f, 1.60f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isExploding) return;

           // Check if the player's position is to the left of the AI's position
          if (player.transform.position.x < transform.position.x)
          {
              // Flip the AI's scale on the x-axis (left)
              transform.localScale = new Vector3(-1f, 1f, 1f);
          }
          else
          {
              // Reset the AI's scale on the x-axis (right)
              transform.localScale = new Vector3(1f, 1f, 1f);
          }

          float distanceToPlayer = Vector2.Distance(transform.position, player.position);

          if (distanceToPlayer <= chaseRadius)
          {
                  animator.SetBool("Running",true);
                  ChasePlayer();
          }
          else
          {
                animator.ResetTrigger("Running");
                animator.SetBool("isOut", isOut);  // Return to the correct idle state
          }

    }
    private void ToggleAnimation()
    {
        isOut = !isOut;
        animator.SetBool("isOut", isOut);
    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player" && !isExploding)
        {
            isExploding = true;
            animator.SetTrigger("Fired");
            audioManager.PlayGoblinSFX(audioManager.GoblinExplosion1);
            StartCoroutine(DestroyAfterAnimation());
        }
    }
    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(0.91f + 0.31f);//time of fired + explosion animation
        Destroy(gameObject);
    }
}
