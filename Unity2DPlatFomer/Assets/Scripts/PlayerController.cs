using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 0;
    [SerializeField] private float jumpPower = 0;
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
        
        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJump", true);
        }

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
        
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0f, 1f, 0f));
            RaycastHit2D rayHit2D = Physics2D.Raycast(rigid.position, Vector3.down, 1f, LayerMask.GetMask("PlatForm"));

            if (rayHit2D.collider != null)
            {
                if (rayHit2D.distance < 0.5f)
                {
                    animator.SetBool("isJump", false);
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit");
            OnDamaged(other.transform.position);
        }
    }

    private void OnDamaged(Vector2 targetPos)
    {
        gameObject.layer = 9;

        spriteRenderer.color = new Color(1, 1,1, 0.5f);

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1 ;
        rigid.AddForce(new Vector2(dirc,1) * 7,ForceMode2D.Impulse);
    }
}
