using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    private float nextActionTime = 0.0f;
    [Space(10)]
    public static PlayerMovement instance;
    [Space(10)]
    public AudioClip[] audios;
    private int musicIndex;
    [Space(10)]
    [Space(10)]
    public string tagBullet = "bullet";

    ObjectPooler objectPooler;

    GameManager gameManager;
    public PlayerMovement playerMovement;

    private void Start()
    {
        gameManager = GameManager.Instance;
        objectPooler = ObjectPooler.instance;
    }

    private void FixedUpdate()
    {
        if (Time.time > nextActionTime)
        {
            StartCoroutine(SpawnBullet());
        }
    }


    IEnumerator SpawnBullet()
    {
        nextActionTime += gameManager.RateShoot;
        musicIndex = Random.Range(0, audios.Length);
        AudioManager.Instance.PlayClipAt(audios[musicIndex], transform.position);
        spawnBullet(gameManager.PlayerLevel);
        switch (gameManager.PlayerLevel)
        {
            case 1:
                if (gameManager.countRedHotChillyPeperEat >= gameManager.howManyRedCountToLevelUp)
                {
                    gameManager.howManyRedCountToLevelUp *= 2f;
                    PlayerManager.Instance.levelUp();
                }
                else if (gameManager.countGreenChillyPeperEat >= gameManager.howManyGreenCountToLevelUp)
                {
                    gameManager.howManyGreenCountToLevelUp *= 2f;
                    PlayerManager.Instance.greenLevelUp();
                    tagBullet = "TinyBulletPlayer";
                }
                break;
            case 2:
                if (gameManager.countRedHotChillyPeperEat >= gameManager.howManyRedCountToLevelUp && gameManager.isRedPlayer)
                {
                    gameManager.howManyRedCountToLevelUp *= 1.5f;
                    PlayerManager.Instance.levelUp();
                }
                else if (gameManager.countGreenChillyPeperEat >= gameManager.howManyGreenCountToLevelUp && gameManager.isGreenPlayer)
                {
                    gameManager.howManyGreenCountToLevelUp *= 1.5f;
                    PlayerManager.Instance.greenLevelUp();
                }
                break;
            case 3:
                if (gameManager.countRedHotChillyPeperEat >= gameManager.howManyRedCountToLevelUp && gameManager.isRedPlayer)
                {
                    gameManager.howManyRedCountToLevelUp *= 1.33f;
                    PlayerManager.Instance.levelUp();
                } else if (gameManager.countGreenChillyPeperEat >= gameManager.howManyGreenCountToLevelUp && gameManager.isGreenPlayer)
                {
                    gameManager.howManyGreenCountToLevelUp *= 1.33f;
                    PlayerManager.Instance.greenLevelUp();
                }
                break;
            case 4:
                if (gameManager.countRedHotChillyPeperEat >= gameManager.howManyRedCountToLevelUp && gameManager.isRedPlayer)
                {
                    PlayerManager.Instance.levelUp();
                }
                else if (gameManager.countGreenChillyPeperEat >= gameManager.howManyGreenCountToLevelUp && gameManager.isGreenPlayer)
                {
                    PlayerManager.Instance.greenLevelUp();
                }
                break;
        }
        yield return null;
    }
    private void spawnBullet(int level)
    {

        switch (level)
        {
            case 1:
                objectPooler.SpawnFromPool(tagBullet, transform.position, Quaternion.identity);
                break;
            case 2:
                objectPooler.SpawnFromPool(tagBullet, new Vector3(transform.position.x - 0.3f, transform.position.y, transform.position.z), Quaternion.identity);
                objectPooler.SpawnFromPool(tagBullet, new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z), Quaternion.identity);
                break;                     
            case 3:                        
                objectPooler.SpawnFromPool(tagBullet, new Vector3(transform.position.x - 0.3f, transform.position.y, transform.position.z), Quaternion.identity);
                objectPooler.SpawnFromPool(tagBullet, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
                objectPooler.SpawnFromPool(tagBullet, new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z), Quaternion.identity);
                break;                     
            case 4:                        
                objectPooler.SpawnFromPool(tagBullet, new Vector3(transform.position.x - 0.3f, transform.position.y, transform.position.z), Quaternion.identity);
                objectPooler.SpawnFromPool(tagBullet, new Vector3(transform.position.x - 0.15f, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
                objectPooler.SpawnFromPool(tagBullet, new Vector3(transform.position.x + 0.15f, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
                objectPooler.SpawnFromPool(tagBullet, new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z), Quaternion.identity);
                break;                     
            case 5:                        
                objectPooler.SpawnFromPool(tagBullet, new Vector3(transform.position.x - 0.3f, transform.position.y, transform.position.z), Quaternion.identity);
                objectPooler.SpawnFromPool(tagBullet, new Vector3(transform.position.x - 0.15f, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
                objectPooler.SpawnFromPool(tagBullet, new Vector3(transform.position.x, transform.position.y + 0.35f, transform.position.z), Quaternion.identity);
                objectPooler.SpawnFromPool(tagBullet, new Vector3(transform.position.x + 0.15f, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
                objectPooler.SpawnFromPool(tagBullet, new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z), Quaternion.identity);
                break;
        }
        
    }
}
