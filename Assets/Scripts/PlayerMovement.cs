using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    [Header("Movement Settings")]
    public float speed = 5.0f;
    public float currentSpeed; 
    
    [Header("Jump Settings")]
    public float jumpForce = 5f;
    private float currentJumpForce;
    private float originalJumpForce;
    
    [Header("Health Settings")]
    public int maxHealth = 5;
    private int currentHealth;

    private Rigidbody rb;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private float originalSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 4f;
        rb.useGravity = true;

        startPosition = transform.position;
        startRotation = transform.rotation;

        // --- Initialize all values ---
        originalSpeed = speed;
        currentSpeed = speed;
        originalJumpForce = jumpForce;
        currentJumpForce = jumpForce;
        currentHealth = maxHealth;

        Debug.Log($"✅ Player initialized: Speed={currentSpeed}, Jump={currentJumpForce}, Health={currentHealth}/{maxHealth}");
    }

    void Update()
    {
        // Sprint with LeftShift (temporary boost)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = speed * 2f;
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && currentSpeed != speed)
        {
            // Only reset if not boosted by power-up
            // (Power-up uses a different system)
        }

        // Reset player position with R key
        if (Input.GetKey(KeyCode.R))
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            currentHealth = maxHealth;
            Debug.Log($"🔄 Respawned! Health restored to {currentHealth}");
        }
    }

    void FixedUpdate()
    {
        // Movement with WASD or Arrow Keys
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
        }

        // Jump with Space (uses currentJumpForce)
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * currentJumpForce, ForceMode.Impulse);
        }
    }

    // ========== PUBLIC METHODS FOR POWER-UPS ==========

    // --- SPEED BOOST ---
    public void ApplySpeedBoost(float multiplier, float duration)
    {
        CancelInvoke("ResetSpeed");
        currentSpeed = originalSpeed * multiplier;
        Debug.Log($"⚡ Speed boosted! Speed: {currentSpeed}");
        Invoke("ResetSpeed", duration);
    }

    private void ResetSpeed()
    {
        currentSpeed = originalSpeed;
        Debug.Log($"✅ Speed restored to: {currentSpeed}");
    }

    // --- HEAL ---
    public void Heal(int amount)
    {
        int oldHealth = currentHealth;
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        int actualHeal = currentHealth - oldHealth;
        Debug.Log($"💚 Healed {actualHeal} HP! Health: {currentHealth}/{maxHealth}");
    }

    // --- JUMP BOOST ---
    public void ApplyJumpBoost(float multiplier, float duration)
    {
        CancelInvoke("ResetJump");
        currentJumpForce = originalJumpForce * multiplier;
        Debug.Log($"🚀 Jump boosted! Jump force: {currentJumpForce} (was {originalJumpForce})");
        Invoke("ResetJump", duration);
    }

    private void ResetJump()
    {
        currentJumpForce = originalJumpForce;
        Debug.Log($"✅ Jump restored to: {currentJumpForce}");
    }

    // --- TAKE DAMAGE (for death zones) ---
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"💥 Took {damage} damage! Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log("💀 GAME OVER! Respawn triggered.");
            transform.position = startPosition;
            currentHealth = maxHealth;
            rb.linearVelocity = Vector3.zero;
            Debug.Log($"🔄 Respawned! Health restored to {currentHealth}");
        }
    }

    // --- Get Health (for UI or other scripts) ---
    public int GetHealth()
    {
        return currentHealth;
    }

    // --- Reset Method (for respawn zones) ---
    public void ResetPosition()
    {
        transform.position = startPosition;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}