using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public int PlayerLife = 3;
    public float PlayerDamage = 20;
    public float RateShoot = 1.5f;
    public float speedBullet = 0.1f;
    public int PlayerLevel = 1;
    [Space(10)]
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
        switch (PlayerLevel)
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
        PlayerLevel++;
        PlayerDamage = 20 + PlayerLevel - 1;
        level.text = PlayerLevel.ToString();
        Inventory.Instance.UpdateDamageWithoutIncrease();
        Instantiate(levelUpText);
        playerLevelUpParticle.Play();
    }

    public void greenLevelUp()
    {
        AudioManager.Instance.PlayClipAt(levelUpSong, transform.position);
        RateShoot = 0.3f - PlayerLevel * 0.05f;
        if (GameManager.Instance.isRedPlayer)
        {
            PlayerDamage = 9;
        }
        else
        {
            PlayerDamage -= 2;
            if (PlayerDamage <= 3)
            {
                PlayerDamage = 3;
            }
        }
        sprite.color = new Color(152, 255, 105);
        GameManager.Instance.isGreenPlayer = true;
        GameManager.Instance.isRedPlayer = false;
        GameManager.Instance.boostRedChillyPeper = 1.5f;
        PlayerLevel++;
        level.text = PlayerLevel.ToString();
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
            PlayerLife -= 1;

            if (PlayerLife > 0)
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
