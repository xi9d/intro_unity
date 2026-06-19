using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damageAmount = 1;

    void OnTriggerEnter(Collider other)
{
    Debug.Log($"Enemy touched: {other.gameObject.name}");
    if (other.CompareTag("Player"))
    {
        if (other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(damageAmount);
            Debug.Log($"Player took {damageAmount} damage.");
        }
        else
        {
            Debug.LogWarning("Player has no PlayerHealth component!");
        }
    }
}
}
