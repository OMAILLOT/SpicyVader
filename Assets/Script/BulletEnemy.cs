using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - GameManager.Instance.speedBullet * Time.deltaTime, 0);
        if (transform.position.y < -20)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            PlayerMovement.instance.PlayerHit();
            Destroy(gameObject);
        }
    }
}
