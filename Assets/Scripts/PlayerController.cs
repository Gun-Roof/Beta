using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Fire")]
    [SerializeField] private Button fireButton;
    [SerializeField] private Gun gun;

    [Header("Move")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private float speed;

    private bool isHost;
    private bool facingRight = true;
    private Rigidbody2D rb;
    private PhotonView photonView;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        photonView = GetComponent<PhotonView>();
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        fireButton = GameObject.FindGameObjectWithTag("FireButton").GetComponent<Button>();
        gun = GetComponentInChildren<Gun>();

        fireButton.onClick.AddListener(gun.Shoot);

        isHost = PhotonNetwork.IsMasterClient;

        if (isHost)
            gameObject.tag = "Host";
        else
            gameObject.tag = "Player";
    }

    private void Update()
    {
        if (!photonView.IsMine)
            return;

        moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        moveVelocity = moveInput.normalized * speed;

        if (!facingRight && moveInput.x > 0)
            Flip();
        else if (facingRight && moveInput.x < 0)
            Flip();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            transform.position = new Vector3(0f, -1.7f, 0f);
        }
    }
}
