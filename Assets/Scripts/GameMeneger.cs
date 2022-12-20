using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMeneger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float PlayerMoveSpeed;
    [SerializeField] float BulletMoveSpeed;
    [SerializeField] float BulletLifeTime;
    [SerializeField] float GunSpawnTime;
    [SerializeField] float GunDeleteTime;
    [SerializeField] int NumberOfRounds;
    [SerializeField] int RoundTime;

    void Start()
    {
        
    }
    private void Update()
    {
        PlayerPrefs.SetFloat("Playerspeed", PlayerMoveSpeed);
        PlayerPrefs.SetFloat("Bulletspeed", BulletMoveSpeed);
        PlayerPrefs.SetFloat("BulletLifeTime", BulletLifeTime);
        PlayerPrefs.SetFloat("GunSpawnTime", GunSpawnTime);
        PlayerPrefs.SetFloat("GunDeleteTime", GunDeleteTime);
    }
    // Update is called once per frame

}
