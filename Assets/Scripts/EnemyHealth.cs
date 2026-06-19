using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 2;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"Took Damage new health:{health}");
        if (health <= 0)
        {
           // Add score if you have ScoreManager
           
             Destroy(gameObject);
        }
    }
}
