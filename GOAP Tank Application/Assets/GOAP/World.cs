using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GOAP
{
    public class ResourceQueue
    {
        public Queue<GameObject> _queue = new Queue<GameObject>();
        public string _tag;
        public string _state;


        public ResourceQueue resourceQueue;

        public ResourceQueue(string tag, string stateName, WorldStates worldStates)
        {
            _tag = tag;
            _state = stateName;
            if (_tag != "")
            {

                GameObject[] resources = GameObject.FindGameObjectsWithTag(_tag);

                foreach (GameObject gameObject in resources)
                {
                    _queue.Enqueue(gameObject);
                }
            }

            if (_state != "")
            {
                worldStates.ModifyState(_state, _queue.Count);
            }
        }

        // Add the resource
        public void AddResource(GameObject r)
        {
            _queue.Enqueue(r);
        }


        // Remove the resource
        public GameObject RemoveAndReturnResource()
        {

            if (_queue.Count == 0)
                return null;

            return _queue.Dequeue();
        }

        // RemoveResource Obj
        public void RemoveResource(GameObject r)
        {
            // Put everything in a new queue except 'r' and copy it back to que
            _queue = new Queue<GameObject>(_queue.Where(p => p != r));
        }
    }



    public sealed class World
    {
        public static World Instance { get; } = new World();

        // Our world states
        private static WorldStates worldStates;

        // Storage for all resources
        private static Dictionary<string, ResourceQueue> resourcesDictionary = new Dictionary<string, ResourceQueue>();

        private static ResourceQueue Queue;


        //constructor only ever called once when script compiles
        static World()
        {
            //Create our worldStates
            worldStates = new WorldStates();
        }

        public World()
        {

        }

        //Helper function to add resource to a queue and dictionary
        public void AddResourceQueue(string tag, string stateName, WorldStates worldStates)
        {
            if (resourcesDictionary.ContainsKey(stateName))
            {
                Debug.Log(stateName + " already exists");
                return;
            }
            Queue = new ResourceQueue(tag, stateName, worldStates);
            resourcesDictionary.Add(stateName, Queue);
        }


        //Helper function to get the Queue dictionary of the paramater key passed in
        public ResourceQueue GetQueue(string type)
        {
            return resourcesDictionary[type];
        }

        //Getter for worldStates
        public WorldStates GetWorldStates()
        {
            return worldStates;
        }
    }
}
