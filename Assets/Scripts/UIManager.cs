using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image healthBarImage; 
    public Sprite[] healthBarSprites;

    private HealthManager healthManager;

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
