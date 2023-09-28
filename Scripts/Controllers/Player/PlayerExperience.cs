using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerExperience : MonoBehaviour
{
    public int currentLevel = 1;
    public int maxLevel = 50;
    public float currentXP = 0;
    public float baseXP = 100;
    public float exponent = 1.5f;

    public Image xpBar;
    public TextMeshProUGUI levelText;
    public GameObject levelUpParticleEffectPrefab;
    public AudioClip levelUpSoundEffect;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        xpBar.fillAmount = currentXP / CalculateLevelXP(currentLevel);
        levelText.text = currentXP.ToString() + " / " + CalculateLevelXP(currentLevel).ToString();
    }

    private float CalculateLevelXP(int level)
    {
        return baseXP * Mathf.Pow(level, exponent);
    }

    public void AddXP(float xp)
    {
        if (currentLevel >= maxLevel)
        {
            return;
        }

        currentXP += xp;

        while (currentXP >= CalculateLevelXP(currentLevel) && currentLevel < maxLevel)
        {
            currentXP -= CalculateLevelXP(currentLevel);
            currentLevel++;
            LevelUp();
        }

        UpdateXPBar();
    }

    private void LevelUp()
    {
        // Instantiate level-up particle effect
        if (levelUpParticleEffectPrefab != null)
        {
            GameObject levelUpEffect = Instantiate(levelUpParticleEffectPrefab, transform.position, Quaternion.identity);
            Destroy(levelUpEffect, 5f);
        }

        // Play level-up sound effect
        if (levelUpSoundEffect != null && audioSource != null)
        {
            audioSource.PlayOneShot(levelUpSoundEffect);
        }
    }

    private void UpdateXPBar()
    {
        if (xpBar != null)
        {
            xpBar.fillAmount = currentXP / CalculateLevelXP(currentLevel);
            levelText.text = currentXP.ToString() + " / " + CalculateLevelXP(currentLevel).ToString();
        }
    }
}
