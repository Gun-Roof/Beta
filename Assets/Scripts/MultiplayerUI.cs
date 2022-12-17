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
    [SerializeField] private PhotonView photonView;

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
            if (player.hostDead)
            {
                deadPanel.SetActive(true);
                if (isHost)
                    deadText.text = "You die";
                else
                    deadText.text = "You win";
            }
            else if (player.dead)
            {
                deadPanel.SetActive(true);
                if (isHost)
                    deadText.text = "You win";
                else
                    deadText.text = "You die";
            }
        }
    }
}
