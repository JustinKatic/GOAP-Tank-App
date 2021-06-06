using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GOAP;


public class AgentDebugger : EditorWindow
{
    //adds scroll wheel for editor window
    Vector2 scrollPos = Vector2.zero;

    //allows editor to be opened from toolbar
    [MenuItem("Window/Agent Debugger")]
    public static void ShowWindow()
    {
        GetWindow<AgentDebugger>();
    }

    private void Update()
    {
        Repaint();
    }


    private void OnGUI()
    {
        //gets refrence to the gameObject we have selected in scene
        GameObject agent = Selection.activeGameObject;

        //if no object is selected or the object isnt a agent return
        if (Selection.activeGameObject == null || agent.GetComponent<Agent>() == null)
            return;

        //update the scroll bar to correct location each frame
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandHeight(true));

        //display the selected agents name
        EditorGUILayout.LabelField("Agent Name: ", agent.name);

        //display the agents current action it is performing
        if (agent.gameObject.GetComponent<Agent>().currentAction != null)
            EditorGUILayout.LabelField("Current Action: ", agent.gameObject.GetComponent<Agent>().currentAction.ToString());


        //AGENT GOALS
        GUILayout.Label("Agents Goals: ");

        //Displays each of the agents goals
        foreach (KeyValuePair<SubGoal, int> goal in agent.gameObject.GetComponent<Agent>().goalsDictionary)
        {
            EditorGUILayout.BeginVertical("box");
            foreach (KeyValuePair<string, int> subGoal in goal.Key.sGoals)
            {
                GUILayout.Label(subGoal.Key);
            }
            EditorGUILayout.EndVertical();
        }



        EditorGUILayout.Space(25);


        //Displays the current action plan path the agent is executing
        GUI.color = Color.green;
        EditorGUILayout.LabelField("Current Action Plan: ", EditorStyles.boldLabel);

        foreach (var s in agent.GetComponent<Agent>().actionPlan)
        {
            EditorGUILayout.LabelField(s.actionName);
        }
        GUI.color = Color.white;




        EditorGUILayout.Space(50);



        //Displays all the Actions that the agent has avaiable 
        EditorGUILayout.LabelField("List Of Possible Actions:", EditorStyles.boldLabel);
        foreach (Action action in agent.gameObject.GetComponent<Agent>().actions)
        {
            EditorGUILayout.BeginVertical("box");
            string requiredConditions = "";
            string postCondtions = "";


            //gets the required conditions of this action
            if (action.requiredConditionsDictionary.Count > 0)
                foreach (KeyValuePair<string, int> requiredCondition in action.requiredConditionsDictionary)
                {
                    requiredConditions += requiredCondition.Key + ", ";
                }
            //If no required condtions for this action display "None"
            else
                requiredConditions = "None";

            //gets the postCondtions of this action
            if (action.postConditionsDictionary.Count > 0)
                foreach (KeyValuePair<string, int> effect in action.postConditionsDictionary)
                {
                    postCondtions += effect.Key + ", ";
                }
            else
                //If no post condtions for this action display "None"
                postCondtions = "None";

            //displays the name of the action
            if(action.actionName != "")
            EditorGUILayout.LabelField("Action Name: ", action.actionName);

            //displays the cost of the action
            if (action.costOfAction.ToString() != null)
                EditorGUILayout.LabelField("Action Cost: ", action.costOfAction.ToString());


            //displays the target of the agent
            if (action.target != null)
                EditorGUILayout.LabelField("tag Of Destination Target: ", action.target.name);

            //displays the required condtions for this action
            if (requiredConditions != null)
                EditorGUILayout.LabelField("Required Conditions: ", requiredConditions);

            //displays the post condtions for this action
            if (postCondtions != null)
                EditorGUILayout.LabelField("Effects On Completion: ", postCondtions);

            EditorGUILayout.EndVertical();
        }



        EditorGUILayout.Space(25);



        //PERSONAL STATES
        GUI.color = Color.green;
        EditorGUILayout.LabelField(" Agent Personal States: ", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical("box");
        //if no personal state display "Agent has no current personal states"
        if (agent.gameObject.GetComponent<Agent>().agentPersonalState.GetStates().Count <= 0)
            EditorGUILayout.LabelField("Agent has no current personal states");
        else
        {
            //display each personal state the agent has
            foreach (KeyValuePair<string, int> personalState in agent.gameObject.GetComponent<Agent>().agentPersonalState.GetStates())
            {
                EditorGUILayout.LabelField(personalState.Key);
            }
        }
        EditorGUILayout.EndVertical();



        //INVENTORY
        EditorGUILayout.LabelField("Agent Inventory: ", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical("box");
        //if no items in inventory display "Agent has no current items in inventory"
        if (agent.gameObject.GetComponent<Agent>().inventory.objectsInInventoryList.Count <= 0)
            EditorGUILayout.LabelField("Agent has no current items in inventory");
        else
        {
            //displays each item inside of inventory list
            foreach (GameObject gameObject in agent.gameObject.GetComponent<Agent>().inventory.objectsInInventoryList)
            {
                EditorGUILayout.LabelField(gameObject.tag);
            }
        }
        EditorGUILayout.EndVertical();


        //WORLD STATES
        EditorGUILayout.LabelField("Current World States: ", EditorStyles.boldLabel);

        //displays each world state in scene.
        foreach (KeyValuePair<string, int> s in World.Instance.GetWorldStates().GetStates())
        {
            EditorGUILayout.LabelField(s.Key + "  " + s.Value.ToString());
        }

        EditorGUILayout.EndScrollView();

    }
}
