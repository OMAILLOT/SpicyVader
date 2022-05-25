using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update

    GameManager gameManager;
    public Text hightScoreText;
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        hightScoreText.text = Mathf.Round(PlayerPrefs.GetFloat("score")).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
