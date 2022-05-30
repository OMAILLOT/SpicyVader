using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBasicShip : MonoBehaviour
{
    EventManager eventManager;
    [SerializeField]
    public int[] lineTake = new int[16]; 
    public GameObject[] balisesSpawn;
    public GameObject basicShip;
    WorldLevelManager worldLevelManager;
    public static SpawnBasicShip instance;
    public float timeEvent;
    int howManyShip;
    public int countShipDestroy = 0;

    private void Awake()
    {
        instance = this;
        worldLevelManager = WorldLevelManager.instance;
    }
    void Start()
    {
        //Debug.Log("Time : " + Time.time + "\nTimeEvent : " + timeEvent + "\nTotalTime : " + Time.time+timeEvent);
        timeEvent += Time.time;
        eventManager = GameObject.FindObjectOfType(typeof(EventManager)) as EventManager;
        balisesSpawn = GameObject.FindGameObjectsWithTag("BaliseBasicShip");
        lineTake = new int[16];
        for (int i = 0; i < lineTake.Length; i++)
        {
            lineTake[i] = 0;
        }
        Debug.Log("Start Basic Ship Event");
        StartCoroutine(SpawnShip());
    }

    void FixedUpdate()
    {
        if (countShipDestroy >= howManyShip && howManyShip != 0)
        {
            eventManager.winEvent();
            Destroy(gameObject);

        } else if (Time.time >= timeEvent)
        {
            GameObject[] allShip = GameObject.FindGameObjectsWithTag("BasicShip");
            foreach(GameObject ship in allShip)
            {
                runAway(ship);
            }
            if (allShip.Length == 0)
            {
                eventManager.isJustEvent = true;
                eventManager.isEvent = false;
                Destroy(gameObject);
            }
            
        }  
    }

    void runAway(GameObject basicShip)
    {
        basicShip.transform.position = new Vector3(basicShip.transform.position.x, basicShip.transform.position.y + 0.01f, basicShip.transform.position.z);
    }


    public IEnumerator SpawnShip()
    {
        yield return new WaitForSeconds(3f);
        switch (worldLevelManager.basicShipEventlevel)
        {
            case 1:
                howManyShip = 4;
                break;
            case 2:
                howManyShip = 8;
                break;
            case 3:
                howManyShip = 12;
                break;
            case 4:
                howManyShip = 16;
                break;
        }
        for (int i = 0; i < howManyShip; i++)
        {
            Instantiate(basicShip, balisesSpawn[Random.Range(0, balisesSpawn.Length)].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
