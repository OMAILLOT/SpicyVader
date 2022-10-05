using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShip : MonoBehaviour, IPooledObject
{
    [SerializeField] private float PV = 50;
    [SerializeField] private float speed = 3;
    [SerializeField] private float TireRate;
    [SerializeField] private float dropChance = 0.3f;
    [SerializeField] private float dropLifeChance = 0.05f;

    [SerializeField] private ParticleSystem particleShoot;
    [SerializeField] private ParticleSystem Explosion;
    [SerializeField] private AudioClip[] ExplosionSound;
    [Space(5)]
    [SerializeField] private GameObject bullet;
    [SerializeField] private AudioClip[] shootAudios;
    private int musicIndex;

    float nextActionTime = 0.0f;

    void IPooledObject.OnObjectSpawn()
    {
        StartCoroutine(GoToPosition(BasicShipEvent.Instance.placeHolderLines[0].cellPlaceHolder[0]));
        BasicShipEvent.Instance.placeHolderLines[0].cellPlaceHolder.Remove(BasicShipEvent.Instance.placeHolderLines[0].cellPlaceHolder[0]);
    }

    void Update()
    {
        if (Time.time >= nextActionTime)
        {
            nextActionTime += TireRate + Random.Range(0, 1f);
            musicIndex = Random.Range(0, shootAudios.Length);
            AudioManager.Instance.PlayClipAt(shootAudios[musicIndex], transform.position);
            bullet.transform.position = transform.position;
            Instantiate(bullet);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bullet>())
        {
            PV -= PlayerManager.Instance.PlayerDamage;
            if (PV <= 0)
            {
                Instantiate(Explosion, transform.position, Quaternion.identity);
                musicIndex = Random.Range(0, ExplosionSound.Length);
                AudioManager.Instance.PlayClipAt(ExplosionSound[musicIndex], transform.position);
                gameObject.SetActive(false);
                BasicShipEvent.Instance.IncrementBasicShipDestroyCount();
            }
        }

        if (collision.GetComponent<PlayerMovement>())
        {
            PlayerManager.Instance.PlayerHit();
        }
    }

    IEnumerator GoToPosition(Transform placeHolder)
    {
        while(transform.position != placeHolder.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, placeHolder.position, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
    }

    
}
