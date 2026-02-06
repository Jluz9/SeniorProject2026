using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayDialogueOnCollision : MonoBehaviour
{
    public AudioClip dialogue;

    public string subtitleText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DialogueManager.instance.PlayDialogue(dialogue);
            DialogueManager.instance.SetSubtitle(subtitleText, dialogue.length);
        }
    }
}
