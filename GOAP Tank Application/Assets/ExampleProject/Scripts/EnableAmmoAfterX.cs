using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;

public class EnableAmmoAfterX : MonoBehaviour
{

    public GameObject objectToEnable;


    private void Start()
    {
        Invoke("ActivateObject", Random.Range(5, 15));
    }


    void ActivateObject()
    {
        if (!objectToEnable.activeSelf)
        {
            objectToEnable.SetActive(true);
            World.Instance.GetWorldStates().ModifyState("AmmoAvailable", 1);
        }
        Invoke("ActivateObject", Random.Range(30, 60));
    }
}
