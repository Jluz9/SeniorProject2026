using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenDoor : MonoBehaviour
{
public GameObject door;
public GameObject leftDoor;

bool canOpen = false;
bool isOpen =false;
public bool doubleDoor = false;

Animator doorAnimator;
Animator leftDoorAnimator;

void Start()
    {
        doorAnimator = door.GetComponent<Animator>();
        leftDoorAnimator = leftDoor.GetComponent<Animator>();
    }

void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag ("Player"))
        {
            canOpen = true;
            doorAnimator.enabled = true;
            leftDoorAnimator.enabled = true;
        }
    }

void OnTriggerStay (Collider other)
    {
        if (other.gameObject.CompareTag ("Player") && canOpen == true)
        {
            if (Mouse.current.leftButton.isPressed && isOpen == false)
            {
                canOpen = false;
                isOpen = true;
                doorAnimator.SetBool("IsOpen", true);
                
                if (doubleDoor)
                {
                    leftDoorAnimator.SetBool("IsOpen", true);
                }
            }
            else if (Mouse.current.leftButton.isPressed && isOpen == true)
            {
                canOpen = false;
                isOpen = false;
                doorAnimator.SetBool("IsOpen", false);
                
                if (doubleDoor)
                {
                    leftDoorAnimator.SetBool("IsOpen", false);
                }
            }
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.CompareTag ("Player"))
        {
            StartCoroutine(WaitForAnim());
        }

        IEnumerator WaitForAnim()
        {
            yield return new WaitForSeconds(0.45f);
            doorAnimator.enabled = false;
            leftDoorAnimator.enabled = false;
        }
    }
}
