using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrigger : MonoBehaviour
{
    public float damage;
    public float strength = 900;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(collision.gameObject.GetComponent<Rigidbody>().transform.forward * strength * -1, ForceMode.Impulse);
        }
    }
}
