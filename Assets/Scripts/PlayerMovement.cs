using UnityEngine;
public class PlayerMovement : MonoBehaviour
{   

private Rigidbody rb;
private Vector3 startPosition;
private Quaternion  startRotation;

    [Header("Movement settings")]
    public float speed = 5.0f;
    public float currentSpeed; 
    public float originalSpeed;

    [Header("Health Settings")]
    public int maxHealth = 5;
    private int currentHealth;

    [Header("Bullet settings")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 4f;
        //rb.linearDumping = 0.5f;
        rb.useGravity = true;


        // gameobject origin
        startPosition = transform.position;
        startRotation = transform.rotation;


        // initialize movement settings
        originalSpeed = speed;
        currentSpeed = speed;
        currentHealth = maxHealth;
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
        void LateUpdate() // or Update - rotation after movement
    {
        // ---- Rotate towards mouse ----
        RotateTowardsMouse();

        // ---- Shooting ----
        if (Input.GetButtonDown("Fire1")) 
        {
            Shoot();
        }

        // ---- Reset position ----
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    // speed boost
    public void ApplySpeedBoost(float multiplier, float duration)
    {
        CancelInvoke("ResetSpeed");
        currentSpeed = originalSpeed * multiplier;
        Debug.Log($"Speed Bootsted to{currentSpeed}");
        Invoke("ResetSpeed", duration);
    }
   public void ResetSpeed()
    {
        currentSpeed = originalSpeed;
        Debug.Log($"Speed restored:{currentSpeed}");
    }
  
    // shoot bulletrs
  public void Shoot()
{
    if (bulletPrefab == null)
    {
        Debug.LogError("Bullet prefab is not assigned in the Inspector!");
        return;
    }
    if (bulletSpawnPoint == null)
    {
        Debug.LogError("Bullet spawn point is not assigned in the Inspector!");
        return;
    }

    GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

    // Use TryGetComponent (lesson best practice)
    if (bullet.TryGetComponent<Bullet>(out Bullet bulletScript))
    {
        Vector3 direction = bulletSpawnPoint.forward * bulletSpeed;
        bulletScript.SetDirection(direction);
        Debug.Log($"Bullet spawned! Direction: {direction}");
    }
    else
    {
        Debug.LogError("The bullet prefab is missing the Bullet component!");
    }
}
    
   void RotateTowardsMouse()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist;
        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Vector3 direction = targetPoint - transform.position;
            direction.y = 0;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(direction);
        }
    }


}
