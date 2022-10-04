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
        if (gameManager.PlayerLife >= 5)
        {
            lifeImages[1].enabled = false;
            lifeImages[2].enabled = false;
            lifeImages[3].enabled = false;
            lifeImages[4].enabled = false;
            OverFiveLifeNumber.text = gameManager.PlayerLife.ToString();
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
                if (gameManager.PlayerLife > i)
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

        gameManager.PlayerDamage += gameManager.boostRedChillyPeper;
        string damageText = ((gameManager.PlayerDamage * gameManager.PlayerLevel)).ToString();
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
            gameManager.PlayerDamage += gameManager.boostRedChillyPeper;
            if (gameManager.PlayerDamage >= 15)
            {
                gameManager.PlayerDamage = 15;
                DamageText.text = "Max";
            } else
            {
                DamageText.text = gameManager.PlayerDamage.ToString();
            }
        }
    }

    public void UpdateTireRate()
    {
        if (gameManager.RateShoot <= 0.5 && gameManager.isRedPlayer)
        {
            gameManager.isMaxTireRate = true;
            gameManager.RateShoot = 0.5f;
        } else if (gameManager.RateShoot <= 0.3 && gameManager.isGreenPlayer && gameManager.PlayerLevel == 5)
        {
            gameManager.isMaxTireRate = true;
        }  else if (gameManager.isRedPlayer)
        {
            gameManager.RateShoot -= gameManager.boostGreenChillyPeper;
        }
        TireRateText.text = gameManager.RateShoot.ToString();
    }

    public void UpdateDamageWithoutIncrease()
    {
        if (gameManager.isRedPlayer)
        {

        string damageText = ((gameManager.PlayerDamage * gameManager.PlayerLevel)).ToString();
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
            DamageText.text = gameManager.PlayerDamage.ToString();
        }
    }

    public void UpdateTireRateWithoutIncrease()
    {
        //TireRateText.text = (totalTireRate).ToString();
        TireRateText.text = gameManager.RateShoot.ToString();

    }
}