using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHitFade : MonoBehaviour
{
    public Animation animation;
    bool stop;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            stop = true;
            StartCoroutine(playAnim());
        }

    }
    IEnumerator playAnim()
    {
        animation.Play();
        yield return new WaitForSeconds(2f);
        stop = false;
        gameObject.SetActive(false);

    }
}
