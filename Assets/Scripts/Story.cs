using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{

    [SerializeField]
    [Tooltip("UI to hide")]
    private GameObject Canvas;

    [SerializeField]
    [Tooltip("Birds Group")]
    private GameObject BirdsGroup;

    [SerializeField]
    [Tooltip("Birds Group Dark")]
    private GameObject BirdsGroupDark;

    [SerializeField]
    [Tooltip("Birds Group Corrupted")]
    private GameObject BirdsGroupCorrupted;

    [SerializeField]
    [Tooltip("Non Corrupted Bird")]
    private GameObject NonCorrupted;

    [SerializeField]
    [Tooltip("Camera")]
    private GameObject Camera;

    [SerializeField]
    [Tooltip("Boss")]
    private GameObject Boss;

    [SerializeField]
    [Tooltip("Bright Env")]
    private GameObject BrightEnv;

    [SerializeField]
    [Tooltip("Dark Env")]
    private GameObject DarkEnv;

    [SerializeField]
    [Tooltip("Tornadoes")]
    private GameObject Tornadoes;

    [SerializeField]
    [Tooltip("Black")]
    private GameObject Black;

    [SerializeField]
    [Tooltip("Background Music")]
    private GameObject BackgroundMusic;

    [SerializeField]
    [Tooltip("Thunder Sound")]
    private GameObject ThunderSound;

    [SerializeField]
    [Tooltip("Thunder SFX")]
    private AudioClip ThunderClip;

    public static bool started;

    private bool skipped;
    private float speed = 7f;

    private bool zoomingOut;
    private bool birdsFlying;
    private bool bossFlying;
    private bool corrupting;
    private bool chasing;
    private bool escaping;
    private bool ending;

    private AudioSource thunder;
    private Animator anim;
    private Image BlackImage;

    void Awake()
    {
        thunder = gameObject.AddComponent<AudioSource>();
        thunder.playOnAwake = false;
        thunder.volume = .4f;
        thunder.clip = ThunderClip;

        anim = Black.GetComponent<Animator>();
        BlackImage = Black.GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        started = false;
        skipped = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (started)
        {
            zoomingOut = true;
            birdsFlying = false;
            bossFlying = false;
            corrupting = false;
            chasing = false;
            escaping = false;
            ending = false;
            
            StartCoroutine(PlayAnimation());
            started = false;
        }
        if (skipped)
        {
            SceneManager.LoadScene("IntroStory");
        }
    }

    IEnumerator PlayAnimation()
    {
        Canvas.SetActive(false);
        while (!skipped)
        {
            if (zoomingOut)
            {
                Camera.transform.position -= new Vector3(0, 0, 0.5f);
                if (Camera.transform.position.z <= -142)
                {
                    BirdsGroup.SetActive(true);
                    zoomingOut = false;
                    birdsFlying = true;
                    
                }
            }

            if (birdsFlying)
            {
                BirdsGroup.transform.position += speed * Time.fixedDeltaTime * new Vector3(1.5f, 0, 0);
                if (BirdsGroup.transform.position.x > 55)
                {
                    Boss.SetActive(true);
                    bossFlying = true;
                    
                    Animator animator = Camera.GetComponent<Animator>();
                    animator.enabled = !animator.enabled;
                    animator.SetBool("Shake", true);
                    thunder.Play();
                }
            }

            if (bossFlying)
            {
                Boss.transform.position += speed * Time.fixedDeltaTime * new Vector3(-24, -8, 0);
                if (Boss.transform.position.y <= 60)
                {
                    Tornadoes.SetActive(true);
                }
                    
                if (Boss.transform.position.y <= 30)
                {

                    Boss.SetActive(false);
                    BrightEnv.SetActive(false);
                    BirdsGroup.SetActive(false);
                    DarkEnv.SetActive(true);
                    Black.SetActive(true);
                    corrupting = true;
                    birdsFlying = false;
                    bossFlying = false;

                
                    yield return new WaitForSeconds(2.0f);

                }
            }

            if (corrupting)
            {
                Animator animator = Camera.GetComponent<Animator>();
                animator.SetBool("Shake", false);
                Tornadoes.SetActive(false);
                BirdsGroupDark.SetActive(true);
                
                corrupting = false;
                chasing = true;
                yield return new WaitForSeconds(2.0f);
            }

            if (chasing)
            {
                AudioSource audioSource = GetComponent<AudioSource>();
                StartCoroutine(FadeAudio(audioSource, 1));
                //StartCoroutine(FadeFromBlack());
                Black.SetActive(false);
                foreach (Transform child in BirdsGroupCorrupted.transform)
                {
                    child.eulerAngles = new Vector3(0, 90f, 0);
                }
                BirdsGroupCorrupted.transform.position += speed * Time.fixedDeltaTime * new Vector3(-1.5f, 0, 0);
                if (BirdsGroupCorrupted.transform.position.x <= 45)
                {
                    escaping = true;

                    BackgroundMusic.SetActive(true);
                    DontDestroyOnLoad(BackgroundMusic);
                }
            }

            if (escaping)
            {
                NonCorrupted.transform.eulerAngles = new Vector3(0, 90f, 0);
                NonCorrupted.transform.position += speed * Time.fixedDeltaTime * new Vector3(-2f, 0, 0);
                
                if (NonCorrupted.transform.position.x <= 150)
                {
                    ending = true;
                    ThunderSound.SetActive(true);
                    DontDestroyOnLoad(ThunderSound);
                }

            }

            if (ending)
            {
                Animator animator = Camera.GetComponent<Animator>();
                animator.SetBool("Shake", true);
                if (NonCorrupted.transform.position.x <= 130)
                {
                    skipped = true;
                    Black.SetActive(true);
                    //StartCoroutine(FadeToBlack());
                }
                
            }
            
            yield return null;
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

    IEnumerator FadeToBlack()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => BlackImage.color.a == 1);
        SceneManager.LoadScene("IntroStory");
    }

    IEnumerator FadeFromBlack()
    {
        anim.SetBool("Fade", false);
        yield return new WaitUntil(() => BlackImage.color.a == 0);
    }

}
