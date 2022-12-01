using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform shootPos;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Joystick joystick;
    [SerializeField] private float speed;

    private bool isHost;
    private bool dead = false;
    private Rigidbody2D rb;
    private PhotonView photonView;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    public bool facingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        photonView = GetComponent<PhotonView>();
        isHost = PhotonNetwork.IsMasterClient;

        if (isHost)
            canvas.gameObject.tag = "hostCanvas";
        else
            canvas.gameObject.tag = "canvas";
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
        }
    }
}
