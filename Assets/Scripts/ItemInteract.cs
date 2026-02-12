using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using NUnit.Framework;

public class ItemInteract : MonoBehaviour
{
    public GameObject interactUI;

    public bool hasDialogue = false;
    bool canPlayDialogue = false;
    [HideInInspector]
    bool canInteract = false;

    [SerializeField] PlayDialogueOnInteract dialogue;

    void Start ()
    {

    }

    void Update ()
    {
        if (canInteract && Keyboard.current.eKey.isPressed)
        {
            //Debug.Log("Interacted with an object.");

            if(hasDialogue)
            {
                canInteract = false;
                canPlayDialogue = true;
            }
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactUI.SetActive(true);

            if (hasDialogue)
            {
                canInteract = true;
                StartCoroutine(WaitUntilInteract());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactUI.SetActive(false);
        }
    }

    IEnumerator WaitUntilInteract()
    {
        yield return new WaitUntil(() => canPlayDialogue == true);
        canPlayDialogue = false;
        dialogue.PlayDialogue();  
            if (!dialogue.oneTimeDialogue)
            {
                StartCoroutine(dialogue.WaitUntilLastLine());
            }
    }
}
