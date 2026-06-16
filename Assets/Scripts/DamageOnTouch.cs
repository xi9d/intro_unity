using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    
    void OnCollisionEnter(Collision collision)
    {
        // check if enemy collided with player
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit player!");

            // collision details
            Debug.Log("Impact force: " + collision.relativeVelocity.magnitude);
            ContactPoint contact = collision.GetContact(0);
            Debug.Log("Hit Point: " + contact.point);

            // destroy enemy on contact
            Destroy(gameObject);
        }
    }


    // onCollisionStay is called every frame while the collision continues
    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is still touching...");
        }
    }

    // onCollisionExit is called ONCE when collision end (object separate)
    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player left contact . (Exit)");
        }
    }
}
