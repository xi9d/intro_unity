using UnityEngine;

public class ZoneEffect : MonoBehaviour
{
    [Header("Zone Settings")]
    public bool destroyOnUse = false;  // True = one-time pickup, False = permanent zone

    [Header("Visual Feedback")]
    public Color speedColor = Color.yellow;
    public Color healColor = Color.green;
    public Color jumpColor = Color.cyan;
    public Color deathColor = Color.red;

    private Renderer zoneRenderer;
    private Color originalColor;

    void Start()
    {
        zoneRenderer = GetComponent<Renderer>();
        if (zoneRenderer != null)
        {
            originalColor = zoneRenderer.material.color;
        }

        // Debug: Show what tag this zone has
        Debug.Log($"📍 Zone '{gameObject.name}' has tag: '{gameObject.tag}'");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only affect the player
        if (!other.CompareTag("Player"))
            return;

        // Get the player component
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player == null)
        {
            Debug.LogError("❌ PlayerMovement component not found on player!");
            return;
        }

        // Read this object's tag
        string zoneTag = gameObject.tag;
        Debug.Log($"🎯 Player entered: '{zoneTag}'");

        // --- APPLY EFFECT BASED ON TAG ---
        if (zoneTag == "SpeedZone")
        {
            player.ApplySpeedBoost(2f, 3f);  // 2x speed for 3 seconds
            Debug.Log("🏃 Speed boost zone activated!");
            ChangeColor(speedColor);
        }
        else if (zoneTag == "HealZone")
        {
            player.Heal(1);  // Heal 1 HP
            Debug.Log("💚 Healing zone activated!");
            ChangeColor(healColor);
        }
        else if (zoneTag == "JumpZone")
        {
            player.ApplyJumpBoost(2f, 3f);  // 2x jump for 3 seconds
            Debug.Log("🚀 Jump boost zone activated!");
            ChangeColor(jumpColor);
        }
        else if (zoneTag == "DeathZone")
        {
            player.TakeDamage(1);  // Take 1 damage
            player.ResetPosition();  // Reset to start
            Debug.Log("☠️ Death zone activated! Player reset.");
            ChangeColor(deathColor);
        }
        else
        {
            Debug.LogWarning($"⚠️ Unknown zone tag: '{zoneTag}'. Did you type it correctly?");
            return;
        }

        // Destroy zone if it's a one-time pickup
        if (destroyOnUse)
        {
            Debug.Log($"🗑️ Zone '{gameObject.name}' destroyed!");
            Destroy(gameObject);
        }
    }

    // --- OnTriggerStay: Continuous effects (optional) ---
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        // Only for permanent zones (not destroyed on use)
        if (destroyOnUse)
            return;

        // Visual feedback: Pulse effect for HealZone
        if (gameObject.tag == "HealZone")
        {
            if (zoneRenderer != null)
            {
                float pulse = Mathf.PingPong(Time.time * 2f, 0.5f) + 0.5f;
                zoneRenderer.material.color = Color.Lerp(healColor, Color.white, pulse);
            }
        }

        // Visual feedback: Pulse effect for SpeedZone
        if (gameObject.tag == "SpeedZone")
        {
            if (zoneRenderer != null)
            {
                float pulse = Mathf.PingPong(Time.time * 3f, 0.5f) + 0.5f;
                zoneRenderer.material.color = Color.Lerp(speedColor, Color.white, pulse);
            }
        }

        // Visual feedback: Pulse effect for JumpZone
        if (gameObject.tag == "JumpZone")
        {
            if (zoneRenderer != null)
            {
                float pulse = Mathf.PingPong(Time.time * 2.5f, 0.5f) + 0.5f;
                zoneRenderer.material.color = Color.Lerp(jumpColor, Color.white, pulse);
            }
        }
        // Visual feedback: Pulse effect for DeathZone
        if (gameObject.tag == "DeathZone")
        {
            if (zoneRenderer != null)
            {
                float pulse = Mathf.PingPong(Time.time * 2.5f, 0.5f) + 0.5f;
                zoneRenderer.material.color = Color.Lerp(deathColor, Color.white, pulse);
            }
        }

        // Optional: Continuous healing in HealZone
        // (Uncomment if you want healing every second while inside)
        /*
        if (gameObject.tag == "HealZone")
        {
            // Add a timer system here for periodic healing
            // This is a bonus challenge for students!
        }
        */
    }

    // --- OnTriggerExit: Reset visual when leaving ---
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Debug.Log($"🚪 Player left: '{gameObject.tag}'");

        // Restore original color
        if (zoneRenderer != null && zoneRenderer.material != null)
        {
            zoneRenderer.material.color = originalColor;
        }
    }

    // Helper method to change color
    private void ChangeColor(Color newColor)
    {
        if (zoneRenderer != null)
        {
            zoneRenderer.material.color = newColor;
        }
    }
}