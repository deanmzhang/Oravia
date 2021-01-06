using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFeather : MonoBehaviour
{
    public AudioSource collectSoundEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            collectSoundEffect.Play();
            other.transform.GetComponent<PlayerController>().numFeathers += 1;
            ScoringSystem.currentFeathers += 1;
            Destroy(gameObject);
        }
    }
}
