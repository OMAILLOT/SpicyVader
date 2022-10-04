using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{

    [SerializeField] private SpriteRenderer graphics;
    [SerializeField] private float invicibilityFlashDelay = 0.2f;
    [SerializeField] private float invicibilityDelay = 3f;
    [SerializeField] private bool isInvincible = false;
    [Space(10)]
    [SerializeField] private AudioClip playerHit;
    [SerializeField] private AudioClip levelUpSong;
    
    [Space(10)]
    [SerializeField] private ParticleSystem playerHitParticle;
    [SerializeField] private ParticleSystem playerDieParticle;
    [SerializeField] private ParticleSystem playerLevelUpParticle;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshPro levelUpText;
    [Space(10)]
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private GameObject fadeHit;
    public void levelUp()
    {
        AudioManager.Instance.PlayClipAt(levelUpSong, transform.position);
        switch (GameManager.Instance.PlayerLevel)
        {
            case 1:
                GameManager.Instance.boostRedChillyPeper = GameManager.Instance.baseBoostRedChillyPeper / 2;
                break;
            case 2:
                GameManager.Instance.boostRedChillyPeper = GameManager.Instance.baseBoostRedChillyPeper / 3;
                break;
            case 3:
                GameManager.Instance.boostRedChillyPeper = GameManager.Instance.baseBoostRedChillyPeper / 4;
                break;
            case 4:
                GameManager.Instance.boostRedChillyPeper = GameManager.Instance.baseBoostRedChillyPeper / 5;
                break;
        }
        GameManager.Instance.PlayerLevel++;
        GameManager.Instance.PlayerDamage = 20 + GameManager.Instance.PlayerLevel - 1;
        level.text = GameManager.Instance.PlayerLevel.ToString();
        Inventory.Instance.UpdateDamageWithoutIncrease();
        Instantiate(levelUpText);
        playerLevelUpParticle.Play();
    }

    public void greenLevelUp()
    {
        AudioManager.Instance.PlayClipAt(levelUpSong, transform.position);
        GameManager.Instance.RateShoot = 0.3f - GameManager.Instance.PlayerLevel * 0.05f;
        if (GameManager.Instance.isRedPlayer)
        {
            GameManager.Instance.PlayerDamage = 9;
        }
        else
        {
            GameManager.Instance.PlayerDamage -= 2;
            if (GameManager.Instance.PlayerDamage <= 3)
            {
                GameManager.Instance.PlayerDamage = 3;
            }
        }
        sprite.color = new Color(152, 255, 105);
        GameManager.Instance.isGreenPlayer = true;
        GameManager.Instance.isRedPlayer = false;
        GameManager.Instance.boostRedChillyPeper = 1.5f;
        GameManager.Instance.PlayerLevel++;
        level.text = GameManager.Instance.PlayerLevel.ToString();
        Inventory.Instance.UpdateTireRateWithoutIncrease();
        Inventory.Instance.UpdateDamageWithoutIncrease();
        Instantiate(levelUpText);
        playerLevelUpParticle.Play();
    }


    public void PlayerHit()
    {
        if (!isInvincible)
        {

            fadeHit.SetActive(true);
            playerHitParticle.Play();
            GameManager.Instance.PlayerLife -= 1;

            if (GameManager.Instance.PlayerLife > 0)
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
        AudioManager.Instance.PlayClipAt(playerHit, transform.position);
        yield return new WaitForSeconds(invicibilityDelay);
        isInvincible = false;
    }



    private bool justOneTime = false;
    private void PlayerDeath()
    {
        if (!justOneTime)
        {
            playerDieParticle.Play();
            if (GameManager.Instance.score > PlayerPrefs.GetFloat("score"))
            {
                PlayerPrefs.SetFloat("score", GameManager.Instance.score);
            }
            justOneTime = true;
            StartCoroutine(GameManager.Instance.loadNextScene());
            PlayerMovement.Instance.renderer.enabled = false;
        }
    }

}
