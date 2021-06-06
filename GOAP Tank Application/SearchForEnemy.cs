using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;


public class SearchForEnemy : Action
{
    GameObject closestEnemyObj = null;
    public GameObjectRuntimeSet enemyRuntimeSet;

    public override bool OnEnter()
    {
        if (inventory.FindItemWithTag("Tank"))
            return false;

        if (enemyRuntimeSet.Length() <= 0)
            return false;

        float closestEnemy = Mathf.Infinity;


        for (int i = 0; i < enemyRuntimeSet.Length(); i++)
        {
            float dist = Vector3.Distance(transform.position, enemyRuntimeSet.GetItemIndex(i).transform.position);
            if (dist < closestEnemy)
            {
                closestEnemy = dist;
                closestEnemyObj = enemyRuntimeSet.GetItemIndex(i);
            }
        }
        inventory.AddItem(closestEnemyObj);
        agentPersonalState.AddPersonalState("FoundEnemy");
        return true;
    }


    public override bool OnExit()
    {
        return true;
    }

    public override void OnTick()
    {
        throw new System.NotImplementedException();
    }

    public override bool ConditionToExit()
    {
        throw new System.NotImplementedException();
    }
}
