using System.Collections;
using UnityEngine;

public class PlayDialogueOnInteract : MonoBehaviour
{
    public DialogueClass[] iDialogue;

    public int totalLines = 1;
    private int lineNum = 0;
    private int linesToPlay = 0;

    public bool oneTimeDialogue = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        linesToPlay = totalLines;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayDialogue()
    {
        if (linesToPlay > 0)
        {
            DialogueManager.instance.PlayDialogue(iDialogue[lineNum].clip);
            DialogueManager.instance.SetSubtitle(iDialogue[lineNum].text, iDialogue[lineNum].clip.length);

            StartCoroutine(PlaySequentialLines());
        }
    }

    IEnumerator PlaySequentialLines()
    {
        yield return new WaitForSeconds(iDialogue[lineNum].clip.length);
        lineNum++;
        linesToPlay--;
        PlayDialogue();
    }

        public void ResetDialogue()
    {
        linesToPlay = totalLines;
        lineNum = 0;
    }

    public IEnumerator WaitUntilLastLine()
    {
        yield return new WaitUntil(() => linesToPlay == 0);
        ResetDialogue();
    }
}
