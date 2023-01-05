using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reclining : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    Rigidbody2D rb;
    bool otkid = false;
    [SerializeField] float cor;
    [SerializeField] int UrOtkid = 300;
    [SerializeField] private PlayerController pl;
    GameObject[] players;
    [SerializeField] GameObject player;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        //Debug.Log(rb.transform.position.y);
        if (Vector3.Distance(players[0].transform.position, transform.position) > 0)
        {
            player = players[0];
        }
        else
        {
            player = players[1];
        }
        if (rb.gravityScale != 0 && otkid == true && rb.transform.position.y < cor)
        {
            rb.gravityScale = 0;
            otkid = false;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            cor = gameObject.transform.position.y;
            UrOtkid += 50;
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Bullet")
        {
            bool fr = PlayerPrefs.GetInt("fr") == 1 ? true : false;
            rb.constraints = RigidbodyConstraints2D.None;
            cor = gameObject.transform.position.y;
            rb.gravityScale = 1;
            Debug.Log(fr);
            if (player.transform.position.x < gameObject.transform.position.x)
            {
                rb.AddForce(new Vector2(1, 1) * UrOtkid);
            }
            else
            {
                rb.AddForce(new Vector2(-1, 1) * UrOtkid);
            }

            //rb.AddForce(new Vector2(-1, 1) * UrOtkid);
            Destroy(collision.gameObject);
            otkid = true;
            Destroy(collision.gameObject);

        }

    }

}
