using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private Transform shootPos;
    [SerializeField] private Joystick joystick;
    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private PhotonView photonView;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private bool isHost;

    public bool facingRight = true;
    public bool dead;
    public bool hostDead;

    private void Start()
    {
        dead = false;
        hostDead = false;

        isHost = PhotonNetwork.IsMasterClient;
        if (isHost)
            gameObject.tag = "Host";
        else
            gameObject.tag = "Player";

        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();

        rb = GetComponent<Rigidbody2D>();
        photonView = GetComponent<PhotonView>();
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

        if(facingRight)
            shootPos.localRotation = Quaternion.Euler(0f, 0f, 0f);
        else
            shootPos.localRotation = Quaternion.Euler(0f, 180f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            transform.position = new Vector3(0f, -1.7f, 0f);
            if (isHost)
                hostDead = true;
            else
                dead = true;
        }
    }
}
