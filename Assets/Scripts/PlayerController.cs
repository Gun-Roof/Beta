using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Joystick hostJoystick;
    [SerializeField] private Joystick joystick;
    [SerializeField] private float speed;

    private bool isHost;
    private bool dead = false;
    private bool facingRight = true;
    private Rigidbody2D rb;
    private PhotonView photonView;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    private void Start()
    {
        hostJoystick = GameObject.FindGameObjectWithTag("hostJoystick").GetComponent<Joystick>();
        joystick = GameObject.FindGameObjectWithTag("joystick").GetComponent<Joystick>();
        rb = GetComponent<Rigidbody2D>();
        photonView = GetComponent<PhotonView>();
        isHost = PhotonNetwork.IsMasterClient;
        if (isHost)
        {
            hostJoystick.gameObject.SetActive(true);
            joystick.gameObject.SetActive(false);
        }
        else
        {
            hostJoystick.gameObject.SetActive(false);
            joystick.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if(!photonView.IsMine)
            return;

        if(isHost)
            moveInput = new Vector2(hostJoystick.Horizontal, hostJoystick.Vertical);
        else
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
            dead = true;
        }
    }
}
