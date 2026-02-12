using UnityEngine;
using UnityEngine.UI;

public class HungerManager : MonoBehaviour
{
    public float hunger = 100.0f;
    public Text hungerDebug;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hunger -= Time.deltaTime;
    }
}
