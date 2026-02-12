using UnityEngine;
using UnityEngine.UIElements;

public class HungerVFX : MonoBehaviour
{
    float fadeRate = 30.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        
    }

    public void RedFadeIn()
    {
        GetComponent<Image>().tintColor = new (124f, 33f, 33f, 255/fadeRate * Time.deltaTime);
    }

        public void RedFadeOut()
    {
        GetComponent<Image>().tintColor = new (124f, 33f, 33f, (255/fadeRate) / Time.deltaTime);
    }
}
