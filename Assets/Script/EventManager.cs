using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private GameManager gameManager;
    public bool isEvent;
    public float chanceEvent;
    [Space(5)]
    public GameObject[] events;
    public bool isJustEvent = false;
    public GameObject[] items;
    public GameObject eventBoss;
    private int indexEvent;
    public bool isItemTaken = false;
    public bool chooseItems = false;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void StartEvent()
    {
        if (gameManager.worldLevel == 10 || gameManager.worldLevel >= 10 && gameManager.worldLevel % 5 == 0)
        {
            Instantiate(eventBoss);
        } else
        {
            if (!isJustEvent)
            {
                isItemTaken = false;
                indexEvent = Random.Range(0, events.Length);
                Instantiate(events[indexEvent]);
            }
        }
        
    }

    public void winEvent()
    {
        chooseItems = true;
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, items.Length);
            items[index].transform.position = new Vector3(-12 + i*1.5f, 8f, 0f);
            Instantiate(items[index]);
        }
    }
}
