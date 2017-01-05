using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction : MonoBehaviour {


    public GameObject medkit;
    public bool activeMK = false;
    
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("MedkitSpawner", 5, 5);
    }


    void Update()
    {
        
    }

    void MedkitSpawner()
    {
        if (!activeMK)
        {
            Instantiate(medkit, new Vector3(Random.Range(-45f, -40f), 8f, -222f), Quaternion.identity);
            activeMK = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Destroy(medkit);
    }
    
}
