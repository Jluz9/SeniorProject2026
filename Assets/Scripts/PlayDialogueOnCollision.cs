using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayDialogueOnCollision : MonoBehaviour
{
    public AudioClip[] dialogue;
    public string[] subtitleText;
    
    public int totalLines = 1;
    private int lineNum = 0;
    private int linesToPlay = 0;

    [SerializeField] bool oneTimeDialogue = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        linesToPlay = totalLines;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayDialogue();

            if (!oneTimeDialogue)
            {
                StartCoroutine(WaitUntilLastLine());
            }
        }
    }

    IEnumerator PlaySequentialLines()
    {
        yield return new WaitForSeconds(dialogue[lineNum].length);
        lineNum++;
        linesToPlay--;
        PlayDialogue();
    }

    public void PlayDialogue()
    {
        if (linesToPlay > 0)
        {
            DialogueManager.instance.PlayDialogue(dialogue[lineNum]);
            DialogueManager.instance.SetSubtitle(subtitleText[lineNum], dialogue[lineNum].length);

            StartCoroutine(PlaySequentialLines());
        }
    }

    public void ResetDialogue()
    {
        linesToPlay = totalLines;
        lineNum = 0;
    }

    IEnumerator WaitUntilLastLine()
    {
        yield return new WaitUntil(() => linesToPlay == 0);
        ResetDialogue();
    }
}
