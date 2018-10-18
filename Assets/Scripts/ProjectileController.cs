using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ProjectileController : MonoBehaviour {

    public Vector3 velocity;

    public int damageAmount = 10;
    public string tagToDamage;
    public string shieldColour;
    GameObject player;
    HealthManager playerHealth;
    

    // Use this for initialization
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<HealthManager>();
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
        this.transform.Translate(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider col)
    {
        //if (col.gameObject.tag == tagToDamage)
        //{
        //    HealthManager healthManager = col.gameObject.GetComponent<HealthManager>();
        //    healthManager.ApplyDamage(damageAmount);

        //    Destroy(this.gameObject);
        //}
        if (col.gameObject.tag == "Player")
        {   
            playerHealth.ApplyDamage(damageAmount);
            
            if (this.tag == "IceDmg")
            {
                player.GetComponent<FirstPersonController>().applySlow();
            }
            Destroy(this.gameObject);
        }
        if (col.gameObject.tag == "Environment" || col.gameObject.name == shieldColour)
        {
            Destroy(this.gameObject);
        }
    }
}
