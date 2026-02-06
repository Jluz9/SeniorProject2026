using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class ItemInteract : MonoBehaviour
{
    public GameObject interactUI;

    public bool hasDialogue = false;
    [HideInInspector] public bool playDialogueOnInteract = false;

    void Start ()
    {

    }

    void Update ()
    {

    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactUI.SetActive(true);
        }
    }

    //Interact UI enabled if player is within distance of an interactable
    void OnTriggerStay (Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Keyboard.current.eKey.isPressed)
        {
            Debug.Log("Interacted with an object.");

            if (hasDialogue)
            {
                playDialogueOnInteract = true;
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
}
