using UnityEngine;
using System.Collections;

public class enemyManager : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime = 3.0f;
    public Transform[] spawnPoints;
    public CharacterController player;

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
            int index = Random.Range(0, spawnPoints.Length);

            Instantiate(enemy, spawnPoints[index].position, spawnPoints[index].rotation);
        
    }
}
