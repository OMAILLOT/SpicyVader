using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShip : MonoBehaviour
{
    public float PV = 50;
    public float speed = 3;
    public float dropChance = 0.3f;
    public float dropLifeChance = 0.05f;
    [Space(5)]
    //public GameObject mecanicPiece;
    public GameObject playerLife;
    public GameObject[] grid;

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
    void Update()
    {
        if (!stopMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, grid[whatCell].transform.position, Time.deltaTime * speed);
            if (transform.position == grid[whatCell].transform.position)
            {
                stopMove = true;
                Shoot();
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

    void Shoot()
    {
        Debug.Log("je shoot");
    }
}
