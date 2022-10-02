using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Collectible : MonoBehaviour
{
    public new AudioClip audio;
    public TextMeshPro PickupText;
    public float collectibleSpeed;


    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - collectibleSpeed * Time.deltaTime);
        if (transform.position.y <= -10)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PickupCollectible();
            Instantiate(PickupText);
            AudioManager.Instance.PlayClipAt(audio, transform.position);
            gameObject.SetActive(false);
        }
    }

    public virtual void PickupCollectible()
    {

    }
}
