using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class ConsumeItem : MonoBehaviour
{
    [Header("Hunger")]
    public HungerManager hungerManager;

    [Header("SFX")]
    public AudioClip eatSFX;
    public AudioSource speaker;

    private bool _canInteract = false;
    private bool canConsume = false;
    private float heal = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        heal = hungerManager.hunger;
        //Debug.Log("Heal amount: " + heal);
        if(_canInteract && Keyboard.current.eKey.isPressed)
        {
            canConsume = true;
            _canInteract = false;
        }
    }

        void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _canInteract = true;
            StartCoroutine (WaitUntilInteract());

            if (canConsume)
            {
                canConsume = false;
            }
        }
    }

        IEnumerator WaitUntilInteract()
    {
        yield return new WaitUntil(() => canConsume == true);
        hungerManager.hunger += hungerManager.maxHunger-heal;
        if(speaker.isPlaying)
        {
            speaker.Stop();
        }
        speaker.PlayOneShot(eatSFX);
        canConsume = false;
        Destroy(gameObject);
    }
}
