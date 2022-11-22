using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField]private float posY;

    private float back_pos;
    private Transform back_Tranform;
    private float back_Size;

    private void Start()
    {
        back_Tranform = GetComponent<Transform>();
        back_Size = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        back_pos += speed * Time.deltaTime;
        back_pos = Mathf.Repeat(back_pos, back_Size);
        back_Tranform.position = new Vector3(back_pos, posY, 0f);
    }
}