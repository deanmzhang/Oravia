using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AftermathTextEffect : MonoBehaviour
{
    public float delay = 5f;
    public string[] fullText;
    private string currentText = "";

    private void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = -1; i < fullText.Length; i++)
        {
            if (i==-1) yield return new WaitForSeconds(delay);
            else
            {
                this.GetComponent<Text>().text += fullText[i] + '\n';
                yield return new WaitForSeconds(delay);
            }
            
        }
    }
}