using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    AudioSource dialogueSpeaker;
    
    public static DialogueManager instance;
    
    [SerializeField] TextMeshProUGUI subtitleUI = default;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;

        GameObject speaker = GameObject.FindWithTag("Speaker");
        dialogueSpeaker = speaker.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDialogue(AudioClip clip)
    {
        if (dialogueSpeaker.isPlaying)
            dialogueSpeaker.Stop();
        
        dialogueSpeaker.PlayOneShot(clip);
    }

    public void SetSubtitle(string text, float delay)
    {
        subtitleUI.text = text;

        StartCoroutine(ClearSubtitlesAfterDialogue(delay));
    }

    public void ClearSubtitles()
    {
        subtitleUI.text = "";
    }

    private IEnumerator ClearSubtitlesAfterDialogue(float delay)
    {
        yield return new WaitForSeconds(delay);
        ClearSubtitles();
    }
}
