using System.Collections;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public HungerManager hungerManager;
    private GameObject player;
    FirstPersonController movement;

    public GameObject redTransition;
    Image t;
    private Animator redAnim;
    
    public float deathAnimLength = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        movement = player.GetComponent<FirstPersonController>();

        redAnim = redTransition.GetComponent<Animator>();
        t = redTransition.GetComponent<Image>();

        StartCoroutine(SceneTransition());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Death()
    {
        movement.enabled = false;
        player.transform.position = transform.position;
        //redAnim.enabled = true;
        redAnim.SetBool("Dead", true);

        StartCoroutine(Respawn());
    }

    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(deathAnimLength);
        redAnim.SetBool("Dead", false);
        redAnim.SetBool("Respawning", true);
        hungerManager.hunger = hungerManager.maxHunger;
        movement.enabled = true;
        redAnim.SetBool("Respawning", false);
        //redAnim.enabled = false;
        ResetRedScreen(t);
    }

    public IEnumerator SceneTransition()
    {
        Debug.Log("Scene Transition");
        redAnim.SetBool("Respawning", true);
        yield return new WaitForSeconds(1.5f);
        redAnim.SetBool("Respawning", false);
        ResetRedScreen(t);
    }

    public void ResetRedScreen(Image image)
    {
        Color r = new Color32(124,33,33,0);
        image.color = r;
    }
}
