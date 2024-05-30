using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private float speed;
    private Animator animator;
    private Rigidbody2D body;
    private int facingDirection = 1; // 1 for right, -1 for left
    private float attackTime = 1.0f;
    private float attackCounter = 1.0f;
    private bool isAttacking;
    private AudioManager audioManager;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0f;
        animator = GetComponent<Animator>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (PauseMenu.isPaused)
        {
            return;
        }

        if (isAttacking)
        {
            //Stops movement and starts a counter to match the attack time
            body.velocity = Vector2.zero; // Stops movement
            attackCounter -= Time.deltaTime;
                if (attackCounter <= 0)
                {
                    animator.SetBool("isAttacking", false);
                    isAttacking = false;
                }
        }
        else
        {
            HandleMovement();
            HandleAttack();
        }
    }

    private void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;
        
        // Adjusts character facing based on movement direction
        AdjustFacingDirection(moveHorizontal);

        animator.SetBool("Run", moveHorizontal != 0 || moveVertical != 0);
        movement *= speed;
        body.velocity = movement;
    }

    private void AdjustFacingDirection(float moveHorizontal)
    {
        if (moveHorizontal > 0.01f)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, 1);
            facingDirection = 1;
        }
        else if (moveHorizontal < -0.01f)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, 1);
            facingDirection = -1;
        }
    }

    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;
            Vector2 direction = (mousePosition - transform.position).normalized;
            direction.x *= facingDirection;

            SetAnimatorDirection(direction);
            audioManager.PlayPlayerSFX(audioManager.PlayerAttack);
            attackCounter = attackTime;
            animator.SetBool("isAttacking", true);
            isAttacking = true;
        }
    }

    private void SetAnimatorDirection(Vector2 direction) //set parameters
    {
        animator.SetFloat("lastMoveX", direction.x);
        animator.SetFloat("lastMoveY", direction.y);
    }
}
