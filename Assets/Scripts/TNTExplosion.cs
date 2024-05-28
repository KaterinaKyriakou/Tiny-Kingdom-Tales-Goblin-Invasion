using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTExplosion : MonoBehaviour
{
    private HealthManager healthMan;
    private AudioManager audioManager;
    [SerializeField] private int damageToGive = 10;
    private bool hasCollided = false;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        healthMan = FindObjectOfType<HealthManager>();
        StartCoroutine(DelayedExplosion());
    }

    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player") && !hasCollided)
        {
            hasCollided = true; //same with tnt barrell
            healthMan.HurtPlayer(damageToGive);
            audioManager.PlayGoblinSFX(audioManager.GoblinExplosion0);
        }
    }

    private IEnumerator DelayedExplosion()
    {
        yield return new WaitForSeconds(0.85f);
        if (!hasCollided)
        {
            audioManager.PlayGoblinSFX(audioManager.GoblinExplosion0);
        }
        Destroy(gameObject);
    }
}
