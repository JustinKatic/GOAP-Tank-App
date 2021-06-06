using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;


public class Shoot : Action
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public LayerMask enemyLayermask;
    bool targetLOS;


    public override bool OnEnter()
    {
        agent.isStopped = true;
        if (GetComponent<Tank>().shotFired == true)
            return false;



        return true;
    }



    //___________________________________________________________________________________________________



    public override void OnTick()
    {
        LookTowards();
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), Mathf.Infinity, enemyLayermask))
        {
            targetLOS = true;
        }
        else
            targetLOS = false;
    }


    //___________________________________________________________________________________________________





    public override bool ConditionToExit()
    {
        if (targetLOS)
            return true;
        else
            return false;

    }



    //___________________________________________________________________________________________________


    public override bool OnExit()
    {
        inventory.RemoveItem(inventory.FindItemWithTag("Tank"));
        GetComponent<Tank>().shotFired = true;
        agentPersonalState.RemoveState("CanShoot");
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        GetComponent<Tank>().ammoCount -= 1;
        agentPersonalState.RemoveState("InRangeOfEnemy");
        return true;
    }

    public void LookTowards()
    {
        Vector3 lookPos = inventory.FindItemWithTag("Tank").transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3);
    }
}
