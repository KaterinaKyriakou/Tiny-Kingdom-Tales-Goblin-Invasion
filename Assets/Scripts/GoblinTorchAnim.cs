using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinTorchAnim : MonoBehaviour
{
    //private AudioManager audioManager;
    private HealthManager healthMan;
    private Animator animator;
    private Vector3 previousPosition;

    private float currentWaitTime;
    private bool attackInitiated = false;
    public bool isTouching;


    [SerializeField] public float waitToHurt = 0.2f;


    void Start()
    {
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
        previousPosition = transform.position;
        currentWaitTime = waitToHurt;
    }

    void Update()
    {

        Vector3 movementDirection = (transform.position - previousPosition).normalized; //face player when attacking
        if (movementDirection != Vector3.zero)
        {
            animator.SetFloat("lastmoveX", movementDirection.x);
            animator.SetFloat("lastmoveY", movementDirection.y);
        }
        previousPosition = transform.position;

        if (isTouching)
        {
            if (currentWaitTime > 0)
            {
                currentWaitTime -= Time.deltaTime;
            }
            else if (!attackInitiated)
            {
                animator.SetBool("Attacking", true);
                //audioManager.PlayGoblinSFX(audioManager.GoblinAttack);
                attackInitiated = true;
                Invoke("ResetAttack", 0.5f); //calls reset attack after animation so time should match the animation
            }
        }
    }

    private void ResetAttack()
    {
        animator.SetBool("Attacking", false);
        attackInitiated = false;
        currentWaitTime = waitToHurt;  // Reset the timer
    }


   private void OnCollisionEnter2D(Collision2D other) //as soon as collision happens
    {
        if (other.collider.tag == "Player")
        {
            isTouching = true;
            currentWaitTime = 0;
        }
    }

   private void OnCollisionExit2D(Collision2D other)//collision exit
    {
        isTouching = false;
        ResetAttack();
    }
}
