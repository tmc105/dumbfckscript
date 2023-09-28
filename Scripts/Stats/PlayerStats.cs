using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    // [SerializeField] private int level = 1;
    [SerializeField] private float maxHealth = 100;
    public List<Stat> playerStats;

    public float currentHealth;
    public float rage = 100;
    public float maxRage = 100;
    public float rageDegenRate = 2f;
    public Image healthGlobe;
    public Slider healthSlider, rageSlider;
    public Image rageGlobe;
    public int armor;
    public BaseStats baseStat;
    public int baseStatValue;
    public int stamina;
    // float healthPerStamina = 0.5f;
    // float spellPowerPerIntellect = 0.5f;
    // float attackPowerPerStrength = 0.5f;
    // float attackPowerPerAgility = 0.5f;
    int attackPower;

    #region HealthPotionVariables
    private float healthPotionCooldown = 10f;
    private float healthPotionTimer = 0f;
    private float healthPotionHealPercentage = 0.4f;
    private float healthPotionOverTimePercentage = 0.2f;
    private float healthPotionOverTimeDuration = 5f;
    private bool isHealthPotionActive = false;
    private float healthPotionOverTimeHealRate;
    #endregion




    // private List<Item> equippedItems = new List<Item>();

    private void Start()
    {
        foreach (Stat stat in playerStats)
        {
            stat.currentValue = 0;
        }
        // Calculate the current health, armor, and resistances based on the base values and the player's level
        currentHealth = maxHealth;
        UpdateManaBar();
    }

    private void Update()
    {

        healthPotionTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && healthPotionTimer <= 0)
        {
            UseHealthPotion();
        }
        RegenerateMana();

        UpdateHealthBar();

    }

    public void TakeDamage(int damageAmount, string damageType)
    {
        // damageAmount = CalculateFinalDamage(damageAmount, damageType);
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void RegenerateMana()
    {
        rage -= rageDegenRate * Time.deltaTime;
        rage = Mathf.Clamp(rage, 0, maxRage);
        // Update the mana bar UI when the mana value changes
        UpdateManaBar();

    }

    void UpdateHealthBar()
    {
        healthGlobe.materialForRendering.SetFloat("_FillLevel", currentHealth / maxHealth);
        healthSlider.value = currentHealth / maxHealth;

    }
    private void Die()
    {
        // Restart the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Player has died!");
    }

    void UpdateManaBar()
    {
        rageGlobe.materialForRendering.SetFloat("_FillLevel", rage / maxRage);
        rageSlider.value = rage / maxRage;
    }

    private IEnumerator HealthPotionOverTime()
    {
        isHealthPotionActive = true;

        float totalHealAmount = maxHealth * healthPotionOverTimePercentage;
        float healInterval = 0.5f;
        int numberOfHeals = Mathf.FloorToInt(healthPotionOverTimeDuration / healInterval);
        float healPerTick = totalHealAmount / numberOfHeals;

        for (int i = 0; i < numberOfHeals; i++)
        {
            Heal(healPerTick);
            yield return new WaitForSeconds(healInterval);
        }

        isHealthPotionActive = false;
    }

    private void UseHealthPotion()
    {
        healthPotionTimer = healthPotionCooldown;

        // Heal the player instantly
        float healAmount = maxHealth * healthPotionHealPercentage;
        Heal(healAmount);

        // Start healing over time
        if (!isHealthPotionActive)
        {
            StartCoroutine(HealthPotionOverTime());
        }
    }




}

