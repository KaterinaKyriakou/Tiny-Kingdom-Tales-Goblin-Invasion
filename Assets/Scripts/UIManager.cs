using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image healthBarImage; // assign main image
    public Sprite[] healthBarSprites; // assign all the sprites

    private HealthManager healthManager; // reference to healthmanager

    void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        if (healthManager != null)
        {
            // Calculate the index based on the current health percentage
            float healthPercentage = (float)healthManager.currentHealth / healthManager.maxHealth;
            int spriteIndex = Mathf.CeilToInt(healthPercentage * healthBarSprites.Length) - 1;
            spriteIndex = Mathf.Clamp(spriteIndex, 0, healthBarSprites.Length - 1);


            // Update the health bar sprite
            healthBarImage.sprite = healthBarSprites[spriteIndex];
        }
    }
}
