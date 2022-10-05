using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemsWinEvent : MonoBehaviour
{
    [Header("Chilly Peper")]
    public AudioClip audio;
    public TextMeshPro damageText;
    public TextMeshPro rateOfFire;
    [Space(10)]
    public AudioClip lifeAudio;
    public TextMeshPro LifeText;
    EventManager eventManager;

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
        if (transform.position.y >= 2.5f)
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
                    Inventory.Instance.UpdateDamage();
                    AudioManager.Instance.PlayClipAt(audio, transform.position);
                    GameManager.Instance.countRedHotChillyPeperEat++;
                    break;
                case "GreenChillyPeper" :
                    if (GameManager.Instance.isMaxTireRate)
                    {
                        gameObject.SetActive(false);
                    } else
                    {
                        Instantiate(rateOfFire);
                        AudioManager.Instance.PlayClipAt(audio, transform.position);
                        Inventory.Instance.UpdateTireRate();
                        GameManager.Instance.countGreenChillyPeperEat++;
                    }
                        break;
                case "LifeItem" :
                    if (collision.CompareTag("Player"))
                    {
                        if (PlayerManager.Instance.PlayerLife < 5)
                        {
                            PlayerManager.Instance.PlayerLife += 1;
                        }
                        Instantiate(LifeText);
                        AudioManager.Instance.PlayClipAt(lifeAudio, transform.position);
                        
                    }
                    break;
            }
            eventManager.isItemTaken = true;
            eventManager.chooseItems = false;
            Destroy(gameObject);
        }
    }
}
