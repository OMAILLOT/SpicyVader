using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    void Start()
    {
        WaitInStart();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y+ PlayerManager.Instance.speedBullet * Time.deltaTime,0);
        if (transform.position.y > 20)
            gameObject.SetActive(false);
    }


    IEnumerator WaitInStart()
    {
        var wait = new WaitForSeconds(0.5f); 
        while (true)
        {
            yield return wait; // Reuse
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag != "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
