using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createShield : MonoBehaviour {

    public float shieldDuration = 0.3f;
    public float coolDownDuration = 0.5f;
    public KeyCode keyCode;
    
    private float coolDownTimer;
    private float shieldActiveTimer;

    private bool onCoolDown;
    private bool isShieldActive;

    // Use this for initialization
    void Start () {
        onCoolDown = false;
        isShieldActive = false;
        coolDownTimer = 0.0f;
        shieldActiveTimer = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {

        // activate the shield if the key is pressed, and begin counting how long is shield is active for as well as it's cooldown
        if (Input.GetKeyDown(keyCode) && !onCoolDown)
        {
            GetComponent<Renderer>().enabled = true;
            GetComponent<SphereCollider>().enabled = true;

            onCoolDown = true;
            isShieldActive = true;
        }

        
        if (onCoolDown)
        {
            coolDownTimer += Time.deltaTime;

            // if the shield is on cool down long enough, allow it to be activated again
            if (coolDownTimer > coolDownDuration)
            {
                onCoolDown = false;
                coolDownTimer = 0.0f;
            }
        }

        // manages how long the shield is active for 
        if (isShieldActive)
        {
            shieldActiveTimer += Time.deltaTime;

            if (shieldActiveTimer > shieldDuration)
            {
                GetComponent<Renderer>().enabled = false;
                GetComponent<SphereCollider>().enabled = false;

                isShieldActive = false;
                shieldActiveTimer = 0.0f;
            }
        }
	}
}
