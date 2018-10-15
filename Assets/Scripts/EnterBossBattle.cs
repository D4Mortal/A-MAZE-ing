using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBossBattle : MonoBehaviour {
    public BossController bossController;
	// Use this for initialization
	void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        bossController.GetComponent<BossController>().enabled = true;
    }
}
