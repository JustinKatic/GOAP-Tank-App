using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;


public class PickUpAmmo : Action
{
    public GameObjectRuntimeSet ammoRuntimeSet;
    float closestAmmo = Mathf.Infinity;
    GameObject closestAmmoObj;

    public override bool OnEnter()
    {
        agent.isStopped = false;
        closestAmmo = Mathf.Infinity;
        closestAmmoObj = null;

        if (ammoRuntimeSet.Length() <= 0)
            return false;

        for (int i = 0; i < ammoRuntimeSet.Length(); i++)
        {
            float dist = Vector3.Distance(transform.position, ammoRuntimeSet.GetItemIndex(i).transform.position);
            if (dist < closestAmmo)
            {
                closestAmmo = dist;
                closestAmmoObj = ammoRuntimeSet.GetItemIndex(i);
            }
        }
        return true;
    }



    //___________________________________________________________________________________________________



    public override void OnTick()
    {
        agent.SetDestination(closestAmmoObj.transform.position);

        if (closestAmmoObj.activeSelf == false)
        {
            IsActionActive = false;
        }
    }


    //___________________________________________________________________________________________________





    public override bool ConditionToExit()
    {
        float distToDest = Vector3.Distance(transform.position, closestAmmoObj.transform.position);

        if (distToDest <= 4)
            return true;
        else
            return false;

    }



    //___________________________________________________________________________________________________




    public override bool OnExit()
    {
        if (closestAmmoObj.activeSelf == true)
        {
            closestAmmoObj.SetActive(false);
            gameObject.GetComponent<Tank>().ammoCount = 3;
            agentPersonalState.RemoveState("NeedAmmo");
            agentPersonalState.AddPersonalState("HasAmmo");
            World.Instance.GetWorldStates().ModifyState("AmmoAvailable", -1);
        }
        return true;
    }
}
