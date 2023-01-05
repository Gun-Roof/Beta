using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PhotonView _photonView;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        if (_photonView.IsMine)
        {
            CreateController();
        }
    }

    public void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("Player"), new Vector3(Random.Range(-5f,5f), Random.Range(-1f, -2.5f), 0f), Quaternion.identity);
    }
}
