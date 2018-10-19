using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{

    public ProjectileController projectilePrefabFire;
    public ProjectileController projectilePrefabIce;

    public createShield blue;
    public createShield red;

    //public GameObject destroyExplosionPrefab;
    public PlayerController player;
    public float velocity = 10;
    public GameObject spawnPoint;
    public float targetRadius = 10f;
    private float[] rhythm;
    private int counter;
    float nextFire;
    private float beat = 0.662f;

    public AudioSource bossMusic;
    public AudioSource inGameMusic;


    void Start()
    {
       

    }

    void OnEnable()
    {
        player.transform.position = new Vector3(-2.62f, 0.5f, -5.33f);
        player.transform.localEulerAngles = new Vector3(0, -45, 0);

        nextFire = Time.time;

        counter = 0;
        nextFire = Time.time + beat * 3;

        blue.shieldDuration = 0.1f;
        blue.coolDownDuration = 0.4f;

        red.shieldDuration = 0.1f;
        red.coolDownDuration = 0.4f;

        player.GetComponent<FirstPersonController>().enabled = false;

        bossMusic.Play();
        inGameMusic.Stop();
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
                if (counter == 66)
                {
                    SceneManager.LoadScene("Won");
                }

                shoot();

                nextFire = Time.time + beat;
                counter++;
            }
        }

        
    }

    private void shoot()
    {
        if (counter < 64)
        {
            ProjectileController p;
            if (Random.Range(-1, 1) < 0)
            {
                p = Instantiate<ProjectileController>(projectilePrefabFire);
            }
            else
            {
                p = Instantiate<ProjectileController>(projectilePrefabIce);
            }
            Vector3 bulletSpawn = spawnPoint.transform.position;
            p.transform.position = bulletSpawn;
            p.velocity = (this.player.transform.position - bulletSpawn).normalized * velocity;
        }  
    }
}