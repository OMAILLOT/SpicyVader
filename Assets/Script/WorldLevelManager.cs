using BaseTemplate.Behaviours;
using UnityEngine;

public class WorldLevelManager : MonoSingleton<WorldLevelManager>
{
    public int worldLevel = 1;

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
