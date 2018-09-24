using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyManager : MonoBehaviour {
    public int keys;
    public Component doorCollider;

    // Use this for initialization
    void Start () {
        keys = transform.childCount;
	}
	
	// Update is called once per frame
	void Update () {
		if (keys == 0)
        {
            doorCollider.GetComponent<BoxCollider>().isTrigger = true;
        }
	}
}
