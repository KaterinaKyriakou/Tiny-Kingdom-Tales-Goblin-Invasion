using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinTourchDmg : MonoBehaviour
{

    [SerializeField] private int damageToGive = 10;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
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
