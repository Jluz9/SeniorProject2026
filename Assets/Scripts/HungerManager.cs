using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class HungerManager : MonoBehaviour
{
    [Header ("Hunger")]
    [HideInInspector] public float hunger = 60.0f;
    public float maxHunger = 60.0f;
    public Text hungerDebug;
    
    private Animator redAnimator;
    public GameObject redScreen;

    [Header ("SFX")]
    public AudioClip hungerSFX;
    public AudioClip starveSFX;
    public AudioSource speaker;

    private bool hungerCanPlay = true;
    private bool starveCanPlay = true;
    private bool canFadeOut = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        redAnimator = redScreen.GetComponent<Animator>();

        hunger = maxHunger;
    }

    // Update is called once per frame
    void Update()
    {
        if (hunger > 0)
        {
            hunger -= Time.deltaTime;
        }
        else
        {
            
        }
        
        if (hunger <= 60)
        {
            PlayHungerSFX();
        }

        if (hunger <= 30)
        {
            redAnimator.enabled = true;
            redAnimator.SetBool("FadeIn", true);
            canFadeOut = true;

            PlayStarveSFX();
        }
        else if (hunger > 30)
        {
            redAnimator.SetBool("FadeIn", false);

            if (speaker.isPlaying == true)
            {
                speaker.Stop();
            }

            if (canFadeOut)
            {
                StartCoroutine(WaitUntilAnimDone());
            }
        }

        hungerDebug.text = ("Hunger: " + hunger);
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

    IEnumerator WaitUntilAnimDone()
    {
        yield return new WaitForSeconds(15);
        redAnimator.enabled = false;
        canFadeOut = false;
    }
}
