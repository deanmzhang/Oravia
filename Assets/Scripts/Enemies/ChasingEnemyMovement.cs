using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemyMovement : Enemy
{
    #region Tracking_variables
    // the player we want to chase
    Transform player;
    Vector3 target;
    // body of the attached enemy
    Rigidbody EnemyRB;
    // the enemy
    //Enemy basicEnemy;
    // the minimum distance we want for the enemy to start chasing
    [SerializeField]
    int minDist;
    private bool facingRight = false;
    #endregion

    #region Unity_functions
    // Start is called before the first frame update
    void Start()
    {
        EnemyRB = GetComponent<Rigidbody>();
        //basicEnemy = GetComponent<Enemy>();
        //find the player in the scene
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= minDist)
        {
            if (player.transform.position.x < transform.position.x && facingRight)
                Flip();
            if (player.transform.position.x > transform.position.x && !facingRight)
                Flip();
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            gameObject.GetComponentInChildren<Animator>().SetBool("isMoving", true);
        }
        else gameObject.GetComponentInChildren<Animator>().SetBool("isMoving", false);
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Player>().TakeDamage(damageDealt * Time.deltaTime);
        }
    }
    #endregion

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.z *= -1;
        gameObject.transform.localScale = tmpScale;
    }
}
