using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Animator animation;
    private Rigidbody2D body;
    private int facingDirection = 1; // 1 for right, -1 for left

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0f;
        animation = GetComponent<Animator>();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;

        /*Switches character when facing left right (x axis) */
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

        //Set the "Run" parameter in the Animator
        animation.SetBool("Run", moveHorizontal != 0 || moveVertical != 0);

        // Apply speed to movement vector
        movement *= speed;
        body.velocity = movement;

        // Debug.Log("Player Position: " + transform.position);
    }

    public int GetFacingDirection()
    {
        return facingDirection;
    }
}
