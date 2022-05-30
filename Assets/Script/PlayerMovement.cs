using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Could also already reference this in the Inspector if possible
    [SerializeField] private Camera _camera;



    public SpriteRenderer graphics;
    public float invicibilityFlashDelay = 0.2f;
    public float invicibilityDelay = 3f;
    protected bool isInvincible = false;
    [Space(10)]
    public static PlayerMovement instance;
    [Space(10)]
    public AudioClip playerHit;
    public AudioClip levelUpSong;
    private Vector3 goToPostion = new Vector3(0,0,0);
    [Space(10)]
    public ParticleSystem[] playerParticles;
    [Space(10)]
    public TextMeshProUGUI level;
    public TextMeshPro levelUpText;
    [Space(10)]

    GameManager gameManager;
    private void Awake()
    {
        instance = this;
        
        // It is better to cache the camera reference since Camera.main is quite expensive
        if (!_camera) _camera = Camera.main;   
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    private void FixedUpdate()
    {

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            var touch_Pos = _camera.ScreenToWorldPoint(touch.position);
            goToPostion = new Vector3(touch_Pos.x,touch_Pos.y,0);
            Vector3.Lerp(transform.position, goToPostion,100f);
            transform.position = Vector3.MoveTowards(transform.position, goToPostion, gameManager.playerSpeed * Time.deltaTime);
        }
    }

    public void levelUp()
    {
        AudioManager.instance.PlayClipAt(levelUpSong, transform.position);
        switch(gameManager.PlayerLevel)
        {
            case 1:                
                gameManager.boostRedChillyPeper = gameManager.baseBoostRedChillyPeper / 2;
                break;
            case 2:
                gameManager.boostRedChillyPeper = gameManager.baseBoostRedChillyPeper / 3;
                break;
            case 3:
                gameManager.boostRedChillyPeper = gameManager.baseBoostRedChillyPeper / 4;
                break;
            case 4:
                gameManager.boostRedChillyPeper = gameManager.baseBoostRedChillyPeper / 5;
                break;
        }
        gameManager.PlayerLevel++;
        gameManager.PlayerDamage = 20 + gameManager.PlayerLevel - 1;
        level.text = gameManager.PlayerLevel.ToString();
        Inventory.instance.UpdateDamageWithoutIncrease();
        Instantiate(levelUpText);
        Instantiate(playerParticles[2], transform.position, Quaternion.identity);
    }

    public void greenLevelUp()
    {
        AudioManager.instance.PlayClipAt(levelUpSong, transform.position);
        gameManager.RateShoot = 0.3f - gameManager.PlayerLevel * 0.05f;
        if (gameManager.isRedPlayer)
        {
            gameManager.PlayerDamage = 9;
        } else
        {
            gameManager.PlayerDamage -= 2;
            if (gameManager.PlayerDamage <= 3)
            {
                gameManager.PlayerDamage = 3;
            }
        }
        gameManager.isGreenPlayer = true;
        gameManager.isRedPlayer = false;
        gameManager.boostRedChillyPeper = 1.5f;
        gameManager.PlayerLevel++;
        level.text = gameManager.PlayerLevel.ToString();
        Inventory.instance.UpdateTireRateWithoutIncrease();
        Inventory.instance.UpdateDamageWithoutIncrease();
        Instantiate(levelUpText);
        Instantiate(playerParticles[2], transform.position, Quaternion.identity);
    }


    public void PlayerHit()
    {
        if (!isInvincible)
        {   
            playerParticles[0].transform.position = transform.position;
            Instantiate(playerParticles[0]);
            gameManager.PlayerLife -= 1;

            if (gameManager.PlayerLife > 0)
            {
                isInvincible = true;
                StartCoroutine(InvicibilityFlash());
                StartCoroutine(IncibilityDuration());
            }
            else
            {
                PlayerDeath();
                return;
            }
        }
    }

    public IEnumerator InvicibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0.2f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invicibilityFlashDelay);
        }
    }

    public IEnumerator IncibilityDuration()
    {
        AudioManager.instance.PlayClipAt(playerHit, transform.position);
        yield return new WaitForSeconds(invicibilityDelay);
        isInvincible = false;
    }



    private bool justOneTime = false;
    private void PlayerDeath()
    {
        if (!justOneTime)
        {
            Instantiate(playerParticles[1], transform.position, Quaternion.identity);
            //PlayerMovement.instance.enabled = false;
            if (gameManager.score > PlayerPrefs.GetFloat("score"))
            {
                PlayerPrefs.SetFloat("score", gameManager.score);
            }
            justOneTime = true;
            StartCoroutine(gameManager.loadNextScene());
            transform.position = new Vector3(transform.position.x + 100, transform.position.y, transform.position.z);
        }
    }


}
