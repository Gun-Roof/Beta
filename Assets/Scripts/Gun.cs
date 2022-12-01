using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Build.Content;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private Transform shotPoint;

    public void Shoot()
    {
        bulletPrefab.GetComponent<Bullet>().facingRight = player.GetComponent<PlayerController>().facingRight;
        PhotonNetwork.Instantiate(Path.Combine("Bullet"), shotPoint.position, transform.rotation);
    }
}
