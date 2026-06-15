using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit!");

            // Optional: log collision details
            Debug.Log("Impact force: " + collision.relativeVelocity.magnitude);
            ContactPoint contact = collision.GetContact(0);
            Debug.Log("Hit point: " + contact.point);

            // Destroy the player object
            Destroy(collision.gameObject);
        }
    }
}
