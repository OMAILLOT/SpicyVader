using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour, IPooledObject
{
    public GameObject[] chillyPeper;
    GameObject currentChillyPeper;
    int indexChillyPeper;
    [Space(10)]
    public GameObject[] balises;
    private GameManager gameManager;
    [Space(10)]
    public ParticleSystem particle;
    public GameObject playerLife;
    [Space(10)]
    public float PV = 50;
    private float saveBasePv;
    public float speed = 1;
    private float saveSpeed;
    public float dropChance = 0.3f;
    public float dropLifeChance = 0.05f;
    [Space(10)]
    public AudioClip[] audios;
    private int musicIndex;
    [Space(10)]
    public float chanceToFocusPlayer = 0.3f;
    private float rnd;
    private float randomTarget;

    private bool isStart = false;
    

    private WorldLevelManager worldLevelManager;
    //Défini si le special asteroid va a droite ou a gauche -- si il va à gauche c'est true
    private bool rightOrLeft = false;

    public void OnObjectSpawn()
    {
        if (!isStart)
        {
            saveBasePv = PV;
            saveSpeed = speed;
            isStart = true;
        }
        rightOrLeft = false;
        worldLevelManager = WorldLevelManager.Instance;
        PV = saveBasePv + worldLevelManager.asteroidsPV;
        speed = saveSpeed + worldLevelManager.asteroidsSpeed;

        gameManager = GameManager.Instance;
        indexChillyPeper = Random.Range(0, chillyPeper.Length);
        currentChillyPeper = chillyPeper[indexChillyPeper];
        
        if (gameObject.tag == "LittleAsteroid" || gameObject.tag == "BigAsteroid")
        {
            balises = GameObject.FindGameObjectsWithTag("balise");
            if (Random.Range(0f, 1f) > chanceToFocusPlayer)
            {
                rnd = Random.Range(1f, 15f);
                randomTarget = Random.Range(balises[0].transform.position.x, balises[1].transform.position.x);
            }
            else
            {
                randomTarget = PlayerMovement.Instance.transform.position.x;
            }

        } else {
            balises = GameObject.FindGameObjectsWithTag("SpecialRespawn");

            if (transform.position.x > 9)
            {
                rightOrLeft = true;
            }
        }



    }

    void FixedUpdate()
    {
        

        if (transform.position.y < -7.6)
            gameObject.SetActive(false);

        if (PV <= 0)
        {
            StartCoroutine(destroyAsteroid()); 
        }

        if (gameObject.tag == "LittleAsteroid" || gameObject.tag == "BigAsteroid")
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(randomTarget, balises[0].transform.position.y, balises[0].transform.position.z), (Time.deltaTime/2) * speed);
        } else
        {
            if (rightOrLeft)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(balises[0].transform.position.x, balises[0].transform.position.y, balises[0].transform.position.z), (Time.deltaTime / 2) * speed);
            } else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(balises[1].transform.position.x, balises[1].transform.position.y, balises[1].transform.position.z), (Time.deltaTime / 2) * speed);
            }
        }

        Vector3 targ = transform.position;
        targ.x = targ.x - randomTarget;
        targ.y = targ.y - balises[0].transform.position.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rnd * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerManager.Instance.PlayerHit();
        }

        if (collision.CompareTag("bullet") || collision.CompareTag("TinyBulletPlayer"))
        {
            PV -= PlayerManager.Instance.PlayerDamage;
        }
    }

    IEnumerator destroyAsteroid()
    {
        Instantiate(particle, transform.position, Quaternion.identity);
        musicIndex = Random.Range(0, audios.Length);
        AudioManager.Instance.PlayClipAt(audios[musicIndex], transform.position);
        if (currentChillyPeper.tag == "GreenChillyPeper" && !gameManager.isMaxTireRate || currentChillyPeper.tag == "RedHotChillyPeper" && !gameManager.isMaxDamage)
        {
            if (Random.Range(0f, 1f) <= dropLifeChance)
            {
                playerLife.transform.position = transform.position;
                Instantiate(playerLife);
            }
            else if (Random.Range(0f, 1f) <= dropChance)
            {
                    currentChillyPeper.transform.position = transform.position;
                    Instantiate(currentChillyPeper);
            }
        }
        gameObject.SetActive(false);
        yield return null;
    }


}
