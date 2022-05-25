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
    private int indexEvent;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void StartEvent()
    {

        indexEvent = Random.Range(0, events.Length);
        Instantiate(events[indexEvent]);
        
    }
}
