using UnityEngine;

public class WorldLevelManager : MonoBehaviour
{

    public float asteroidsPV = 0;
    public float asteroidsSpeed = 0;
    public float asteroidsDropChance = 0f;
    public float asteroidsLifeChance = 0f;
    [Space(5)]
    public float basicShipPV = 0;
    public float basicShipSpeed = 0;
    public float basicShipDropChance = 0f;
    public float basicShipLifeChance = 0f;
    [Space(20)]
    public float increaseAsteroidsSpeed = 0.2f;
    public float increaseAsteroidsPv = 4;
    [Space(5)]
    public float increaseBasicShipSpeed = 0.2f;
    public float increaseBasicShipPv = 2;
    public static WorldLevelManager instance;
    [Space(10)]
    public int worldLevelForLevel2 = 5;
    public int worldLevelForLevel3 = 10;
    public int worldLevelForLevel4 = 15;
    public int basicShipEventlevel = 1;

    GameManager gameManager;
    private void Awake()
    {
        instance = this;
        if (instance == null)
        {
            Debug.Log("Il n'y a pas d'instance World Level Manager");

        }
    }
    void Start()
    {
        gameManager = GameManager.Instance;
        switch (basicShipEventlevel)
        {
            case 1:
                if (gameManager.worldLevel >= worldLevelForLevel2)
                {
                    basicShipEventlevel = 2;
                }
                break;
            case 2:
                if (gameManager.worldLevel >= worldLevelForLevel3)
                {
                    basicShipEventlevel = 3;
                }
                break;
            case 3:
                if (gameManager.worldLevel >= worldLevelForLevel4)
                {
                    basicShipEventlevel = 4;
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateWorldLevel()
    {
        asteroidsPV += increaseAsteroidsPv;
        asteroidsSpeed += increaseAsteroidsSpeed;

        basicShipPV += increaseBasicShipPv;
        basicShipSpeed += increaseBasicShipSpeed;
    }
}
