using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Joystick hostJoystick;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Transform shotPoint;

    [SerializeField] private float startTimeBtwShots;
    [SerializeField] private float offset;

    private bool isHost;
    private float timeBtwShots;
    private float rotZ;

    private void Start()
    {
        hostJoystick = GameObject.FindGameObjectWithTag("hostJoystickA").GetComponent<Joystick>();
        joystick = GameObject.FindGameObjectWithTag("joystickA").GetComponent<Joystick>();
        isHost = PhotonNetwork.IsMasterClient;
        if (isHost)
        {
            hostJoystick.gameObject.SetActive(true);
            joystick.gameObject.SetActive(false);
        }
        else
        {
            hostJoystick.gameObject.SetActive(false);
            joystick.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (isHost)
        {
            if (Mathf.Abs(hostJoystick.Horizontal) > 0.3f || Mathf.Abs(hostJoystick.Vertical) > 0.3f)
                rotZ = Mathf.Atan2(hostJoystick.Vertical, hostJoystick.Horizontal) * Mathf.Rad2Deg;
        }
        else
        {
            if (Mathf.Abs(joystick.Horizontal) > 0.3f || Mathf.Abs(joystick.Vertical) > 0.3f)
                rotZ = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBtwShots <= 0)
        {
            if (isHost)
            {
                if (hostJoystick.Horizontal != 0 || hostJoystick.Vertical != 0)
                    Shoot();
            }
            else
            {
                if (joystick.Horizontal != 0 || joystick.Vertical != 0)
                    Shoot();
            }
        }
        else
            timeBtwShots -= Time.deltaTime;
    }

    private void Shoot()
    {
        PhotonNetwork.Instantiate(Path.Combine("Bullet"), shotPoint.position, transform.rotation);
        timeBtwShots = startTimeBtwShots;
    }
}
