using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAsteroid : MonoBehaviour
{
    // Start is called before the first frame update

    public new ParticleSystem particleSystem;
    void Start()
    {
        ActiveParticle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ActiveParticle()
    {
        particleSystem.Play();
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
}   
