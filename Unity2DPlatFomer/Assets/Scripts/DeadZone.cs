using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private Manager _manager = null;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (_manager.health > 1)
            {
                other.attachedRigidbody.velocity = Vector2.zero;
                other.transform.position = new Vector3(0, 0, -1);
            }
            _manager.HealthDown();
        }
        
    }
}
