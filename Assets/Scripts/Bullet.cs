using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 velocity;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    public void SetDirection(Vector3 dir)
    {
        velocity = dir;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
            {
                enemyHealth.TakeDamage(1); // bullet damage
                Destroy(gameObject);
            }
        }
    }
}