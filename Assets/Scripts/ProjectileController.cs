using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public Vector3 velocity;
    
    public int damageAmount = 10;
    public string tagToDamage;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 3f);
	}
	
	// Update is called once per frame
	void Update () {
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
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("hello");
            Destroy(this.gameObject);
        }
    }
}
