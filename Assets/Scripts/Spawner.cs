using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject projectile;
    
    
    
    void Update()
    {
         //press G to spawn a projectile
         if(Input.GetKey(KeyCode.G)){
            Vector3 randomPos = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), Random.Range(-4f, 4f));
            GameObject obj = Instantiate(projectile, randomPos, Quaternion.identity);
            
            Destroy(obj,2f);
            }
          
          
    }
}
