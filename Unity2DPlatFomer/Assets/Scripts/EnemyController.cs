using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rigid = null;

    [SerializeField] private int nextMove = 0;

    private Animator animator = null;

    private SpriteRenderer spriteRendrer = null;

    private CircleCollider2D circlecollider = null;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRendrer = GetComponent<SpriteRenderer>();
        circlecollider = GetComponent<CircleCollider2D>();
        Invoke("NextState", 5f);
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);


        Vector2 frontVector = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        Debug.DrawRay(frontVector, Vector3.down, new Color(0, 1f, 0));
        RaycastHit2D raycastHit2D = Physics2D.Raycast(frontVector, Vector3.down, 1f, LayerMask.GetMask("PlatForm"));
        if (raycastHit2D.collider == null)
        {
            Turn();
        }
    }

    private void NextState()
    {
        nextMove = Random.Range(-1, 2);

        animator.SetInteger("WalkSpeed", nextMove);

        if (nextMove != 0)
        {
            spriteRendrer.flipX = nextMove == 1;
        }
        
        float nextTime = Random.Range(2f, 5f);
        Invoke("NextState", nextTime);
        

    }

    private void Turn()
    {
        nextMove *=  -1;
        
        spriteRendrer.flipX = nextMove == 1;
        CancelInvoke();
        Invoke("NextState", 5f);
    }

    public void OnDamaged()
    {
        spriteRendrer.color = new Color(1, 1, 1, 0.5f);
        spriteRendrer.flipY = true;
        circlecollider.enabled = false;
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        Invoke("DeActive", 5);
    }

    private void DeActive()
    {
        gameObject.SetActive(false);
    }
}
