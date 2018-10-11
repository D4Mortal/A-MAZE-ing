 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour {

    public Animation hinge;
    private bool inRange;
	// Use this for initialization
	void Start () {
        inRange = false;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (inRange)
        {
            hinge.Play();

            
        }

    }
    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.name == "Player")
        {
            inRange = true;
        }
            
    
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            inRange = false;
        }
    }



}
