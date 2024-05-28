using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    private HealthManager healthMan;
    [SerializeField] private int damageToGive = 10;


    void Start()
    {
        healthMan = FindObjectOfType<HealthManager>();
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            healthMan.HurtPlayer(damageToGive);
        }
    }
}
