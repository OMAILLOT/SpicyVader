using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBasicShip : MonoBehaviour
{
    [SerializeField]
    public int[] lineTake = new int[16]; 
    public GameObject[] balisesSpawn;
    public GameObject basicShip;
    WorldLevelManager worldLevelManager;
    public static SpawnBasicShip instance;
    int howManyShip;

    private void Awake()
    {
        instance = this;
        worldLevelManager = WorldLevelManager.instance;
    }
    void Start()
    {
        balisesSpawn = GameObject.FindGameObjectsWithTag("BaliseBasicShip");
        lineTake = new int[16];
        for (int i = 0; i < lineTake.Length; i++)
        {
            lineTake[i] = 0;
        }
        Debug.Log("Start Basic Ship Event");
        StartCoroutine(SpawnShip());
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
