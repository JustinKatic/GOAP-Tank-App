using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GOAP
{
    public abstract class Action : MonoBehaviour
    {
        // Name of the action
        [HideInInspector]
        public string actionName;
        // Cost of the action
        [Tooltip("Cost To Complete action, lowest cost of all actions will be the final path")]
        public float costOfAction = 1.0f;
        // Duration the action should take

        [HideInInspector]
        public GameObject target;

        // Out target destination for the 
        [HideInInspector]
        public Vector3 destination = Vector3.zero;
        // checks if this action is the current active action
        [HideInInspector] public bool IsActionActive = false;


        // nav agent attached to gameobject
        [HideInInspector] public NavMeshAgent agent;

        //array of WorldStates requiredConditions
        [Tooltip("Conditions that need to be met before this action is considered in final path")]
        public WorldState[] requiredConditions;
        //array of WorldStates of effectOnCompletion
        [Tooltip("Conditions that need to be met before this action is considered in final path")]
        public WorldState[] postConditions;

        // Dictionary of requiredConditions
        public Dictionary<string, int> requiredConditionsDictionary;
        // Dictionary of effectsOnCompletion
        public Dictionary<string, int> postConditionsDictionary;
        // Access our inventory
        public Inventory inventory;
        //agents personalState 
        public WorldStates agentPersonalState;



        // Constructor
        public Action()
        {
            // creates dictionaries that hold our requiredConditions and EffectsOnCompleteion
            requiredConditionsDictionary = new Dictionary<string, int>();
            postConditionsDictionary = new Dictionary<string, int>();
        }


        private void Awake()
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
            //add requiredConditions to the requiredConditionsDictionary.
            if (requiredConditions != null)
            {
                foreach (WorldState w in requiredConditions)
                {
                    requiredConditionsDictionary.Add(w.action, w.value);
                }
            }

            //add effectsOnCompletion to the effectsOnCompletionDictionary
            if (postConditions != null)
            {
                foreach (WorldState w in postConditions)
                {
                    postConditionsDictionary.Add(w.action, w.value);
                }
            }


            // gets refrence to inventory on agent
            inventory = GetComponent<Agent>().inventory;

            // Gets refrence to agents personal states
            agentPersonalState = GetComponent<Agent>().agentPersonalState;
        }


        //checks if conditions passed in match all the required conditions
        public bool IsAhievableGiven(Dictionary<string, int> conditions)
        {
            foreach (KeyValuePair<string, int> requiredCondition in requiredConditionsDictionary)
            {
                if (!conditions.ContainsKey(requiredCondition.Key))
                {
                    return false;
                }
            }
            return true;
        }



        //Abstract classes that are required when we inherit from this class
        public abstract bool OnEnter(); // called when we enter this action
        public abstract bool OnExit(); //called when we exit this action

        public abstract void OnTick(); //called when we exit this action
        public abstract bool ConditionToExit(); //called when we exit this action

    }
}

