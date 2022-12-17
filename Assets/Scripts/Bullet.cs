using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask whatIsSolid;
    float timer = 0;


    public GameObject destroyEffect;

    private void Start()
    {
       // Invoke("DestroyBullet", lifeTime);
    }

    private void Update()
    {
        speed = PlayerPrefs.GetFloat("Bulletspeed");
        lifeTime = PlayerPrefs.GetFloat("BulletLifeTime");
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);       
        timer += Time.deltaTime;
        if (hitInfo.collider != null || timer>lifeTime )
        {
            timer = 0;
            DestroyBullet();
        }

        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void DestroyBullet()
    {
        PhotonNetwork.Instantiate(Path.Combine("BulletEffect"), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyBullet();
    }

}
