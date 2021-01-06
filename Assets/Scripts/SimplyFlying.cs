using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplyFlying : MonoBehaviour
{
    private bool isMoving;
    private float speed = 7f;
    private float x_min = 300f;
    private float x_max = 350f;
    private float y_min = 38f;
    private float y_max = 50f;
    private float x_chosen;
    private float y_chosen;

    public static bool started;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        started = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving & !started)
        {
            isMoving = true;
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        x_chosen = Random.Range(x_min, x_max);
        y_chosen = Random.Range(y_min, y_max);
        transform.position = new Vector3(130, y_chosen);  
        while (transform.position.x < x_chosen)
        {
            transform.position += speed * Time.fixedDeltaTime * new Vector3(1.5f, 0, 0);
            yield return null;
        }
        isMoving = false;
    }
}
