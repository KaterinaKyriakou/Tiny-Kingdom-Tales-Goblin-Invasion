using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    private AudioManager audioManager;
    public int currentHealth;
    public int maxHealth;

    private bool flashActive;
    [SerializeField]
    private float flashLenght = 0f;
    private float flashCounter = 0f;
    private SpriteRenderer enemySprite;

    void Start()
    {
        enemySprite = GetComponent<SpriteRenderer>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Update()
    {
        if (flashActive)
        {
            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
                flashActive = false;
            }
            else
            {
                float alpha = Mathf.PingPong(Time.time * 5, 1); // Flashing effect
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, alpha);
            }
        }
    }

    public void HurtEnemy(int damageToGive)
    {
        flashActive = true;
        flashCounter = flashLenght;
        currentHealth -= damageToGive;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            audioManager.PlayGoblinSFX(audioManager.GoblinDeath);
        }
    }
}
