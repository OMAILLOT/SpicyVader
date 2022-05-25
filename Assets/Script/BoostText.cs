using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostText : MonoBehaviour
{

    private float currentTime;
    private GameObject player;
    public float speed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentTime = Time.time;
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.001f, player.transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time >= currentTime + 1.2f)
        {
            Destroy(gameObject);
        } else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z); 
        }


    }
}
