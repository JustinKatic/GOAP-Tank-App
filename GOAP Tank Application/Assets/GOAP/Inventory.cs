using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public class Inventory
    {
        // Store our items in a List
        public List<GameObject> objectsInInventoryList = new List<GameObject>();

        // Method to add items to our list
        public void AddItem(GameObject objToAdd)
        {
            objectsInInventoryList.Add(objToAdd);
        }

        public GameObject FindItemWithTag(string tag)
        {
            // Iterate through all the items
            foreach (GameObject i in objectsInInventoryList)
            {
                // Check i isn't null.  If it is then break
                if (i == null) break;
                // Found a match
                if (i.tag == tag)
                {
                    return i;
                }
            }
            // Nothing found
            return null;
        }

        // Remove an item from our list
        public void RemoveItem(GameObject objToRemove)
        {
            int indexToRemove = -1;

            // Search through the list to see if it exists
            foreach (GameObject gameObject in objectsInInventoryList)
            {
                //set indexToRemove to 0.
                indexToRemove++;
                // Have we found it?
                if (gameObject == objToRemove)
                {
                    break;
                }
            }
            // Do we have something to remove?
            if (indexToRemove >= -1)
            {
                // Yes we do. remove the item at indexToRemove
                objectsInInventoryList.RemoveAt(indexToRemove);
            }
        }
    }
}