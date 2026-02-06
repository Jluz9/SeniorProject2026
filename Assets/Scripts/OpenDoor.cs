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

//public float openSpeed = 1;
//float openRate = 0;

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
            if (Keyboard.current.eKey.isPressed && isOpen == false)
            {
                //door.transform.Rotate(0,-80 * openSpeed * Time.deltaTime, 0);
                canOpen = false;
                isOpen = true;
                doorAnimator.SetBool("IsOpen", true);
                
                if (doubleDoor)
                {
                    leftDoorAnimator.SetBool("IsOpen", true);
                }
            }
            else if (Keyboard.current.eKey.isPressed && isOpen == true)
            {
                //door.transform.Rotate(0,80 * openSpeed * Time.deltaTime, 0);
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
            doorAnimator.enabled = false;
            leftDoorAnimator.enabled = false;
        }
    }
}
