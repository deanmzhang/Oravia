using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkipIntroStory : MonoBehaviour
{
    public string fullText;
    public Text currText;
    [SerializeField]
    private string sceneNameToLoad;

    private bool skipped = false;
    private bool secondSkipped = false;
    public bool shown = false;

    private void Start()
    {
        skipped = false;
        secondSkipped = false;
        shown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!skipped)
                skipped = true;
            else secondSkipped = true;
        }

        if (secondSkipped)
        {
           // SceneManager.LoadScene(sceneNameToLoad);
        }

        if (skipped && !shown)
        {
            shown = true;
            currText.text = fullText;
            StartCoroutine(load(10));
        }
        
    }

    IEnumerator load(float time)
    {
        float countTime = time;
        while (countTime > 0)
        {
            Debug.Log(countTime);
            countTime -= Time.fixedDeltaTime;
            yield return null;
        }

        SceneManager.LoadScene(sceneNameToLoad);
    }
}
