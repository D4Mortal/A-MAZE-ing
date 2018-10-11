using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {
    public PlayerController player;
    // Use this for initialization
    void Start () {
        if (this.player == null)
        {
            this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(this.player.transform.position);

    }
}
