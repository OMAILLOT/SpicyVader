using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI BestScoreText;


    public TextMeshProUGUI DamageText;
    public TextMeshProUGUI TireRateText;

    GameManager gameManager;

    public Image[] lifeImages;
    public TextMeshProUGUI OverFiveLifeText;
    public TextMeshProUGUI OverFiveLifeNumber;

    public static Inventory instance;
    private float baseTireRate;
    private float totalTireRate;

    private void Awake()
    {

        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instace d'inventory dans la scène");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        BestScoreText.text = Mathf.Round(PlayerPrefs.GetFloat("score")).ToString();
        baseTireRate = gameManager.RateShoot;
        totalTireRate = baseTireRate;
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
        gameManager.score += Time.deltaTime;
        ScoreText.text = Mathf.Round(gameManager.score).ToString();
    }

    public void UpdateDamage()
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
    }

    public void UpdateTireRate()
    {
        gameManager.RateShoot *= gameManager.boostGreenChillyPeper;
        if (gameManager.RateShoot <= 0.3)
        {
            gameManager.isMaxTireRate = true;
            gameManager.RateShoot = 0.3f;
        }

        totalTireRate *= 1 - gameManager.boostGreenChillyPeper + 1;
        string stringTotal = totalTireRate.ToString();
        string tireRateText = stringTotal[0] + "," + stringTotal[2];
        //string tireRateText = (totalTireRate).ToString();
        TireRateText.text = tireRateText;
    }

    public void UpdateDamageWithoutIncrease()
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
    }

    public void UpdateTireRateWithoutIncrease()
    {
        TireRateText.text = (totalTireRate).ToString();
    }
}