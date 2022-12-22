using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class GunSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] Guns;
    [SerializeField] private float SpawnTime, DeleteTime;

    private GameObject gun;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > SpawnTime)
        {
            timer = 0;
            GunsSpawn();
        }
    }

    public void GunsSpawn()
    {
        string GunName = Guns[Random.Range(0, 3)].name;
        gun = PhotonNetwork.Instantiate(GunName, new Vector3(Random.Range(-7f, 7f), Random.Range(-3.9f, -1.4f), 0), transform.rotation);
    }
}
