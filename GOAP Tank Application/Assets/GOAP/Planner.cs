using System.Collections.Generic;

namespace GOAP
{
    public class Node
    {
        //the parent node this node is connected to
        public Node _parentNode;
        //cost to get to node
        public float _cost;
        //the state  by the time the action assigned to this node is achieved
        public Dictionary<string, int> _stateDictionary;
        //the action of this node
        public Action _action;

        // Constructor
        public Node(Node parent, float cost, Dictionary<string, int> allStates, Action action)
        {
            _parentNode = parent;
            _cost = cost;
            _stateDictionary = new Dictionary<string, int>(allStates);
            _action = action;
        }

        // Overloaded Constructor
        public Node(Node parent, float cost, Dictionary<string, int> allStates, Dictionary<string, int> personalStates, Action action)
        {
            _parentNode = parent;
            _cost = cost;
            _stateDictionary = new Dictionary<string, int>(allStates);

            //as well as the world states add the agents beliefs as states that can be
            //used to match preconditions
            foreach (KeyValuePair<string, int> b in personalStates)
            {
                if (!_stateDictionary.ContainsKey(b.Key))
                {
                    _stateDictionary.Add(b.Key, b.Value);
                }
            }
            _action = action;
        }
    }

    public class GPlanner
    {
        public Queue<Action> plan(List<Action> actionList, Dictionary<string, int> goalDictionary, WorldStates agentPersonalStates)
        {
            //creates a list to store possible paths into
            List<Node> pathsThatReturnSuccessList = new List<Node>();
            Node start = new Node(null, 0.0f, World.Instance.GetWorldStates().GetStates(), agentPersonalStates.GetStates(), null);

            //pass the first node through to start branching out the graph of plans from
            bool success = BuildGraph(start, pathsThatReturnSuccessList, actionList, goalDictionary);

            //if a plan wasn't found
            if (!success)
            {
                return null;
            }

            //if all the plans found, find the one that's cheapest to execute
            //and use that
            Node cheapestPath = null;
            foreach (Node path in pathsThatReturnSuccessList)
            {
                //if didnt find a cheaper path cheapest path = path
                if (cheapestPath == null)
                {
                    cheapestPath = path;
                }
                //if found a cheaper path cheapest = path
                else if (path._cost < cheapestPath._cost)
                {
                    cheapestPath = path;
                }
            }


            //list to store our final path
            List<Action> resultingPlanList = new List<Action>();

            //insert cheapest path inside of the resulting plan list - returns null when reach end of node
            while (cheapestPath != null)
            {
                if (cheapestPath._action != null)
                {
                    resultingPlanList.Insert(0, cheapestPath._action);
                }
                cheapestPath = cheapestPath._parentNode;
            }

            //make a queue out of the actions in resulting plan list.
            Queue<Action> queue = new Queue<Action>();

            foreach (Action a in resultingPlanList)
            {
                //puts action on end of queue
                queue.Enqueue(a);
            }

            //our final queue.
            return queue;
        }

        private bool BuildGraph(Node parent, List<Node> leavesList, List<Action> actionsList, Dictionary<string, int> goalDictionary)
        {
            bool foundPath = false;

            //with all the actions
            foreach (Action action in actionsList)
            {
                //check their requiredConditions
                if (action.IsAhievableGiven(parent._stateDictionary))
                {
                    //get the state of the world if the parent node was executed
                    Dictionary<string, int> currentState = new Dictionary<string, int>(parent._stateDictionary);

                    //add the postConditions of this node to the nodes stateDictionary to reflect what
                    //the world would look like if this node's action were executed
                    foreach (KeyValuePair<string, int> eff in action.postConditionsDictionary)
                    {
                        if (!currentState.ContainsKey(eff.Key))
                        {
                            currentState.Add(eff.Key, eff.Value);
                        }
                    }

                    //create the next node in the branch and set this current node as the parent
                    Node node = new Node(parent, parent._cost + action.costOfAction, currentState, action);

                    //if the current state of the world after doing this node's action is the goal
                    //this plan will achieve that goal and will become the agent's plan
                    if (GoalAchieved(goalDictionary, currentState))
                    {
                        leavesList.Add(node);
                        foundPath = true;
                    }
                    else
                    {
                        //if no goal has been found branch out to add other actions to the plan
                        List<Action> newActionList = NewUseableActionList(actionsList, action);
                        bool found = BuildGraph(node, leavesList, newActionList, goalDictionary);

                        if (found)
                        {
                            foundPath = true;
                        }
                    }
                }
            }
            return foundPath;
        }

        //remove used actions from a list of actions
        private List<Action> NewUseableActionList(List<Action> useableActionList, Action ActionToRemove)
        {
            List<Action> newActionList = new List<Action>();
            //makes a new list from useableAction list without the actionToRemove inside of it.
            foreach (Action a in useableActionList)
            {
                if (!a.Equals(ActionToRemove))
                {
                    newActionList.Add(a);
                }
            }
            return newActionList;
        }

        //check goals against state of the world to determine if the goal has been achieved.
        private bool GoalAchieved(Dictionary<string, int> goalDictionary, Dictionary<string, int> stateDictionary)
        {
            //loop through each of the goals
            foreach (KeyValuePair<string, int> g in goalDictionary)
            {
                if (!stateDictionary.ContainsKey(g.Key))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
