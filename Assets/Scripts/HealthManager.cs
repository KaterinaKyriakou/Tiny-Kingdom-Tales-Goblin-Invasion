using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public int sceneBuildIndex;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    

    public GameObject playerDeathEffectPrefab; // Reference to the PlayerDeathEffect prefab

    private bool flashActive;
    [SerializeField] private float flashLength = 0.2f; // Adjust the flash length as needed
    private float flashCounter = 0f;
    private SpriteRenderer playerSprite;
    private AudioManager audioManager;

    private bool isDead = false;

    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (flashActive)
        {
            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                flashActive = false;
            }
            else
            {
                float alpha = Mathf.PingPong(Time.time * 5, 1); // Flashing effect
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, alpha);
            }
        }
    }

    public void HurtPlayer(int damageToGive)
    {
        currentHealth -= damageToGive;
        audioManager.PlayGoblinSFX(audioManager.GoblinAttack);
        flashActive = true;
        flashCounter = flashLength;
        FindObjectOfType<UIManager>().UpdateHealthBar();

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        SpawnDeathEffect(transform.position);
        // Call audio manager function for game over sound
        if (audioManager != null)
        {
            audioManager.GameOverSFX(audioManager.PlayerDeath);
            Invoke("LoadNextScene", audioManager.PlayerDeath.length);
        }
    }

    private void LoadNextScene()
    {
        playerStorage.initialValue = playerPosition;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Single);
    }

    private void SpawnDeathEffect(Vector3 position)
    {
        if (playerDeathEffectPrefab != null)
        {
            GameObject deathEffect = Instantiate(playerDeathEffectPrefab, position, Quaternion.identity);
            Animator deathEffectAnimator = deathEffect.GetComponent<Animator>();
            if (deathEffectAnimator != null)
            {
                deathEffectAnimator.SetTrigger("PlayDeathAnimation");
            }
        }
    }
}