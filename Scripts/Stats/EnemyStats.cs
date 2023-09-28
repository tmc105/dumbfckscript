using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class EnemyStats : MonoBehaviour
{
    public string enemyType;
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject healthBarPrefab;
    public GameObject damageTextPrefab;
    public Transform damageTextSpawnPoint;
    public Canvas healthBarCanvas;
    private GameObject healthBar;
    public int monsterLevel = 30;
    private Transform transformofEnemy;
    private LootGenerator lootGenerator;
    public GameObject itemPrefab;
    public GameObject canvasContainer;
    public GameObject nameplateCanvas;
    public EnemyRarity enemyRarity;
    public PlayerExperience playerExperience;


    private void Start()
    {
        currentHealth = maxHealth;
        lootGenerator = GetComponent<LootGenerator>();
        healthBar = Instantiate(healthBarPrefab, healthBarCanvas.transform);
        healthBar.transform.SetParent(healthBarCanvas.transform, false);
        healthBar.GetComponent<EnemyHealthBar>().SetEnemyTransform(transform);
        playerExperience = GameObject.Find("Player").GetComponent<PlayerExperience>();
    }

    public void TakeDamage(int damage, DamageTypes damageType)
    {
        // Calculate damage based on enemy type and damage type
        int finalDamage = damage;
        // Reduce health and check for death
        currentHealth -= finalDamage;
        healthBar.GetComponent<EnemyHealthBar>().UpdateHealthBar(currentHealth, maxHealth);
        ShowDamageText(damage);



        if (currentHealth <= 0)
        {

            Die();

        }
    }
    private void ShowDamageText(int damage)
    {
        GameObject canvasContainer = GameObject.Find("Canvas");


        Vector3 worldPosition = transform.position + new Vector3(0, 1, 0); // Add a vertical offset
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        GameObject textObject = Instantiate(damageTextPrefab, screenPosition, Quaternion.identity, canvasContainer.transform);

        textObject.GetComponent<DamageText>().SetText(damage.ToString());
    }




    private void Die()
    {
        LootDrop();
        playerExperience.AddXP(25);
        // Destroy enemy object and health bar
        Destroy(gameObject);
        Destroy(healthBar);

    }

    private void LootDrop()
    {
        // Get a list of item drops
        List<Equipment> lootDrops = lootGenerator.GenerateLoot(monsterLevel, enemyRarity);

        // Iterate through the lootDrops list and instantiate each item as a game object
        foreach (Equipment lootDrop in lootDrops)
        {
            GameObject itemGameObject = Instantiate(itemPrefab, transform.position + Vector3.up * 2f, Quaternion.identity);
            itemGameObject.GetComponent<PickupItem>().item = lootDrop;
            itemGameObject.GetComponent<InventoryTooltip>().item = lootDrop;
            itemGameObject.GetComponent<InventoryTooltip>().tooltipPopup = canvasContainer.GetComponent<TooltipUI>();
        }
    }

}

