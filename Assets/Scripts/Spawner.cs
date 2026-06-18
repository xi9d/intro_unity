using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject projectile;
    
    public int enemyCount = 2;
    public Vector3 spawnAreaSize = new Vector3(10, 0, 10);

    void Start()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(-spawnAreaSize.x/2, spawnAreaSize.x/2),
                0,
                Random.Range(-spawnAreaSize.z/2, spawnAreaSize.z/2)
            ) + transform.position;
            Instantiate(projectile, randomPos, Quaternion.identity);
        }
    }
}
