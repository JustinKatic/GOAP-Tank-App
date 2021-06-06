using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;


public class SeekEnemy : Action
{
    public GameObjectRuntimeSet enemyRuntimeSet;
    float closestEnemy = Mathf.Infinity;
    GameObject closestEnemyObj;
    public float inRangeDist;

    public override bool OnEnter()
    {
        agent.isStopped = false;
        closestEnemy = Mathf.Infinity;
        closestEnemyObj = null;

        if (enemyRuntimeSet.Length() <= 0)
            return false;

        for (int i = 0; i < enemyRuntimeSet.Length(); i++)
        {
            float dist = Vector3.Distance(transform.position, enemyRuntimeSet.GetItemIndex(i).transform.position);
            if (dist < closestEnemy)
            {
                closestEnemy = dist;
                closestEnemyObj = enemyRuntimeSet.GetItemIndex(i);
            }
        }
        return true;
    }



    //___________________________________________________________________________________________________



    public override void OnTick()
    {
        agent.SetDestination(closestEnemyObj.transform.position);
        if (!inventory.FindItemWithTag("Tank"))
            inventory.AddItem(closestEnemyObj);
        if (closestEnemyObj.activeSelf == false)
        {
            IsActionActive = false;
        }

    }


    //___________________________________________________________________________________________________





    public override bool ConditionToExit()
    {
        float distToEnemy = Vector3.Distance(transform.position, closestEnemyObj.transform.position);
        if (distToEnemy <= inRangeDist)
        {
            return true;
        }
        else
            return false;
    }



    //___________________________________________________________________________________________________




    public override bool OnExit()
    {
        if (!inventory.FindItemWithTag("Tank"))
            inventory.AddItem(closestEnemyObj);

        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        agentPersonalState.AddPersonalState("InRangeOfEnemy");
        return true;
    }
}
