using UnityEngine;
using System.Collections;

public class BackButtonM : MonoBehaviour {

    public int levelToStart = 1;
    public float setActiveTime = 1.0f;
    private bool works;
    private void Start()
    {
        works = false;
        gameObject.renderer.enabled = false;
        Invoke("EnableMe",setActiveTime);
    }
    private void EnableMe()
    {
        works = true;
        gameObject.renderer.enabled = true;
    }
    private void OnMouseUp() 
    {
        
        if (works)
        {
            
            AutoFade.LoadLevel(levelToStart, 1.0f, 1.0f, Color.black);
        }
    }
}
