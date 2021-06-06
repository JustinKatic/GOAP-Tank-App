using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject VFX;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            VFX.SetActive(true);
            Destroy(gameObject, .2f);
        }
        else if (other.gameObject.tag == "Tank")
        {
            VFX.SetActive(true);
            other.GetComponent<TankHealth>().TankHurt(1);
            Destroy(gameObject, .2f);
        }
    }
}
