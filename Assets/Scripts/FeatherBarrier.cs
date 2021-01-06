using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeatherBarrier : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("Player") && collider.transform.GetComponent<PlayerController>().numFeathers < 10)
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            Debug.Log("You don't have enough feathers yet! Please collect at least 10 feathers in order to fight the boss");
        } 
        if (collider.transform.CompareTag("Player") && collider.transform.GetComponent<PlayerController>().numFeathers >= 10)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }
}
