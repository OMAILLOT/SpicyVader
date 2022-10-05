using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
    public class Line
    {
        public List<Transform> cellPlaceHolder;
    }

    [System.Serializable]
    public class Rewards
    {
        public int numberOfRewards;
        public List<GameObject> rewardObjects;
    }

public class BasicShipEvent : GameEvent
{
    //[SerializeField] public List<List<Transform>> placeHolderLines;
    [HideInInspector] public List<Line> placeHolderLines;

    [SerializeField] private List<Line> fixPlaceHolderLines;
    [SerializeField] private GameObject basicShip;
    [SerializeField] private float timeEvent;
    [SerializeField] private List<Transform> spawnPlaceHolders;
    [SerializeField] private Dictionary<int, List<GameObject>> Rewards;
    [SerializeField] private Rewards rewards;
    [SerializeField] private List<Transform> rewardPlaceHolders;

    List<GameObject> allBasicShip;
    int destroyBasicShipCount;

    public static new BasicShipEvent Instance;
    public void Awake()
    {
        if (Instance != null)
        {
            print("there is an other instance of this object");
        }
        Instance = this;
        allBasicShip = new List<GameObject>();
    }

    public override void ActiveEvent()
    {
        placeHolderLines = fixPlaceHolderLines;
        timeEvent += Time.time;

        StartCoroutine(SpawnShip());
    }


    public IEnumerator SpawnShip()
    {
        yield return new WaitForSeconds(2f);

        int eventLevel = (int)WorldLevelManager.Instance.worldLevel / 2;
        if (eventLevel > fixPlaceHolderLines.Count) eventLevel = fixPlaceHolderLines.Count;
        for (int lineIndex = 0; lineIndex < eventLevel; lineIndex++)
        {
            int cellPlaceHolderCount = fixPlaceHolderLines[lineIndex].cellPlaceHolder.Count;
            for (int placeHolderIndex = 0; placeHolderIndex <= cellPlaceHolderCount; placeHolderIndex++)
            {
                allBasicShip.Add(ObjectPooler.instance.SpawnFromPool("BasicShip", spawnPlaceHolders[Random.Range(0, spawnPlaceHolders.Count)].position, Quaternion.identity));

                yield return new WaitForSeconds(0.2f);
            }
        }
        
        yield return new WaitUntil(() => Time.time <= timeEvent);

        foreach (GameObject basicShip in allBasicShip)
        {
            StartCoroutine(RunAway(basicShip));
        }
    }

    public void IncrementBasicShipDestroyCount()
    {
        destroyBasicShipCount++;
        if (destroyBasicShipCount == allBasicShip.Count)
        {
            winEvent();
        }
    }

    public override void winEvent()
    {
        base.winEvent();
        for (int i = 0; i < rewards.numberOfRewards; i++) {
            Instantiate(rewards.rewardObjects[Random.Range(0, rewards.rewardObjects.Count-1)],rewardPlaceHolders[i].position,Quaternion.identity);
        }

    }


    IEnumerator RunAway(GameObject basicShip)
    {
        while(basicShip.transform.position.y <= 6f)
        {
            Vector2.MoveTowards(basicShip.transform.position, Vector2.up, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        } 
        basicShip.SetActive(false);
    }
}
