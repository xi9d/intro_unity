using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 10;
    private int currentHealth;


    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Health: "+ currentHealth);
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) 
        {   
            currentHealth = 0;
        }
        
        if (currentHealth == 0)
        {
            Debug.Log("Player Died!");
            // You can add game over logic here
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        } 
        
    }

    
}