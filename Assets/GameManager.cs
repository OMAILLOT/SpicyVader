using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public int worldLevel = 1;
    [Space(5)]
    public bool isRedPlayer = true;
    public bool isGreenPlayer = false;
    [Space(5)]
    public int countAsteroidDestroy;
    public int countRedHotChillyPeperEat;
    public int countGreenChillyPeperEat;
    public float howManyRedCountToLevelUp;
    public float howManyGreenCountToLevelUp;
    public float score;
    [Space(10)]
    public int PlayerLife = 3;
    public float PlayerDamage = 20;
    public float RateShoot = 1.5f;
    public float speedBullet = 0.1f;
    public int PlayerLevel = 1;
    [Space(5)]
    public bool isMaxTireRate = false;
    public bool isMaxDamage = false;
    public bool isMaxLife = false;
    [Space(10)]
    public float speedChillyPeper = 0.1f;
    public float boostRedChillyPeper = 3.0f;
    public float baseBoostRedChillyPeper;
    public float boostGreenChillyPeper = 0.15f;
    [Space(10)]
    public float spawnRate;
    public float isCanSpawn;
    [Space(5)]
    public bool isGameOver;
    [Space(5)]
    public ParticleSystem[] spaceParticles;
    [Space(10)]
    public string nameOfScene;
    public Animation fadeSystem;
    public float timeToWait;
    [Space(10)]
    public float timeForTuto= 15f;
    [Space(10)]
    public AudioClip playerDeath;
    [Space(10)]


    Inventory inventory;
    private void Awake()
    {
        timeForTuto += Time.time;
        Screen.orientation = ScreenOrientation.Portrait;
    }

    private void Start()
    {
        baseBoostRedChillyPeper = boostRedChillyPeper;

        inventory = Inventory.Instance;
    }

    private void Update()
    {
        inventory.UpdateLife();
        inventory.updateScore();
    }
    public IEnumerator loadNextScene()
    {
        AudioManager.Instance.PlayClipAt(playerDeath, transform.position);
        fadeSystem.Play();
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene(nameOfScene);
    }

}
