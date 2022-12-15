using Photon.Pun;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button button;
    [SerializeField] private Button hostButton;

    [Header("Player")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private Transform shotPoint;

    private bool isHost;

    private void Start()
    {
        isHost = PhotonNetwork.IsMasterClient;

        button = GameObject.FindGameObjectWithTag("Button").GetComponent<Button>();
        hostButton = GameObject.FindGameObjectWithTag("HostButton").GetComponent<Button>();

        if (isHost)
        {
            button.gameObject.SetActive(false);
            hostButton.onClick.AddListener(Shoot);
        }
        else
        {
            hostButton.gameObject.SetActive(false);
            button.onClick.AddListener(Shoot);
        }
    }

    public void Shoot()
    {
        bulletPrefab.GetComponent<Bullet>().facingRight = player.GetComponent<PlayerController>().facingRight;
        PhotonNetwork.Instantiate(Path.Combine("Bullet"), shotPoint.position, transform.rotation);
    }
}
