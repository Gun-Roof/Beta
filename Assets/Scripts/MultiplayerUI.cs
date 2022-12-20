using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class MultiplayerUI : MonoBehaviour
{
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private Text deadText;
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
}
