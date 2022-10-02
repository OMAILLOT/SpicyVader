using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeItem : MonoBehaviour
{
    public new AudioClip audio;
    public TextMeshPro LifeText;
    GameManager gameManager = GameManager.Instance;


    void FixedUpdate()
    {
        if (gameManager.isMaxLife)
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
            if (gameManager.PlayerLife < 5)
            {
                gameManager.PlayerLife += 1;
            } 
            Instantiate(LifeText);
            AudioManager.Instance.PlayClipAt(audio, transform.position);
            Destroy(gameObject);
        }
    }
}
