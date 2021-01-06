using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Spawn_variables
    // the type of enemy we want to spawn (I can change this into a list if we 
    //  want to spawn multiple types eventually)
    public GameObject enemy;
    // random position on the x axis
    float randX;
    // random position on the y axis
    float randY;
    // where to spawn
    Vector3 spawnLocation;
    public float spawnRate = 2;
    float nextSpawn = 0;
    #endregion

    #region Unity_functions
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(nextSpawn + " " + Time.time);

        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            // where you decide the bounds of where you want to spawn
            // be careful for y too because it might be weird if we eventually 
            // put terrain in and whatnot like it could spawn in the ground
            randX = Random.Range(-10f, 10f); // could make these values settable in the inspector if we really want, just make floats for the bounds
            randY = Random.Range(-10f, 10f);
            spawnLocation = new Vector3(transform.position.x + randX, transform.position.y + randY, -63.0f);
            //Debug.Log(spawnLocation);
            Instantiate(enemy, spawnLocation, Quaternion.identity);
        }
    }
    #endregion
}
