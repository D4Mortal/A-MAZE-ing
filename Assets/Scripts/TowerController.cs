using UnityEngine;
using System.Collections;

public class TowerController : MonoBehaviour
{

    public ProjectileController projectilePrefab;
    //public GameObject destroyExplosionPrefab;
    public PlayerController player;
    public float velocity = 10;
    public GameObject spawnPoint;
    public float targetRadius = 10f;
    float fireRate;
    float nextFire;

    void Start()
    {
        // Get player reference if none attached already
        if (this.player == null)
        {
            this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            
        }
        fireRate = 1f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        HealthManager healthManager = this.gameObject.GetComponent<HealthManager>();
        var distance = Vector3.Distance(this.transform.position, this.player.transform.position);
        if (distance < targetRadius)
        {
            // Fires a projectile every 3 seconds
            if (Time.time > nextFire)
            {
                ProjectileController p = Instantiate<ProjectileController>(projectilePrefab);
                Vector3 bulletSpawn = spawnPoint.transform.position;
                p.transform.position = bulletSpawn;
                p.velocity = (this.player.transform.position - bulletSpawn).normalized * velocity;
                nextFire = Time.time + fireRate;
            }
        }
    }
}