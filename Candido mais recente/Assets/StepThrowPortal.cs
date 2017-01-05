using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepThrowPortal : MonoBehaviour {

    public GameObject otherPortal;
    public bool onCooldown = false;
    public float timer = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (onCooldown) timer += Time.deltaTime;
        if (timer >= 2)
        {
            onCooldown = false;
            timer = 0f;
        }
	}
    void OnTriggerEnter(Collider other)
    {
        if((other.tag == "Player") && (!onCooldown))
        {
            onCooldown = true;
            other.transform.position = otherPortal.transform.position + otherPortal.transform.forward *10f;
            
        }
    }
}
