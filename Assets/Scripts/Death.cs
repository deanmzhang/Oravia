using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //destroy dontdestroyonload
        GameObject overallScore = GameObject.Find("OverallScore");
        if (overallScore != null)
        {
            Destroy(overallScore);
        }

        GameObject music = GameObject.Find("Background Music");
        if (music != null)
        {
            Destroy(music);
        }

        GameObject thunder = GameObject.Find("ThunderSound");
        if (thunder != null)
        {
            Destroy(thunder);
        }
    }

}
