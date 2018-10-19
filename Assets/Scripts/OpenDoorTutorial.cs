using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorTutorial : MonoBehaviour {

    public Animator hingeAnimator;

    // Use this for initialization
    void Start () {
        hingeAnimator = GetComponent<Animator>();

    }
    
    // Update is called once per frame
    void Update () {

    }
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Collided");
        if (col.gameObject.name == "Player")
        {
            Debug.Log("with Player");

            hingeAnimator.SetBool("unlocked",true);
        }
            
    
    }

    private void OnTriggerExit(Collider col)
    {

    }



}
