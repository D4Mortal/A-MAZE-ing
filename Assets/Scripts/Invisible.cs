using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : MonoBehaviour {
    public GameObject Obj;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        MeshRenderer m = Obj.GetComponent<MeshRenderer>();
        m.enabled = false;
    }
}
