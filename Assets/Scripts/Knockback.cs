using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            Vector2 difference = transform.position - other.transform.position;
            difference = difference.normalized * thrust;//thrust must be a big number because of the mass
            rb.AddForce(difference, ForceMode2D.Impulse); 

            StartCoroutine(KnockbackCoroutine(knockTime));
        }
    }

    private IEnumerator KnockbackCoroutine(float knockTime)
    {
        if (rb != null)
        {
            yield return new WaitForSeconds(knockTime);
            rb.velocity = Vector2.zero; //reset movement
        }
    }
}
