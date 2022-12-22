using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask whatIsSolid;

    public bool facingRight;

    private void Start()
    {
       Invoke("DestroyBullet", lifeTime);
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);

        if (lifeTime < 0)
        {
            DestroyBullet();
        }

        if(facingRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void DestroyBullet()
    {
        PhotonNetwork.Instantiate(Path.Combine("BulletEffect"), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
