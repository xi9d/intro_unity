using UnityEngine;
public class PlayerMovement : MonoBehaviour
{   
public float speed = 5.0f;
public float currentSpeed; 
private Rigidbody rb;
private Vector3 startPosition;
private Quaternion  startRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 4f;
        //rb.linearDumping = 0.5f;
        rb.useGravity = true;


        // gameobject origin
        startPosition = transform.position;
        startRotation = transform.rotation;
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

       // reset player position
       if(Input.GetKey(KeyCode.R))
       {
        transform.position = startPosition;
        transform.rotation = startRotation;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

       }
      // add rigid body to the projectiles(cube), make sure when player collides with the projectiles, they get knocked off 
       
       
    }
    void FixedUpdate()
    {

        currentSpeed = speed;
        // movement 
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

       // player can jump with space
       if(Input.GetKey(KeyCode.Space))
       {

        rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
       }
    }



}
