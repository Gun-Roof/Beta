using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float cor;
    [SerializeField] private int discardingLevel = 7;

    private Rigidbody2D rb;
    private bool discarding = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Debug.Log(rb.transform.position.y);

        if (rb.gravityScale != 0 && discarding == true && rb.transform.position.y < cor)
        {
            rb.gravityScale = 0;
            discarding = false;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            cor = gameObject.transform.position.y;
            discardingLevel++;

            //Debug.Log("jkjkjkjkjkjkjk");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Bullet")
        {
            rb.constraints = RigidbodyConstraints2D.None;
            cor = gameObject.transform.position.y;
            rb.gravityScale = 1;
            rb.AddForce(new Vector2(1, 1.1f) * discardingLevel);
            discarding = true;

            //Destroy(collision.gameObject);
            //Debug.Log(";;;;;;;;;;;;;;;;;");
        }
    }
}
