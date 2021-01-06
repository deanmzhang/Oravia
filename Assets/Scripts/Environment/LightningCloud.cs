using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningCloud : MonoBehaviour
{
    // the player
    Transform player;

    //the projectile to shoot
    public GameObject projectile;

    //lightning strike visual
    public GameObject visual;

    public float range;

    private bool inRange;

    private bool isSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        inRange = Vector3.Distance(transform.position, player.position) <= range;
        if (inRange)
        {
            if (!isSpawning)
            {
                float rand = Random.Range(3.0f, 5.0f);
                InvokeRepeating("Strike", 2.0f, rand);
                isSpawning = true;
            }
        } else
        {
            CancelInvoke();
            isSpawning = false;
        }
    }

    private void Strike()
    {
        GameObject lightning = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
        GameObject light = (GameObject)Instantiate(visual, transform.position, Quaternion.identity);


        Destroy(lightning.gameObject, 1f);
        Destroy(light.gameObject, 1f);
    }
}
