using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mishan : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    bool otkid = false;
    [SerializeField] float cor;
    [SerializeField] int UrOtkid=7;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(rb.transform.position.y);
        if (rb.gravityScale!=0&&otkid == true&& rb.transform.position.y<cor)
        {
            rb.gravityScale = 0;
            otkid = false;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            cor = gameObject.transform.position.y;
            Debug.Log("jkjkjkjkjkjkjk");
            UrOtkid++;
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
            rb.constraints = RigidbodyConstraints2D.None;
            Debug.Log(";;;;;;;;;;;;;;;;;");
            cor = gameObject.transform.position.y;
            rb.gravityScale = 1;
            rb.AddForce(new Vector2(1,1.1f) * UrOtkid);
          //  Destroy(collision.gameObject);
            otkid = true;
           // Destroy(collision.gameObject);
            
        }
    }
}
