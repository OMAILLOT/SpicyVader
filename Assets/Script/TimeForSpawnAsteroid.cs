using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeForSpawnAsteroid : MonoBehaviour
{
    
    [Space(5)]
    public float timeToRespawn = 2f;
    public float reduceTimeToRespawn = 0.5f;
    [Space(5)]
    private float nextActionTime = 5f;
    public int Delay = 3;
    public float multiplicatorDelay;
    private int saveDelay;
    [Space(5)]
    public GameObject[] asteroids;
    public GameObject[] posBalises;
    private float randomSpawnPosX;
    GameObject currentAsteroid;
    int indexAsteroid;
    [Space(5)]
    //public Asteroids[] asteroidsScript;
    public EventManager eventManager;
    [Space(5)]
    public GameObject[] specialsAsteroids;
    public GameObject[] posSpecialBalises;
    public float chanceToSpawnSpecialAsteroid;
    private GameObject currentSpecialAsteroid;
    private int indexSpecialAsteroid;
    private int whatBalise;
    int countNoEvent = 0;

    GameManager gameManager;
    WorldLevelManager worldLevelManager;
    ObjectPooler objectPooler;

    void Start()
    {
        objectPooler = ObjectPooler.instance;
        worldLevelManager = WorldLevelManager.instance;
        gameManager = GameManager.Instance;
        nextActionTime = timeToRespawn + gameManager.timeForTuto;
        saveDelay = Delay;
        
    }
    void Update()
    {
        if (timeToRespawn <=0 )
        {
           timeToRespawn = 0.3f;
           reduceTimeToRespawn = 0.01f;
        }

        if (Time.time >= nextActionTime)
        {
            nextActionTime += timeToRespawn;
            if (Delay <= 0)
            {
                worldLevelManager.updateWorldLevel();
                timeToRespawn *= reduceTimeToRespawn;
                saveDelay = (int)Mathf.Round((float)saveDelay * multiplicatorDelay);
                Delay = saveDelay;
                gameManager.worldLevel++;
                if (chanceToSpawnSpecialAsteroid >= 0.02f)
                {
                    chanceToSpawnSpecialAsteroid *= 0.9f;
                }
                if (Random.Range(0f, 1f) <= eventManager.chanceEvent && eventManager.isEvent == false && !eventManager.isJustEvent)
                {
                    eventManager.isEvent = true;
                    eventManager.StartEvent();
                } else
                {
                    countNoEvent++;
                    if (countNoEvent >= 2)
                    {
                        eventManager.isJustEvent = false;
                    }
                }
            } else
            {
                 Delay--;
            }
            if (!eventManager.isEvent)
            {
                
                float whatAsteroid = gameManager.worldLevel / 4;
                if (whatAsteroid >= 4)
                {
                    whatAsteroid = 4;
                }
                indexAsteroid = Random.Range(0, 6 + ((int)whatAsteroid));
                currentAsteroid = asteroids[indexAsteroid];
                randomSpawnPosX = Random.Range(posBalises[0].transform.position.x, posBalises[1].transform.position.x);
                objectPooler.SpawnFromPool(currentAsteroid.tag, new Vector3(randomSpawnPosX, posBalises[0].transform.position.y, posBalises[0].transform.position.z), transform.rotation);
                if (Random.Range(0f, 1f) <= chanceToSpawnSpecialAsteroid)
                {
                    indexSpecialAsteroid = Random.Range(0, specialsAsteroids.Length);
                    if (
                        specialsAsteroids[indexSpecialAsteroid].tag == "BigGreenAsteroid" && !gameManager.isMaxTireRate ||
                        specialsAsteroids[indexSpecialAsteroid].tag == "LittleGreenAsteroid" && !gameManager.isMaxTireRate ||
                        specialsAsteroids[indexSpecialAsteroid].tag == "BigRedAsteroid" && !gameManager.isMaxDamage ||
                        specialsAsteroids[indexSpecialAsteroid].tag == "LittleRedAsteroid" && !gameManager.isMaxDamage
                        )
                    {
                        whatBalise = Random.Range(0, 1);
                        currentSpecialAsteroid = specialsAsteroids[indexSpecialAsteroid];
                        objectPooler.SpawnFromPool(currentSpecialAsteroid.tag, new Vector3(posSpecialBalises[whatBalise].transform.position.x, posSpecialBalises[whatBalise].transform.position.y, posSpecialBalises[whatBalise].transform.position.z), transform.rotation);
                    }
                }
            }
        }
        
    }


}
