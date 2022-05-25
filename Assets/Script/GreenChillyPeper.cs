using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GreenChillyPeper : MonoBehaviour
{
    GameManager gameManager = GameManager.Instance;
    public new AudioClip audio;
    public TextMeshPro rateOfFire;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameManager.isMaxTireRate)
        {
            gameObject.SetActive(false);
        } else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - GameManager.Instance.speedChillyPeper * Time.deltaTime, 0);
        
            if (transform.position.y <= -10)
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(rateOfFire);
            AudioManager.instance.PlayClipAt(audio, transform.position);
            Inventory.instance.UpdateTireRate();
            gameManager.countGreenChillyPeperEat++;
            Destroy(gameObject);
        }
    }
}
