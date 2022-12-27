using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class Gun : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private Bullet bullet;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Joystick joystick;
    [SerializeField] private GameObject[] shotPoints;
    [SerializeField] private Sprite gun1, gun2, gun3, gun4;

    [Header("Bullet settings")]
    [SerializeField] private float startTimeBtwShots;
    [SerializeField] private float offset;
    [SerializeField] private bool PcControler;

    private PlayerController player;

    private float timeBtwShots;
    private float rotZ;
    private string gun = "gun2";

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
        joystick = GameObject.FindGameObjectWithTag("joystickA").GetComponent<Joystick>();
    }

    private void Update()
    {
        bullet.facingRight = player.facingRight;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBtwShots <= 0)
        {
            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
                Shoot(gun);
        }
        else
            timeBtwShots -= Time.deltaTime;
    }

    private void Shoot(string gun)
    {
        Transform shotPoint;
        shotPoints = GameObject.FindGameObjectsWithTag("sp");    
        
            if (Vector3.Distance(shotPoints[0].transform.position, transform.position) > 0.5f)
            {
            shotPoint = shotPoints[1].transform;
            }
            else
            {
            shotPoint = shotPoints[0].transform;

        }        
            Debug.Log(shotPoint);
        switch (gun)
        {
            
            case "gun1":
                for (int i = 0; i < 4; i++)
                {
                        PhotonNetwork.Instantiate(Path.Combine("Bullet"), shotPoint.position, Quaternion.Euler(0, 0, Random.Range(- 30, 30)));
                }


                break;
            case "gun2":
                PhotonNetwork.Instantiate(Path.Combine("Bullet"), shotPoint.position, transform.rotation);
                break;
            case "gun3":
                PhotonNetwork.Instantiate(Path.Combine("Bullet"), shotPoint.position, transform.rotation);
                break;
            case "gun4":
                PhotonNetwork.Instantiate(Path.Combine("Bullet"), shotPoint.position, transform.rotation);
                break;
        }

        timeBtwShots = startTimeBtwShots;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "gun1")
        {
            GetComponent<SpriteRenderer>().sprite = gun1;
            gun = "gun1";
            startTimeBtwShots = 4;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "gun2")
        {
            GetComponent<SpriteRenderer>().sprite = gun2;
            gun = "gun2";
            startTimeBtwShots = 2;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "gun3")
        {
            GetComponent<SpriteRenderer>().sprite = gun3;
            gun = "gun3";
            startTimeBtwShots = 0.5f;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "gun4")
        {
            GetComponent<SpriteRenderer>().sprite = gun4;
            gun = "gun4";
            startTimeBtwShots = 3;
            Destroy(collision.gameObject);
        }
    }
}