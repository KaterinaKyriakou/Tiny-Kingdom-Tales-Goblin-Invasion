using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject Arrow;
    public Transform ArrowPos;
    private float timer;
    private GameObject Player;
    // private bool canShoot = true; // Flag to control shooting

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        // Deactivate the reference arrow
        Arrow.SetActive(false);
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, Player.transform.position);
        Debug.Log(distance);

        if (distance < 5 ) // Check if shooting is allowed
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {
                timer = 0;
                shoot();
            }
        }
    }

    void shoot()
    {
        // Activate the reference arrow before instantiating a new one
        Arrow.SetActive(true);
        Instantiate(Arrow, ArrowPos.position, Quaternion.identity);
        // Deactivate the reference arrow after shooting
        Arrow.SetActive(false);
    }
}
