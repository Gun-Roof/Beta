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
    PlayerManager pm;
    [SerializeField] GameObject player;
     [SerializeField]GameObject[] players;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject gm;

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
    float timer = 0;

    private void Start()
    {
        joystick = GameObject.FindGameObjectWithTag("joystick").GetComponent<Joystick>();
        rb = GetComponent<Rigidbody2D>();
        photonView = GetComponent<PhotonView>();

        isHost = PhotonNetwork.IsMasterClient;
       // gun.SetActive(true);
    }

    private void Update()
    {
        Debug.Log(PhotonNetwork.GetPing());
        if(!photonView.IsMine)
            return;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        timer += Time.deltaTime;
        if (timer>1)
        {
            gun.SetActive(true);
            timer = -100000000000;
        }

        gameObject.tag = "Player";
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
        
        if (isHost)
        {
            
            players = GameObject.FindGameObjectsWithTag("Player");
            transform.GetComponent<SpriteRenderer>().sprite = redPlayer;
            //transform.position = new Vector3(5f, -1.7f, 0f);
            if (Vector3.Distance(players[0].transform.position, transform.position) > 0)
            {
                players[0].GetComponent<SpriteRenderer>().sprite = bluePlayer;
            }
            else
            {
                players[1].GetComponent<SpriteRenderer>().sprite = bluePlayer;
            }
        }
        else if (!isHost)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            transform.GetComponent<SpriteRenderer>().sprite = bluePlayer;
            // transform.position = new Vector3(-5f, -1.7f, 0f);
            if (Vector3.Distance(players[0].transform.position, transform.position) > 0)
            {
                players[0].GetComponent<SpriteRenderer>().sprite = redPlayer;
            }
            else
            {
                players[1].GetComponent<SpriteRenderer>().sprite = redPlayer;
            }            
        }
        if (players.Length>2&& PhotonNetwork.PlayerList.Length == 2)
        {
            for (int i = 2; i < players.Length; i++)
            {
                Destroy(players[i]);
            }
        }
         if (players.Length < 2 && PhotonNetwork.PlayerList.Length == 2)
        {
            pm.CreateController();
        }
        //if (transform.childCount == 0)
        //{
        //    PhotonNetwork.Instantiate("gun", new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
        //    gun = GameObject.FindGameObjectWithTag("gun1");
        //    gun.transform.SetParent(players[1].transform);
        //    gun.transform.SetParent(players[0].transform);
        //}
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
            transform.position = new Vector3(0f, 0f, 0f);

            dead = true;
        }
    }

}