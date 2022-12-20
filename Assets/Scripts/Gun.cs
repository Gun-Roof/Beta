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
    [SerializeField] private Transform shotPoint;
    [SerializeField] private Sprite gun1, gun2, gun3, gun4;

    [Header("Bullet settings")]
    [SerializeField] private float startTimeBtwShots;
    [SerializeField] private float offset;
    [SerializeField] private bool PcControler;

    private PlayerController player;

    private bool isHost;
    private float timeBtwShots;
    private float rotZ;
    private string gun = "gun2";

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
        joystick = GameObject.FindGameObjectWithTag("joystickA").GetComponent<Joystick>();
        isHost = PhotonNetwork.IsMasterClient;
    }

    private void Update()
    {
        bullet.facingRight = player.facingRight;

        //if (PcControler)
        //{
        //    Vector3 mousepoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        //    Vector3 diff = mousepoint - transform.position;
        //    diff.Normalize();
        //    rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //}
        //else
        //{
        //    if (isHost)
        //    {
        //        if (Mathf.Abs(hostJoystick.Horizontal) > 0.3f || Mathf.Abs(hostJoystick.Vertical) > 0.3f)
        //            rotZ = Mathf.Atan2(hostJoystick.Vertical, hostJoystick.Horizontal) * Mathf.Rad2Deg;
        //    }
        //    else
        //    {
        //        if (Mathf.Abs(joystick.Horizontal) > 0.3f || Mathf.Abs(joystick.Vertical) > 0.3f)
        //            rotZ = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
        //    }
        //}

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
        }
        if (collision.tag == "gun2")
        {
            GetComponent<SpriteRenderer>().sprite = gun2;
            gun = "gun2";
            startTimeBtwShots = 2;
        }
        if (collision.tag == "gun3")
        {
            GetComponent<SpriteRenderer>().sprite = gun3;
            gun = "gun3";
            startTimeBtwShots = 0.5f;
        }
        if (collision.tag == "gun4")
        {
            GetComponent<SpriteRenderer>().sprite = gun4;
            gun = "gun4";
            startTimeBtwShots = 3;
        }
    }
}