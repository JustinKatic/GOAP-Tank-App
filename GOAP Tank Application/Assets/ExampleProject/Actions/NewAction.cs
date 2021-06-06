using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;


public class NewAction : Action
{
    public override bool OnEnter()
    {
	//Write Logic here for what the agent does when it Enters this action
        return true;
    }
//___________________________________________________________________________________________________
    public override void OnTick()
    {
        //Logic for what happens each frame this action is active

    }
//___________________________________________________________________________________________________
    public override bool ConditionToExit()
    {
	//The condition that needs to be met to call OnExit() and then complete this action
        return true;
    }
//___________________________________________________________________________________________________
    public override bool OnExit()
    {
	//Write Logic here for what the agent does when it Exits this action
        return true;
    }
}
