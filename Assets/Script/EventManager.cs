using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private GameManager gameManager;
    public bool isEvent;
    public bool isEventBoss;
    public float chanceEvent;
    [Space(5)]
    public List<GameEvent> events;
    public bool isJustEvent = false;
    public GameObject[] items;
    public GameObject eventBoss;
    private int indexEvent;
    public bool isItemTaken = false;
    public bool chooseItems = false;
    public static EventManager instance;
    [SerializeField] private List<GameObject> ItemsPlaceHolders;

    private void Start()
    {
        instance = this;
        gameManager = GameManager.Instance;
    }

    public void StartEvent()
    {
        if (!isJustEvent)
        {
            isItemTaken = false;
            indexEvent = Random.Range(0, events.Count);
            events[indexEvent].ActiveEvent();
        }
    }

    public void winEvent()
    {
        chooseItems = true;
        for (int i = 0; i < 3; i++)
        {
            int index = Random.Range(0, items.Length);
            items[index].transform.position = ItemsPlaceHolders[i].transform.position;
            Instantiate(items[index]);
        }
    }
}
