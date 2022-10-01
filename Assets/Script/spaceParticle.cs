using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceParticle : MonoBehaviour
{
    bool stop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EventManager.instance.isEventBoss && !stop)
        {
            stop = true;
            StartCoroutine(activetrails());
        }
    }

    IEnumerator activetrails()
    {
        Debug.Log("ActiveTrail");
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var trails = ps.trails;
        trails.lifetime = 0.1f;
        yield return new WaitForSeconds(20f);
        trails.lifetime = 0;
    }
}
