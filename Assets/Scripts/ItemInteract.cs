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
            interactUI.SetActive(false);

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
            canInteract = true;

            if (hasDialogue)
            {
                StartCoroutine(WaitUntilInteract());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactUI.SetActive(false);
            canInteract = false;
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
