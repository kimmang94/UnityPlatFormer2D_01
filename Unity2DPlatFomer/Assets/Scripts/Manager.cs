using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public int totalPoint = 0;
    public int stagePoint = 0;
    public int stageIndex = 0;
    public int health = 3;

    [SerializeField] private PlayerController player;
    
    
    public void NextStage()
    {
        stageIndex++;
        totalPoint = stagePoint;
        stagePoint = 0;
    }

    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
        }
        else
        {
            player.OnDie();
        }
    }
}
