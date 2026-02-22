using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemInteract : MonoBehaviour
{
    public GameObject interactUI;
    bool canInteract = false;

    public bool hasDialogue = false;
    [SerializeField] public bool canPlayDialogue = false;

    [SerializeField] PlayDialogueOnInteract dialogue;

    [HideInInspector] public Transform playerPos;
    public float minDist = 2f;
    private GameObject player;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        playerPos = player.GetComponent<Transform>();

        float dist = Vector3.Distance(playerPos.position, transform.position);

        if (dist <= minDist)
        {
            if (!hasDialogue)
            {
                canInteract = true;
            }
            else
            {
                if (!dialogue.dialogueIsPlaying)
                {
                    canInteract = true;
                }
                else
                {
                    canInteract = false;
                }
            }
        }
        else
        {
            canInteract = false;
        }

        if (canInteract)
        {
            interactUI.SetActive(true);

            if (hasDialogue)
            {
                StartCoroutine(WaitUntilInteract());
            }

            if (Mouse.current.leftButton.isPressed)
            {
                interactUI.SetActive(false);
                if (hasDialogue)
                {
                    canPlayDialogue = true;
                }
            }
        }

        if (!canInteract)
        {
            if (interactUI.activeSelf == true)
            {
                interactUI.SetActive(false);
            }

            if (hasDialogue)
            {
                canPlayDialogue = false;
            }
        }

        //Debug.Log(canPlayDialogue);
    }

    public void ResetWhenLinesOver()
    {
        if (!dialogue.oneTimeDialogue)
        {
            canInteract = true;
        }
        else
        {
            Destroy(this);
        }
    }

        /*if (canInteract && Keyboard.current.eKey.isPressed)
        {
            interactUI.SetActive(false);

            if(hasDialogue)
            {
                canInteract = false;
                canPlayDialogue = true;
            }
        }*/

    /*void OnTriggerEnter (Collider other)
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
    }*/

    IEnumerator WaitUntilInteract()
    {
        yield return new WaitUntil(() => canPlayDialogue == true);
        dialogue.PlayDialogue();  
        if (!dialogue.oneTimeDialogue)
        {
            StartCoroutine(dialogue.WaitUntilLastLine());
        }
        canInteract = false;
        canPlayDialogue = false;
    }
}
