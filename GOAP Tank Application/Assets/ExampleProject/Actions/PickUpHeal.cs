using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;


public class PickUpHeal : Action
{
    public GameObjectRuntimeSet healthPackRuntimeSet;
    float closestHealthPack = Mathf.Infinity;
    GameObject closestHealthPackObj;

    public override bool OnEnter()
    {
        agent.isStopped = false;
        closestHealthPack = Mathf.Infinity;
        closestHealthPackObj = null;

        if (healthPackRuntimeSet.Length() <= 0)
            return false;

        for (int i = 0; i < healthPackRuntimeSet.Length(); i++)
        {
            float dist = Vector3.Distance(transform.position, healthPackRuntimeSet.GetItemIndex(i).transform.position);
            if (dist < closestHealthPack)
            {
                closestHealthPack = dist;
                closestHealthPackObj = healthPackRuntimeSet.GetItemIndex(i);
            }
        }
        return true;
    }



    //___________________________________________________________________________________________________



    public override void OnTick()
    {
        agent.SetDestination(closestHealthPackObj.transform.position);

        if (closestHealthPackObj.activeSelf == false)
        {
            IsActionActive = false;
        }
    }


    //___________________________________________________________________________________________________





    public override bool ConditionToExit()
    {
        float distToDest = Vector3.Distance(transform.position, closestHealthPackObj.transform.position);

        if (distToDest <= 4)
            return true;
        else
            return false;

    }



    //___________________________________________________________________________________________________




    public override bool OnExit()
    {
        if (closestHealthPackObj.activeSelf == true)
        {
            closestHealthPackObj.SetActive(false);
            gameObject.GetComponent<TankHealth>().TankHeal();
            agentPersonalState.RemoveState("HealthLow");
            World.Instance.GetWorldStates().ModifyState("HealthAvailable", -1);
        }
        return true;
    }
}