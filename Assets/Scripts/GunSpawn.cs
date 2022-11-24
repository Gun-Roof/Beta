using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class GunSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] Guns;    // Start is called before the first frame update
    [SerializeField] float SpawnTime, DeleteTime;
    float timer;
    void Start()
    {
        GunsSpawn();
    }

    // Update is called once per frame
    void Update()
    {
       timer += Time.deltaTime;
        if (timer>SpawnTime)
        {
            timer =0;
            GunsSpawn();
        }
    }
    public void GunsSpawn()
    {
        string GunName = Guns[Random.Range(0, 3)].name;
        PhotonNetwork.Instantiate(GunName, new Vector3(Random.Range(-7f, 7f), Random.Range(-3.9f, -1.4f), 0),transform.rotation);
        //PhotonNetwork.Destroy(GunName,DeleteTime);
    }
}
