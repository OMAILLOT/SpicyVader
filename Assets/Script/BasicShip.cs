using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShip : MonoBehaviour
{
    public float PV = 50;
    public float speed = 3;
    public float TireRate;
    public float dropChance = 0.3f;
    public float dropLifeChance = 0.05f;
    public ParticleSystem particleShoot;
    public ParticleSystem Explosion;
    public AudioClip[] ExplosionSound;
    [Space(5)]
    //public GameObject mecanicPiece;
    public GameObject playerLife;
    public GameObject[] grid;
    public GameObject bullet;
    public AudioClip[] shootAudios;
    private int musicIndex;


    float nextActionTime = 0.0f;
    WorldLevelManager worldLevelManager;
    SpawnBasicShip spawnBasicShip;
    int howManyCell;
    int whatCell;
    bool stopMove;

    private void Awake()
    {
        spawnBasicShip = SpawnBasicShip.instance;
        worldLevelManager = WorldLevelManager.instance;
    }
    void Start()
    {
        grid = GameObject.FindGameObjectsWithTag("BaliseToGoBasicShip");
        findWhatCell();
        PV += worldLevelManager.basicShipPV;
        speed += worldLevelManager.basicShipSpeed;
        dropChance += worldLevelManager.basicShipDropChance;
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!stopMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, grid[whatCell].transform.position, Time.deltaTime * speed);
            if (transform.position == grid[whatCell].transform.position)
            {
                stopMove = true;
                nextActionTime = Time.time+0.5f;
                particleShoot.transform.position = new Vector3(transform.position.x,transform.position.y - 0.3f, transform.position.z);
                
            }
        } else
        {
            if (transform.position.y >= 8)
            {
                Destroy(gameObject);
            }
            if (PV <= 0)
            {
                Instantiate(Explosion, transform.position, Quaternion.identity);
                musicIndex = Random.Range(0, ExplosionSound.Length);
                AudioManager.instance.PlayClipAt(ExplosionSound[musicIndex], transform.position);
                spawnBasicShip.countShipDestroy++;
               // Destroy(particleShoot);
                Destroy(gameObject);
            }
            if (Time.time >= nextActionTime)
            {
                StartCoroutine(Shoot());
            }
        }

    }


    void findWhatCell()
    {
        var stop = false;
        switch (worldLevelManager.basicShipEventlevel)
        {
            case 1:
                howManyCell = 4;
                break;
            case 2:
                howManyCell = 8;
                break;
            case 3:
                howManyCell = 12;
                break;
            case 4:
                howManyCell = 16;
                break;
        }
        
        for (int i = 0; i < howManyCell; i++)
        {

            if (spawnBasicShip.lineTake[i] == 0 && !stop)
            {
                Debug.Log(i);
                spawnBasicShip.lineTake[i] = 1;
                whatCell = i;
                stop = true;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet") || collision.CompareTag("TinyBulletPlayer"))
        {
            PV -= GameManager.Instance.PlayerDamage;
        }

        if (collision.CompareTag("Player"))
        {
            PlayerMovement.instance.PlayerHit();
        }
    }

    IEnumerator Shoot()
    {
        //Instantiate(particleShoot);
        nextActionTime += TireRate + Random.Range(0,1f);
        musicIndex = Random.Range(0, shootAudios.Length);
        AudioManager.instance.PlayClipAt(shootAudios[musicIndex], transform.position);
        bullet.transform.position = transform.position;
        Instantiate(bullet);
        yield return null;
    }
}
