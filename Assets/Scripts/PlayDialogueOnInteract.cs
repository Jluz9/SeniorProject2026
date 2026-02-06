using UnityEngine;

public class PlayDialogueOnInteract : MonoBehaviour
{
    [SerializeField] ItemInteract interact;
    
    public AudioClip dialogue;

    public string subtitleText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (interact.playDialogueOnInteract == true)
        {
            DialogueManager.instance.PlayDialogue(dialogue);
            DialogueManager.instance.SetSubtitle(subtitleText, dialogue.length);
            interact.playDialogueOnInteract = false;
        }
    }
}
