using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinTNT : MonoBehaviour
{
    public float speed = 3.0f;
    public float chaseRadius = 5.0f;
    public float throwRange = 3.0f;
    public float throwDelay = 2.0f;

    private float lastThrowTime = -Mathf.Infinity; // last throw
    private bool isThrowing = false;

    private AudioManager audioManager;
    public Transform player;
    private Animator animator;
    private Rigidbody2D rb;
    public GameObject TNTpre;
    public Transform TntPos;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        //Direction
            if (player.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        

        if (distanceToPlayer <= chaseRadius)
        {
            if (distanceToPlayer <= throwRange)
            {
                if (!isThrowing && Time.time >= lastThrowTime + throwDelay)
                {
                    StopAndThrow();
                }
            }
            else
            {
                if (isThrowing) return;
                ChasePlayer();
                animator.SetBool("run", true);
            }
        }
        else
        {
            animator.SetBool("run", false);
        }
    }

    private void StopAndThrow()
    {
        animator.SetBool("run", false);
        animator.SetTrigger("Throw");
        isThrowing = true;
        lastThrowTime = Time.time; // last throw
    }

    public void ThrowComplete()
    {
        isThrowing = false; //add event to the end of throwing animation
    }

    public void InstantiateTNT()//event added to animation
    {
        GameObject TNT = Instantiate(TNTpre, transform.position, TntPos.rotation);
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        TNT.GetComponent<Rigidbody2D>().velocity = direction * 4.0f;
        isThrowing = false;
    }
        private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}
