using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class medkitRotate : MonoBehaviour {

    GameObject player;
    public GameObject medkitManager;
    interaction a;
    bool setBool = false;
    // Use this for initialization
    void Start () {
        //medkitManager = GameObject.FindGameObjectWithTag("manager");
        a = medkitManager.GetComponent<interaction>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        UnityStandardAssets.Characters.FirstPerson.FirstPersonController plc = player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        if ((other.CompareTag("Player"))&&(plc.FullHealth()!=100))
        {
            
            plc.GainHealth(20);
            Debug.Log("COME SOPA, COME!");
            a.ChangeMK(false);
            Debug.Log("falsinho");
            Destroy(GameObject.FindGameObjectWithTag("packerino"));
            
        }
    }
}
