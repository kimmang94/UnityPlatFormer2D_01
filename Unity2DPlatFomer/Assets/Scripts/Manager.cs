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
    
    public void NextStage()
    {
        stageIndex++;
        totalPoint = stagePoint;
        stagePoint = 0;
    }
}
