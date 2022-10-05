using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoSingleton<Inventory>
{
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI BestScoreText;

    [SerializeField] private TextMeshProUGUI DamageText;
    [SerializeField] private TextMeshProUGUI TireRateText;

    GameManager gameManager;

    [SerializeField] private Image[] lifeImages;
    [SerializeField] private TextMeshProUGUI OverFiveLifeText;
    [SerializeField] private TextMeshProUGUI OverFiveLifeNumber;
    [SerializeField] private EventManager eventManager;



    private void Start()
    {
        eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
        gameManager = GameManager.Instance;
        BestScoreText.text = Mathf.Round(PlayerPrefs.GetFloat("score")).ToString();
        UpdateDamageWithoutIncrease();
        UpdateTireRateWithoutIncrease();
    }

    public void UpdateLife()
    {
        if (PlayerManager.Instance.PlayerLife >= 5)
        {
            lifeImages[1].enabled = false;
            lifeImages[2].enabled = false;
            lifeImages[3].enabled = false;
            lifeImages[4].enabled = false;
            OverFiveLifeNumber.text = PlayerManager.Instance.PlayerLife.ToString();
            OverFiveLifeNumber.enabled = true;
            OverFiveLifeText.enabled = true;
            gameManager.isMaxLife = true;
        } else
        {
            OverFiveLifeNumber.enabled = false;
            OverFiveLifeText.enabled = false;
            gameManager.isMaxLife = false;
            for (int i = 0; i < lifeImages.Length; i++)
            {
                if (PlayerManager.Instance.PlayerLife > i)
                {
                    lifeImages[i].enabled = true;
                }
                else
                {
                    lifeImages[i].enabled = false;
                }
            }
        }
    }

    public void updateScore()
    {
        if (!eventManager.chooseItems)
        {
            gameManager.score += Time.deltaTime;
            ScoreText.text = Mathf.Round(gameManager.score).ToString();
        }
    }

    public void UpdateDamage()
    {
        if (gameManager.isRedPlayer)
        {

            PlayerManager.Instance.PlayerDamage += gameManager.boostRedChillyPeper;
        string damageText = ((PlayerManager.Instance.PlayerDamage * PlayerManager.Instance.PlayerLevel)).ToString();
        string finalText = "";
        for (int i = 0; i < damageText.Length; i++)
        {
            if (i < 4)
            {
                finalText += damageText[i];
            }
        }
        DamageText.text = finalText;
        } else
        {
            PlayerManager.Instance.PlayerDamage += gameManager.boostRedChillyPeper;
            if (PlayerManager.Instance.PlayerDamage >= 15)
            {
                PlayerManager.Instance.PlayerDamage = 15;
                DamageText.text = "Max";
            } else
            {
                DamageText.text = PlayerManager.Instance.PlayerDamage.ToString();
            }
        }
    }

    public void UpdateTireRate()
    {
        if (PlayerManager.Instance.RateShoot <= 0.5 && gameManager.isRedPlayer)
        {
            gameManager.isMaxTireRate = true;
            PlayerManager.Instance.RateShoot = 0.5f;
        } else if (PlayerManager.Instance.RateShoot <= 0.3 && gameManager.isGreenPlayer && PlayerManager.Instance.PlayerLevel == 5)
        {
            gameManager.isMaxTireRate = true;
        }  else if (gameManager.isRedPlayer)
        {
            PlayerManager.Instance.RateShoot -= gameManager.boostGreenChillyPeper;
        }
        TireRateText.text = PlayerManager.Instance.RateShoot.ToString();
    }

    public void UpdateDamageWithoutIncrease()
    {
        if (gameManager.isRedPlayer)
        {

        string damageText = ((PlayerManager.Instance.PlayerDamage * PlayerManager.Instance.PlayerLevel)).ToString();
        string finalText = "";
        for (int i = 0; i < damageText.Length; i++)
        {
            if (i < 4)
            {
                finalText += damageText[i];
            }
        }
        DamageText.text = finalText;
        } else
        {
            DamageText.text = PlayerManager.Instance.PlayerDamage.ToString();
        }
    }

    public void UpdateTireRateWithoutIncrease()
    {
        //TireRateText.text = (totalTireRate).ToString();
        TireRateText.text = PlayerManager.Instance.RateShoot.ToString();

    }
}