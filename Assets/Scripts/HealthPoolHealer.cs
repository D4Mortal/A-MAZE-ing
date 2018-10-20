using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoolHealer : MonoBehaviour {
    GameObject player;
    HealthManager playerHealth;
    float healFactor;

	// Use this for initialization
	void Start () {
        Debug.Log("Initialise pool");
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<HealthManager>();
        healFactor = 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider col)
    {
        Debug.Log("Trigger stay");
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Fixed Delta Time: " + Time.fixedDeltaTime);
            Debug.Log("Delta Time: " + Time.deltaTime);
            Debug.Log("Fixed Unscaled Delta Time: " + Time.fixedUnscaledDeltaTime);
            playerHealth.ApplyHeal((int)(Time.fixedDeltaTime * healFactor));
        }
    }
}
