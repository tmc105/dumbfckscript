using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public float popDuration;
    public float duration;
    public float fadeSpeed;
    public float initialUpwardForce;
    private TextMeshProUGUI damageText;
    private float timer;
    private Vector3 randomDirection;
    [SerializeField] private float popSize = 2f;
    [SerializeField] private float randomForce = 1f;
    [SerializeField] private float popExponent = 2f;


    private void Awake()
    {
        damageText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(PopText());
        randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * randomForce;
    }

    private void Update()
    {
        // Move the text upward and fade it out.
        Vector3 movement = new Vector3(randomDirection.x, randomDirection.y + initialUpwardForce, randomDirection.z);
        transform.position += movement * Time.deltaTime;


        timer += Time.deltaTime;
        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }

    public void SetText(string text)
    {
        damageText.text = text;
    }

    IEnumerator PopText()
    {
        float t = 0;
        Vector3 initialScale = transform.localScale;

        while (t < popDuration)
        {
            float popProgress = Mathf.Sin(Mathf.PI * t / popDuration);
            transform.localScale = initialScale * (1 + (popSize - 1) * Mathf.Pow(popProgress, popExponent));
            t += Time.deltaTime;
            yield return null;
        }

        transform.localScale = initialScale;
    }
}


