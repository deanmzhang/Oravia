using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoAttack : Attack
{
    private Vector3 endPosition;
    private string target = "";
    private Transform targetTranform;

    private void Awake()
    {
        //endPosition = transform.position - transform.forward * attackRange;
    }

    private void Update()
    {
        if (transform.position == endPosition)
        {
            Destroy(this.gameObject);
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, 10 * Time.deltaTime);
        }
        //Debug.Log("Direction of capsule: " + transform.forward);
        //Debug.Log("Current position: " + transform.position + ", End position: " + endPosition);
    }

    private void OnCollisionEnter(Collision collision)
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
            Destroy(this.gameObject);
        }
    }

    public override void UseAttack(Vector3 spawnPos, string target)
    {
        this.target = target;
        targetTranform = GameObject.FindWithTag(target).transform;
        endPosition = targetTranform.position;
    }
}
