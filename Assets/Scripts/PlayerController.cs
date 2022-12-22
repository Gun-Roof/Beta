using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerController : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite bluePlayer;
    [SerializeField] private Sprite redPlayer;
    private GameObject player;

    [Header("Player Settings")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private float speed;
    [SerializeField] bool PcControler;
    
    private bool isHost;
    private Rigidbody2D rb;
    private PhotonView photonView;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    [Header("Other")]
    public bool facingRight = true;
    public bool dead = false;

    private void Start()
    {
        joystick = GameObject.FindGameObjectWithTag("joystick").GetComponent<Joystick>();
        rb = GetComponent<Rigidbody2D>();
        photonView = GetComponent<PhotonView>();

        isHost = PhotonNetwork.IsMasterClient;

        if (isHost)
        {
            gameObject.tag = "Host";
            transform.GetComponent<SpriteRenderer>().sprite = redPlayer;
            transform.position = new Vector3(5f, -1.7f, 0f);
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<SpriteRenderer>().sprite = bluePlayer;
        }
        else if(!isHost)
        {
            gameObject.tag = "Player";
            transform.GetComponent<SpriteRenderer>().sprite = bluePlayer;
            transform.position = new Vector3(-5f, -1.7f, 0f);
            player = GameObject.FindGameObjectWithTag("Host");
            player.GetComponent<SpriteRenderer>().sprite = redPlayer;
        }
    }

    private void Update()
    {
        if(!photonView.IsMine)
            return;

        if (PcControler)
        {
            float ver = Input.GetAxis("Vertical");
            float hor = Input.GetAxis("Horizontal");
            moveInput = new Vector2(hor, ver);      
        }
        else
        {
            moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        }


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
            transform.position = new Vector3(0f, 100f, 0f);

            dead = true;
        }
    }

}