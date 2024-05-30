using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinTourchDmg : MonoBehaviour
{

    [SerializeField] private int damageToGive = 10;

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthManager HealthMan;
            HealthMan = other.gameObject.GetComponent<HealthManager>();
            HealthMan.HurtPlayer(damageToGive);
        }
    }
}
