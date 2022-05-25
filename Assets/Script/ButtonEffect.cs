using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour
{

    public string nameOfScene;
    public Animation fadeSystem;
    public float timeToWait = 3f;
    public Image image;

    public void onClickOnButtonQuit()
    {
        Application.Quit();
    }

    public void loadGame()
    {
        StartCoroutine(loadNextScene());
    }

    public void StartGameWithTuto()
    {
        nameOfScene = "GameWithTutoriel";
        StartCoroutine(loadNextScene());
    }

    public IEnumerator loadNextScene()
    {
        image.enabled = true;
        fadeSystem.enabled = true;
        fadeSystem.Play();
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene(nameOfScene);
    }
}
