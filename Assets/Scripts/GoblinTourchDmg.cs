using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinTourchDmg : MonoBehaviour
{

    [SerializeField] private int damageToGive = 10;
    //private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //audioManager.PlayGoblinSFX(audioManager.GoblinAttack);
            HealthManager HealthMan;
            HealthMan = other.gameObject.GetComponent<HealthManager>();
            HealthMan.HurtPlayer(damageToGive);
        }
    }
}
