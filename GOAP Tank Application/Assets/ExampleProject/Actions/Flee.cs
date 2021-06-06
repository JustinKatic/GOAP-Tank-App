using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;


public class Flee : Action
{

    bool posFound = false;
    float wanderTime = 3f;
    float wanderTimer = 0;
    public GameObjectRuntimeSet enemyRuntimeSet;
    GameObject closestEnemyObj;
    float closestEnemy = Mathf.Infinity;



    public override bool OnEnter()
    {
        posFound = false;
        wanderTimer = 0;
        agent.isStopped = false;
        closestEnemy = Mathf.Infinity;
        closestEnemyObj = null;

        return true;
    }

    //___________________________________________________________________________________________________


    public override void OnTick()
    {
        if (posFound == false)
        {
            for (int i = 0; i < enemyRuntimeSet.Length(); i++)
            {
                float dist = Vector3.Distance(transform.position, enemyRuntimeSet.GetItemIndex(i).transform.position);
                if (dist < closestEnemy)
                {
                    closestEnemy = dist;
                    closestEnemyObj = enemyRuntimeSet.GetItemIndex(i);
                }
            }
            if (destination != null)
                destination = transform.position + ((transform.position - closestEnemyObj.transform.position) * 0.1f);
            posFound = true;

        }

        if (posFound == true)
        {
            wanderTimer += Time.deltaTime;
            agent.SetDestination(destination);
            if (wanderTimer >= wanderTime)
            {
                posFound = false;
                wanderTimer = 0;
                closestEnemy = Mathf.Infinity;
                closestEnemyObj = null;
            }
        }
    }


    //___________________________________________________________________________________________________



    public override bool ConditionToExit()
    {
        if (World.Instance.GetWorldStates().HasState("AmmoAvailable") && agentPersonalState.HasState("NeedAmmo") && agentPersonalState.HasState("CanShoot"))
        {
            return true;
        }
        return false;
    }


    //___________________________________________________________________________________________________



    public override bool OnExit()
    {
        //Write Logic here for what the agent does when it Exits this action
        return true;
    }

}
