using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceParticle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EventManager.instance.isEventBoss)
        {
            StartCoroutine(activetrails());
        }
    }

    IEnumerator activetrails()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var trails = ps.trails;
        trails.lifetime = 1;
        trails.ratio = 0.5f;
        yield return new WaitForSeconds(5f);
        trails.lifetime = 0;
    }
}
