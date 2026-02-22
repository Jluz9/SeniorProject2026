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

    public LayerMask layersToHit;
    float maxDistance = 100f;
    Ray ray;
    private bool isHitting = false;
    //GameObject player;

    void Start ()
    {

        //ray = new Ray(cam.transform.position, player.transform.forward);
        //player = GameObject.FindWithTag("Player"); 
    }

    void Update ()
    {
        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (canInteract && Mouse.current.leftButton.isPressed)
        {
            interactUI.SetActive(false);

            if(hasDialogue)
            {
                canInteract = false;
                canPlayDialogue = true;
            }
        }

        /*if (isHitting)
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green);

            if(hit.collider.CompareTag("Interactable"))
            {
                canInteract = true;
            }
        }
        else
        {
            if(canInteract)
            {
                canInteract = false;
            }
        }*/
       if (canInteract)
        {
            interactUI.SetActive(true);

            if (hasDialogue)
            {
                StartCoroutine(WaitUntilInteract());
            }
        }
        else
        {
            if (interactUI.activeSelf == true)
            {
                interactUI.SetActive(false);
            }
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CheckIfHitting();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CheckIfHitting();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canInteract = false;
        }
    }

    void CheckIfHitting()
    {
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, layersToHit, QueryTriggerInteraction.Ignore))
        {
            canInteract = true;
            Debug.Log("Hit Something");
            Debug.DrawLine(ray.origin, hit.point, Color.green);
        }
        else
        {
            canInteract = false;
            canPlayDialogue = false;
            
            Debug.Log("Not hitting anything");
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
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