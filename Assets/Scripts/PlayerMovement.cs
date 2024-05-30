using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Animator animator;
    private Rigidbody2D body;
    public DialogueManager dialogueManager;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dialogueManager = FindObjectOfType<DialogueManager>();

    }

    private void Update()
    {
        if (dialogueManager != null && dialogueManager.IsDialoguePlaying)
        {
            body.velocity = Vector2.zero;
            animator.SetBool("Run", false);
            return;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;

        if (moveHorizontal > 0.01f)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, 1);
        }
        else if (moveHorizontal < -0.01f)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, 1);
        }

        animator.SetBool("Run", moveHorizontal != 0 || moveVertical != 0);
        movement *= speed;
        body.velocity = movement;
    }
}
