using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControl : MonoBehaviour
{
    [SerializeField] private Transform canvasManager;

    private GameObject hostCanvas;
    private GameObject canvas;

    private bool isHost;

    private void Start()
    {
        Invoke("LoadInterface", 2);
        isHost = PhotonNetwork.IsMasterClient;
    }

    private void LoadInterface()
    {
        hostCanvas = GameObject.FindGameObjectWithTag("hostCanvas");
        canvas = GameObject.FindGameObjectWithTag("canvas");

        hostCanvas.transform.SetParent(canvasManager);
        canvas.transform.SetParent(canvasManager);

        if (isHost)
            canvas.SetActive(false);
        else
            hostCanvas.SetActive(false);
    }
}
