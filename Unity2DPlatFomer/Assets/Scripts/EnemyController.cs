using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rigid = null;

    [SerializeField] private int nextMove = 0;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

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
            nextMove *=  -1;
            CancelInvoke();
            Invoke("NextState", 5f);
        }
    }

    private void NextState()
    {
        nextMove = Random.Range(-1, 2);
        
        Invoke("NextState", 5f);
    }
}
