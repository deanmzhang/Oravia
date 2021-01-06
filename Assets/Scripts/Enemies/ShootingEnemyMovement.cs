using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyMovement : MonoBehaviour
{
    #region tracking_variables
    // the player we want to chase
    Transform player;
    // body of the attached enemy
    Rigidbody EnemyRB;
    // the minimum distance we want for the enemy to start shooting
    [SerializeField]
    int range;
    //whether we are in range
    private bool inRange;
    //the projectile to shoot
    public Rigidbody projectile;
    public float projectileForce;
    #endregion

    #region Unity_functions
    // Start is called before the first frame update
    void Start()
    {
        EnemyRB = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").transform;
        float rand = Random.Range(1.0f, 2.0f);
        InvokeRepeating("Shoot", 2, rand);
    }

    // Update is called once per frame
    void Update()
    {
        inRange = Vector3.Distance(transform.position, player.position) <= range;
        if (inRange)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - player.position);
            transform.LookAt(player);
        } else
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }
    #endregion

    #region attack_functions
    private void Shoot()
    {
        if (inRange)
        {
            Rigidbody bullet = (Rigidbody) Instantiate(projectile, transform.position + transform.forward, transform.rotation);
            bullet.AddForce(transform.forward * projectileForce, ForceMode.Impulse);

            Destroy(bullet.gameObject, 2);
        }
    }
    #endregion

}
