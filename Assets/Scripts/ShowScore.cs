using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    public GameObject otherText;
    public float delayBeforeLoading = 10f;

    private float timeElapsed;
    private bool shifted = false;

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > delayBeforeLoading && !shifted)
        {
            otherText.GetComponent<RectTransform>().position += new Vector3(0, 50, 0);
            this.GetComponent<Text>().text = "Time Taken: ";
            GameObject overallScore = GameObject.Find("OverallScore");
            if (overallScore != null)
            {
                this.GetComponent<Text>().text += overallScore.GetComponent<OverallScore>().finalResult;
                Destroy(overallScore);
            }
            shifted = true;

            //destroy dontdestroyonload
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
}
