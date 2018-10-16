using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Music");
        Destroy(gameObject);
    }

}
