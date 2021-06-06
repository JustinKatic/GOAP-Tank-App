using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    //make the dictionary elements their own serializable class
    //so we can edit them in the inspector
    [System.Serializable]
    public class WorldState
    {
        [Tooltip("Name of the condition")]
        public string action;
        [Tooltip("Value of the condition")]
        public int value;
    }

    public class WorldStates
    {
        public Dictionary<string, int> worldStatesDictionary;

        // Constructor
        public WorldStates()
        {
            worldStatesDictionary = new Dictionary<string, int>(); //dictionary to hold world states.
        }
        //Getter to get access statesDictionary
        public Dictionary<string, int> GetStates()
        {
            return worldStatesDictionary;
        }

        // Check if state given exists in states dictionary
        public bool HasState(string state)
        {
            return worldStatesDictionary.ContainsKey(state);
        }



        // Add a state to states dictionary
        private void AddState(string state, int value)
        {
            worldStatesDictionary.Add(state, value);
        }

        public void AddPersonalState(string state)
        {
            if (!HasState(state))
                worldStatesDictionary.Add(state, 0);
        }


        //method to modify/add a state
        public void ModifyState(string state, int amountToAddOrSubtract)
        {
            // If states dictionary has state
            if (HasState(state))
            {
                // Add value passed in to states value
                worldStatesDictionary[state] += amountToAddOrSubtract;
                // If remaining amount is less than zero then remove it
                if (worldStatesDictionary[state] <= 0)
                {
                    // Call the RemoveState method
                    RemoveState(state);
                }
            }
            else
            {
                //Add state and amount to the statesDictionary.
                AddState(state, amountToAddOrSubtract);
            }
        }

        // Method to remove a state
        public void RemoveState(string state)
        {
            // Check if it frist exists
            if (HasState(state))
            {
                worldStatesDictionary.Remove(state);
            }
        }

        // Method to add/set a state directly
        public void SetState(string state, int amountOfState)
        {
            // Check if it exists
            if (HasState(state))
            {
                worldStatesDictionary[state] = amountOfState;
            }
            else
            {
                AddState(state, amountOfState);
            }
        }
    }
}
