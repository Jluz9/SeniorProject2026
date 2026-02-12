using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class ItemInteract : MonoBehaviour
{
    public GameObject interactUI;

    public bool hasDialogue = false;
    bool canPlayDialogue = false;
    bool canInteract = false;

    [SerializeField] PlayDialogueOnInteract dialogue;

    void Start ()
    {

    }

    void Update ()
    {
        if (canInteract && Keyboard.current.eKey.isPressed)
        {
            Debug.Log("Interacted with an object.");
            canPlayDialogue = true;
            canInteract = false;
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
