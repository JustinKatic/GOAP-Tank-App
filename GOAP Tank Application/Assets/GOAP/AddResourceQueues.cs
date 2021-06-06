using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    [System.Serializable]
    public class ResourceData
    {
        [Tooltip("the tag of the object you want to put into a queue on awake can leave empty if dont want to add items to queue on awake")]
        public string TagOfQueueItem;
        [Tooltip("the name of the object that you refrence this queue with")]
        public string NameOfQueue;
    }

    public class AddResourceQueues : MonoBehaviour
    {
        public ResourceData[] worldResourcesInScene;
        

        //add each resource from the inspector into the resoourceQueue
        private void Awake()
        {
            foreach (ResourceData resource in worldResourcesInScene)
            {
                World.Instance.AddResourceQueue(resource.TagOfQueueItem, resource.NameOfQueue, World.Instance.GetWorldStates());
            }
        }
    }
}
