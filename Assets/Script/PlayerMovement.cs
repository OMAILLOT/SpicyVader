using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoSingleton<PlayerMovement>
{

    // Could also already reference this in the Inspector if possible
    [SerializeField] private Camera _camera;
    [SerializeField] private float playerSpeed;
    [SerializeField] private Vector3 goToPostion = new Vector3(0, 0, 0);

    private void FixedUpdate()
    {

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            var touch_Pos = _camera.ScreenToWorldPoint(touch.position);
            goToPostion = new Vector3(touch_Pos.x,touch_Pos.y,0);
            Vector3.Lerp(transform.position, goToPostion,100f);
            transform.position = Vector3.MoveTowards(transform.position, goToPostion, playerSpeed * Time.fixedDeltaTime);
        }
    }



}
