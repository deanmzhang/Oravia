using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAttack : Attack
{
    [SerializeField]
    [Tooltip("How long the lightning bolt lasts.")]
    private float timeToDestruction;

    [SerializeField]
    [Tooltip("Thunder SFX")]
    private AudioClip thunderClip;

    private string target = "";
    private float upperBound;
    private float objectHeight;
    private AudioSource thunder;
    //Jeff code
    public GameObject visual;
    //private float boundaryWidth;

    private void Awake()
    {
        thunder = gameObject.AddComponent<AudioSource>();
        thunder.playOnAwake = false;
        thunder.volume = .64f;
        thunder.clip = thunderClip;

        upperBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z - Camera.main.transform.position.z)).y;
        Debug.Log("Upper Bound: " + upperBound);
        //boundaryWidth = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z)).x;
        float colliderHeight = this.gameObject.GetComponent<Collider>().bounds.size.y;
        objectHeight =  colliderHeight / 2;
        Debug.Log("Object Height: " + objectHeight + "; Collider Height: " + colliderHeight);
        transform.position = new Vector3(transform.position.x, upperBound - objectHeight, transform.position.z);
        Debug.Log("upperBound - objectHeight: " + (upperBound - objectHeight));
        Debug.Log("Position of lightning: " + transform.position);
        //Jeff code
        GameObject light = (GameObject)Instantiate(visual, transform.position, Quaternion.identity);
        Destroy(this.gameObject, timeToDestruction);
        //Jeff code
        Destroy(light.gameObject, timeToDestruction);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(target))
        {
            switch (target)
            {
                case "Player":
                    collision.gameObject.GetComponent<Player>().TakeDamage(damage);
                    break;
                case "Enemy":
                    collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                    break;
            }
        }
    }

    public override void UseAttack(Vector3 spawnPos, string target)
    {
        this.target = target;
        thunder.Play();
    }
}
