using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public GameObject[] allBoss;
    public GameObject SpawnBaliseBoss;
    public ParticleSystem lightSpeedParticle;
    public ParticleSystem spaceParticle;
    public GameObject[] Planets;
    int index;

    private void OnEnable()
    {
        index = Random.Range(0, allBoss.Length);

        StartCoroutine(WaitForSpawnBoss());
    }

    float durationTimePlanetFall = 9;
    private void Update()
    {
        if (Time.time <= durationTimePlanetFall)
        {
            Planets[index].transform.position = new Vector3(Planets[index].transform.position.x, Planets[index].transform.position.y - 0.01f, Planets[index].transform.position.z); 
        }
    }

    IEnumerator WaitForSpawnBoss()
    {
        lightSpeedParticle.gameObject.SetActive(true);
        lightSpeedParticle.Play();
        yield return new WaitForSeconds(9);
        allBoss[index].transform.position = SpawnBaliseBoss.transform.position;
        Instantiate(allBoss[index]);
    }
}
