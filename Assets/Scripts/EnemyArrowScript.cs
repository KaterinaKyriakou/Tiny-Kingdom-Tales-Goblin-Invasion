using UnityEngine;

public class EnemyArrowScript : MonoBehaviour
{
    private GameObject Player;
    private Rigidbody2D rb;
    public float force;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = Player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5f)
        {
            // Destroy the arrow
            Destroy(gameObject);
        }
    
    }

}
