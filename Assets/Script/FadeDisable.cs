using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeDisable : MonoBehaviour
{

    // Update is called once per frame
    public Image image;
    private bool stop = true;
    void Update()
    {
        if (stop)
        {

            if (Time.time >= 2f)
            {
                    image.enabled = false;
                    stop = false;
            }
        }
    }

}
