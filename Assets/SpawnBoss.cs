using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public GameObject[] allBoss;
    public GameObject SpawnBaliseBoss;
    void Start()
    {
        int index = Random.Range(0, allBoss.Length);
        allBoss[index].transform.position = SpawnBaliseBoss.transform.position;
        Instantiate(allBoss[index]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
