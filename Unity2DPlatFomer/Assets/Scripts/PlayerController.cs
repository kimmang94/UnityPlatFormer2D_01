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
    [SerializeField]
    private Manager _manager = null;
    
    
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
            if (rigid.velocity.y < 0 && transform.position.y > other.transform.position.y)
            {
                Attack(other.transform);
            }
            else
            {
                OnDamaged(other.transform.position);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Item")
        {
            bool isBronze = other.gameObject.name.Contains("Bronze");
            bool isSilver = other.gameObject.name.Contains("Silver");
            bool isGold = other.gameObject.name.Contains("Gold");

            if (isBronze)
            {
                _manager.stagePoint += 50;
            }
            else if (isSilver)
            {
                _manager.stagePoint += 100;
            }
            else if (isGold)
            {
                _manager.stagePoint += 300;
            }
            
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Finish")
        {
            _manager.NextStage();
        }
    }

    private void OnDamaged(Vector2 targetPos)
    {
        _manager.health--;
        
        gameObject.layer = 9;

        spriteRenderer.color = new Color(1, 1,1, 0.4f);

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc,1) * 3,ForceMode2D.Impulse);
        
        animator.SetTrigger("Damaged");
        
        Invoke("OffDamaged", 3f);
    }

    private void OffDamaged()
    {
        gameObject.layer = 8;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    private void Attack(Transform enemy)
    {
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        EnemyController enemyMove = enemy.GetComponent<EnemyController>();
        _manager.stagePoint += 1;
        enemyMove.OnDamaged();
    }
}
