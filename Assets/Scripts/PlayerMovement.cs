using UnityEngine;
public class PlayerMovement : MonoBehaviour
{   
public float speed = 5.0f;
    void Update()
    {
       /* float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
       Vector3 movement = new Vector3(horizontal, vertical, 0f);
       transform.Translate(movement * speed * Time.deltaTime);*/
       
       
       float currentSpeed = speed;
       // player can move faster using leftshift
        if(Input.GetKey(KeyCode.LeftShift))
       {
       
      currentSpeed = speed * 2f;
       
       }
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
       
       
    }
}

