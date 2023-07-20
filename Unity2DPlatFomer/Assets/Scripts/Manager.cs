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

    [SerializeField] private PlayerController player = null;

    [SerializeField] private GameObject[] stages;
    
    public void NextStage()
    {
        if (stageIndex < stages.Length - 1)
        {
            stages[stageIndex].SetActive(false);
            stageIndex++;
            stages[stageIndex].SetActive(true);
            PlayerReposition();
        }
        else
        {
            Time.timeScale = 0;
        }

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

    private void PlayerReposition()
    {
        player.transform.position = new Vector3(0, 0, -1);
        player.VelocityZero();
    }
}
