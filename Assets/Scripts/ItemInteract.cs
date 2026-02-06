using UnityEngine;

public class ItemInteract : MonoBehaviour
{
    public GameObject interactUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag ("Player"))
        {
            interactUI.SetActive(true);
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.CompareTag ("Player"))
        {
            interactUI.SetActive(false);
        }
    }
}
