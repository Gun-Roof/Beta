using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MultiplayerUI : MonoBehaviour
{
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private PlayerController player;

    private bool isHost;
    private bool isLoaded;
    
    private void Start()
    {
        isLoaded = false;
        Invoke("Load", 1);
        deadPanel.SetActive(false);
    }

    private void Load()
    {
        isHost = PhotonNetwork.IsMasterClient;

        if (isHost)
            player = GameObject.FindGameObjectWithTag("Host").GetComponent<PlayerController>();
        else
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        isLoaded = true;
    }

    private void Update()
    {
        if (isLoaded)
        {
            if (player.dead)
            {
                deadPanel.SetActive(true);
            }
        }
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }

    public void Relive()
    {
        player.dead = false;
        player.transform.position = new Vector3(0f, -1.7f, 0f);
        deadPanel.SetActive(false);
    }
}
