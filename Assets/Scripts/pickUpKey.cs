using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpKey : MonoBehaviour
{

    public keyManager keyManager;

    // Use this for initialization
    void Start()
    {
        this.keyManager = GameObject.Find("KeyManager").GetComponent<keyManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            keyManager.keys -= 1;
            Destroy(this.gameObject);
        }

        
    }
}
