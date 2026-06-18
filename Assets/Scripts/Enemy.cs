using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damageAmount = 1;

    void OnTriggerEnter(Collider other)
    {
        // Safe tag check
        if (other.CompareTag("Player"))
        {
            // Try to get PlayerHealth component
            if (other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}