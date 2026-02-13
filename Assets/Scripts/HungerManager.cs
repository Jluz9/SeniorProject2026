using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class HungerManager : MonoBehaviour
{
    [Header ("Hunger")]
    [HideInInspector] public float hunger = 60.0f;
    public float maxHunger = 60.0f;
    public Text hungerDebug;
    float tempTimeLeft;
    
    [Header ("VFX")]
    public GameObject redScreen;
    public float animLength = 30.0f;
    Image red;

    [Header ("SFX")]
    public AudioClip hungerSFX;
    public AudioClip starveSFX;
    public AudioSource speaker;

    private bool hungerCanPlay = true;
    private bool starveCanPlay = true;
    private bool canFadeOut = false;
    bool dead = false;
    SceneManager sceneManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //redAnimator = redScreen.GetComponent<Animator>();

        hunger = maxHunger;

        red = redScreen.GetComponent<Image>();
        sceneManager = GetComponent<SceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hunger > 0)
        {
            hunger -= Time.deltaTime;

            if (Keyboard.current.fKey.isPressed && hunger>60)
            {
                hungerDebug.enabled = true;
                hunger = 60.0f;
            }
        }
        else if (hunger <= 0)
        {
            sceneManager.Death();
            canFadeOut = false;
            dead = true;
            redScreen.SetActive(false);
            speaker.Stop();
        }
        
        if (hunger <= 60)
        {

            PlayHungerSFX();
        }

        if (hunger <= 30 && hunger > 0)
        {
            //redAnimator.enabled = true;
            //redAnimator.SetBool("FadeIn", true);
            StartCoroutine(FadeIn(red));
            Debug.Log ("Fade in started");
            canFadeOut = true;
            redScreen.SetActive(true);

            PlayStarveSFX();
        }
        else if (hunger > 30)
        {

            if (canFadeOut && !dead)
            {
                StartCoroutine(FadeOut(red));

                if (speaker.isPlaying)
                {
                    speaker.Stop();
                }
            }
        }

        hungerDebug.text = ("Hunger: " + hunger);

        //Debug.Log ("Time left in animation: " + tempTimeLeft);
    }

    void PlayHungerSFX()
    {
        if(!speaker.isPlaying && hungerCanPlay)
        {
            speaker.PlayOneShot(hungerSFX);
            hungerCanPlay = false;
        }
    }

        void PlayStarveSFX()
    {
        if(!speaker.isPlaying && starveCanPlay)
        {
            speaker.PlayOneShot(starveSFX);
            starveCanPlay = false;
        }
    }

    private YieldInstruction fadeInstruction = new YieldInstruction();
    IEnumerator FadeIn(Image image)
    {
        float elapsedTime = 0.0f;
        Color tempColor = image.color;
        while(elapsedTime < animLength && hunger < 30)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            tempColor.a = Mathf.Clamp01(elapsedTime / animLength);
            image.color = tempColor;
            tempTimeLeft = animLength - elapsedTime;
        }
    }

    IEnumerator FadeOut(Image image)
    {
        float elapsedTime = tempTimeLeft;
        Color tempColor = image.color;
        while(elapsedTime < animLength && hunger > 30)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            tempColor.a = 1.0f - Mathf.Clamp01(elapsedTime / animLength);
            image.color = tempColor;
        }
        canFadeOut = false;
        StartCoroutine(WaitUntilAnimDone());
    }

    IEnumerator WaitUntilAnimDone()
    {
        yield return new WaitForSeconds(animLength);
        redScreen.SetActive(false);
    }
}
