using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RedHotChillyPeper : MonoBehaviour
{
    public new AudioClip audio;
    public TextMeshPro damageText;
    GameManager gameManager = GameManager.Instance;


    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - GameManager.Instance.speedChillyPeper * Time.deltaTime, 0);
        if (transform.position.y <= -10)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(damageText);
            Inventory.instance.UpdateDamage();
            AudioManager.instance.PlayClipAt(audio, transform.position);
            gameManager.countRedHotChillyPeperEat++;
            Destroy(gameObject);
        }
    }



}
