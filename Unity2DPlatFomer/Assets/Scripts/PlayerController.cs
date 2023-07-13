using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 0;
    private Rigidbody2D rigid = null;
    private SpriteRenderer spriteRenderer = null;
    private Animator animator = null;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0, rigid.velocity.y);
        }

        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1f;
        }

        if (rigid.velocity.normalized.x == 0)
        {
            animator.SetBool("isWalk", false);
        }
        else
        {
            animator.SetBool("isWalk", true);
        }

    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed* (-1), rigid.velocity.y);
        }
    }
}
