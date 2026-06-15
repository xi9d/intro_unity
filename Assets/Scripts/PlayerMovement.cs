using UnityEngine;
public class PlayerMovement : MonoBehaviour
{   
public float speed = 5.0f;
private Rigidbody rb;
private Vector3 startPosition;
private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;


        rb.mass = 1f;
        rb.linearDamping = 0.5f;
        rb.useGravity = true;
    }
    void Update()
    {
       /* float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
       Vector3 movement = new Vector3(horizontal, vertical, 0f);
       transform.Translate(movement * speed * Time.deltaTime);*/
       
       
       
       // player can move faster using leftshift
        if(Input.GetKey(KeyCode.LeftShift))
       {
       
         currentSpeed = speed * 2f;
       
       }

       // Press R to reset position
       if(Input.GetKey(KeyCode.R))
       {
        transform.position = startPosition;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
       }
    
    }
    void FixedUpdate()
    {
        // player can move with W
       if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
       {
       
       transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
       
       
       }
       // player can move with S
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
       {
       
       transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);
       
       }
       // player can move with A
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
       {
       
       transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
       
       }
       // player can move with D
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
       {
       
       transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
       
       }

       // space to jump
       if (Input.GetKeyDown(KeyCode.Space)) 
        { 
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse); 
        }
    
    }
    void ContinousMovement()
    {
        float move = Input.GetAxisRaw("Horizontal"); 
   Vector3 force = new Vector3(move * 10f, 0, 0); 
   rb.AddForce(force, ForceMode.Force);
    }

    // colliders
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Game Over");
            Destroy(gameObject);
        }
    }

}

