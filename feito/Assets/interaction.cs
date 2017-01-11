using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction : MonoBehaviour {


    public GameObject medkit;
    public Transform[] spawnPoints;
    public bool activeMK = false;
    
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("MedkitSpawner", 5f, 5f);
    }


    void Update()
    {
        
    }

    void MedkitSpawner()
    {
        int index = Random.Range(0, spawnPoints.Length);
        if (!activeMK)
        {
            Instantiate(medkit, spawnPoints[index].position, Quaternion.identity);
            activeMK = true;
        }
    }
    public void ChangeMK(bool qq)
    {
        activeMK = qq;
        Debug.Log(qq);
    }
    
    
}
