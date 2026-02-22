using System.Collections;
using UnityEngine;

public class PlayDialogueOnInteract : MonoBehaviour
{
    public DialogueClass[] iDialogue;

    public int totalLines = 1;
    [SerializeField] private int lineNum = 0;
    [SerializeField] private int linesToPlay = 0;

    public bool oneTimeDialogue = true;
    public bool dialogueIsPlaying;

    private ItemInteract interact;

    bool canReduce = false;
    bool moreDialogue;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        linesToPlay = totalLines;
        interact = GetComponent<ItemInteract>();
        if (totalLines > 1)
        {
            moreDialogue = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moreDialogue)
        {
            
        }
    }

    public void PlayDialogue()
    {
        DialogueManager.instance.PlayDialogue(iDialogue[lineNum].clip);
        DialogueManager.instance.SetSubtitle(iDialogue[lineNum].text, iDialogue[lineNum].clip.length);
        canReduce = true;
        dialogueIsPlaying = true;

        StartCoroutine(PlaySequentialLines());
    }

    IEnumerator PlaySequentialLines()
    {
        yield return new WaitForSeconds(iDialogue[lineNum].clip.length);
        UpdateLines();
        canReduce = false;
        if (linesToPlay > 0)
        {
            PlayDialogue();
        }
    }

    public void UpdateLines()
    {
        if (canReduce)
        lineNum += 1;
        linesToPlay -= 1;
    }

        public void ResetDialogue()
    {
        linesToPlay = totalLines;
        lineNum = 0;
    }

    public IEnumerator WaitUntilLastLine()
    {
        yield return new WaitUntil(() => linesToPlay <= 0);
        Debug.Log("Last Line Finished");
        dialogueIsPlaying = false;
        ResetDialogue();
        interact.ResetWhenLinesOver();
    }
}
