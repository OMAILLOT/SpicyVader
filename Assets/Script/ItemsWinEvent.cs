using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemsWinEvent : MonoBehaviour
{
    [Header("Chilly Peper")]
    public new AudioClip audio;
    public TextMeshPro damageText;
    public TextMeshPro rateOfFire;
    [Space(10)]
    public new AudioClip lifeAudio;
    public TextMeshPro LifeText;
    EventManager eventManager;


    GameManager gameManager = GameManager.Instance;
    bool stopFalling = false;
    private void Start()
    {
        eventManager = GameObject.FindObjectOfType(typeof(EventManager)) as EventManager;
    }
    void Update()
    {
        if (eventManager.isItemTaken)
        {
            eventManager.isJustEvent = true;
            eventManager.isEvent = false;
            Destroy(gameObject);
        }
        if (transform.position.y >= 2f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - GameManager.Instance.speedChillyPeper * Time.deltaTime, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch(gameObject.tag)
            {
                case "RedHotChillyPeper" :
                    Instantiate(damageText);
                    Inventory.instance.UpdateDamage();
                    AudioManager.instance.PlayClipAt(audio, transform.position);
                    gameManager.countRedHotChillyPeperEat++;
                    break;
                case "GreenChillyPeper" :
                    if (gameManager.isMaxTireRate)
                    {
                        gameObject.SetActive(false);
                    } else
                    {
                        Instantiate(rateOfFire);
                        AudioManager.instance.PlayClipAt(audio, transform.position);
                        Inventory.instance.UpdateTireRate();
                        gameManager.countGreenChillyPeperEat++;
                    }
                        break;
                case "LifeItem" :
                    if (collision.CompareTag("Player"))
                    {
                        if (gameManager.PlayerLife < 5)
                        {
                            gameManager.PlayerLife += 1;
                        }
                        Instantiate(LifeText);
                        AudioManager.instance.PlayClipAt(lifeAudio, transform.position);
                        
                    }
                    break;
            }
            eventManager.isItemTaken = true;
            eventManager.chooseItems = false;
            Destroy(gameObject);
        }
    }
}
