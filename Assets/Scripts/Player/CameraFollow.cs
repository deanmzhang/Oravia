using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region camera_variables
    public Transform target;
    public PlayerController player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private Vector3 currentVelocity = Vector3.zero;
    public float yPosition = 17.72189f;
    public float leftBorder;
    public float rightBorder;
    public float topBorder;
    public bool horizontalLock;
    private Vector3 desiredPosition;
    private Vector3 smoothedPosition;
    // where we start the boss fight
    public float startBossArea;
    // so the camera does not go below ground
    public float groundLevel;
    // max height when you get to the mountain
    public float peak;

    public GameObject boss;
    public GameObject lightningWhenBossEntered;
    public GameObject bossHealth;
    public GameObject bossMusic;
    public AudioClip backgroundMusicClip;
    //public GameObject levelMusic;
    private bool levelMusicStopped;
    private bool bossEntered;
    private AudioSource music;
    #endregion

    #region Unity_functions
    private void Start()
    {
        levelMusicStopped = false;
        bossEntered = false;
        GameObject levelMusic = GameObject.Find("Background Music");
        if (levelMusic == null)
        {
            levelMusic = (GameObject)Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
            levelMusic.name = "Background Music";
            music = levelMusic.AddComponent<AudioSource>();
            music.playOnAwake = false;
            music.volume = .266f;
            music.clip = backgroundMusicClip;
            music.loop = true;
            music.Play();
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            if (target.position.x >= startBossArea)
            {
                if (!bossEntered)
                {
                    boss.SetActive(true);
                    
                    StartCoroutine(BossEnter(boss));
                    bossHealth.SetActive(true);
                    bossMusic.SetActive(true);
                    bossEntered = true;
                    
                }
                
                if (!levelMusicStopped)
                {
                    GameObject levelMusic = GameObject.Find("Background Music");
                    if (levelMusic != null)
                    {
                        AudioSource audioSource = levelMusic.GetComponent<AudioSource>();
                        StartCoroutine(FadeAudio(audioSource, 1));
                    }  
                }
                //offset.z = -125f;
            }
            desiredPosition = target.position + offset;
            smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed);
            if (smoothedPosition.x >= startBossArea)
            {
                horizontalLock = false;
                player.maxHeight = peak;
            }
            if (horizontalLock)
            {
                smoothedPosition.y = yPosition;
            }
            if( smoothedPosition.y > topBorder)
            {
                smoothedPosition.y = topBorder;
            }
            if (smoothedPosition.y < groundLevel)
            {
                smoothedPosition.y = groundLevel;
            }
            if (smoothedPosition.x < leftBorder)
            {
                smoothedPosition.x = leftBorder;
            }
            if (smoothedPosition.x > rightBorder)
            {
                smoothedPosition.x = rightBorder;
            }
            transform.position = smoothedPosition;
        }
    }
   


    IEnumerator FadeAudio(AudioSource audioSource, float duration)
    {
        {
            float currentTime = 0;
            float start = audioSource.volume;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, 0, currentTime / duration);
                yield return null;
            }
            yield break;
        }
    }

    IEnumerator BossEnter(GameObject boss)
    {
        
        while (boss.transform.position.z < 0)
        {
            boss.transform.position += new Vector3(1.2f, 0, 1.2f);
            yield return null;
        }
        lightningWhenBossEntered.SetActive(true);
        boss.GetComponent<AudioSource>().Play();

    }
}

#endregion
