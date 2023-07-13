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
    }

    private void NextState()
    {
        nextMove = Random.Range(-1, 2);
        
        Invoke("NextState", 5f);
    }
}
