using UnityEngine;

public class GroundEffectDoT : MonoBehaviour
{
    public float damageInterval = 1f;
    public LayerMask enemyLayer;
    public PlayerSkill playerSkill;

    private float lastDamageTime;

    private void Start()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3f, enemyLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            EnemyStats enemyStats = hitCollider.GetComponent<EnemyStats>();

            if (enemyStats != null)
            {
                enemyStats.TakeDamage(15, playerSkill.damageType);
            }
        }
        lastDamageTime = Time.time;
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        if (Time.time - lastDamageTime >= damageInterval)
        {
            ApplyDamage();
            lastDamageTime = Time.time;
        }

    }

    private void ApplyDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3f, enemyLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            EnemyStats enemyStats = hitCollider.GetComponent<EnemyStats>();

            if (enemyStats != null)
            {
                enemyStats.TakeDamage(5, playerSkill.damageType);
            }
        }
    }
}
