using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitPlayerParticles : MonoBehaviour
{
    void Update()
    {
        transform.position = PlayerMovement.Instance.transform.position;
    }
}
