using UnityEngine;

public class ZoneEffect : MonoBehaviour
{
    [Header("Zone Settings")]
    public bool destroyOnUse = false;

    [Header("Visual Feedback")]
    public Color speedColor = Color.green;
    public Color healColor = Color.blue;

    private Renderer zoneRenderer;
    private Color originalColor;

    void Start()
    {
        // initialize render and original color
        zoneRenderer = GetComponent<Renderer>();
        if(zoneRenderer != null)
        {
            originalColor = zoneRenderer.material.color;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // check if player collided with zone
        if(!other.CompareTag("Player"))
        {
            return;
        }

        // read the objects tag
        string zoneTag = gameObject.tag;
        Debug.Log("Player entered :" + zoneTag);


        // create instance of player movement to boost speed, health
        PlayerMovement player = other?.GetComponent<PlayerMovement>();
        PlayerHealth playerHealth = other?.GetComponent<PlayerHealth>();
        if(zoneTag == "SpeedZone" && player != null)
        {
            player.ApplySpeedBoost(2f, 3f);
        }else if(zoneTag == "HealZone")
        {
            playerHealth.Heal(1);
        }
    }
    void OnTriggerStay(Collider other)
    {
        // check if player collided with zone
        if(!other.CompareTag("Player"))
        {
            return;
        }
        if(destroyOnUse)
        {
            return;
        }

        // visual feedback for speedzone, healthzone
        if(gameObject.tag == "SpeedZone")
        {
            if(zoneRenderer != null)
            {
                // pulse effect calculation
                float pulse = Mathf.PingPong(Time.time * 3f, 0.5f) + 0.5f;
                zoneRenderer.material.color = Color.Lerp(speedColor, Color.white, pulse);
            }

        }else if(gameObject.tag == "HealZone")
        {
             if(zoneRenderer != null)
            {
                // pulse effect calculation
                float pulse = Mathf.PingPong(Time.time * 3f, 0.5f) + 0.5f;
                zoneRenderer.material.color = Color.Lerp(healColor, Color.white, pulse);
            }
        }
    }

}
