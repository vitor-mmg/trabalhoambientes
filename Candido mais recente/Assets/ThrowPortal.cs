using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPortal : MonoBehaviour
{

    public GameObject LeftPortal;
    public GameObject RightPortal;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("z"))
        {
            Debug.Log("tecla z");
            throwPortal(LeftPortal);
            
        }
        if(Input.GetKey("x"))
        {
            Debug.Log("tecla x");
            throwPortal(RightPortal);
        }
    }
    void throwPortal(GameObject portal)
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            Quaternion hitObjectRotation = Quaternion.LookRotation(hit.normal);
            portal.transform.position = hit.point;
            portal.transform.rotation = hitObjectRotation;
        }
    }
}
