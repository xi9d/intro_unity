using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 velocity;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
        Debug.Log($"Bullet spawned at {transform.position}, will destroy in {lifetime}s");
    }

    void Update()
    {
        transform.position += velocity * Time.deltaTime;
        // Optional: if velocity is zero, log a warning once
        if (velocity == Vector3.zero)
        {
            Debug.LogWarning("Bullet velocity is zero!");
        }
    }

    public void SetDirection(Vector3 dir)
    {
        velocity = dir;
        Debug.Log($"Bullet velocity set to: {velocity}");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Bullet hit: {other.gameObject.name} with tag {other.tag}");
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
            {
                enemyHealth.TakeDamage(1);
                Destroy(gameObject);
                Debug.Log("Enemy damaged, bullet destroyed.");
            }
            else
            {
                Debug.LogWarning($"Enemy {other.gameObject.name} has no EnemyHealth component!");
            }
        }
    }
}
