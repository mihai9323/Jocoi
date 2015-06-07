using UnityEngine;
using System.Collections;

public class CountMouseInput : MonoBehaviour {

    public int LMBDown, LMBUp, RMBDown, RMBUp;
    
    public float maxHoverTime;
    public float maxLMBTime;
    public float maxRMBTime;
   
    private float currentHoverTime;
    private float currentLMBTime;
    private float currentRMBTime;

    private bool RMB, LMB;


    private void Start()
    {
        LMBDown = LMBUp = RMBDown = RMBUp = 0;
        maxHoverTime = maxLMBTime = maxRMBTime = 0;
        RMB = LMB = false;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider == this.gameObject.collider)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    LMBDown++;
                    currentLMBTime = Time.timeSinceLevelLoad;
                    LMB = true;
                }
                if (Input.GetMouseButtonDown(1))
                {
                    RMBDown++;
                    currentRMBTime = Time.timeSinceLevelLoad;
                    RMB = true;
                }
            }
        }
        if (LMB)
        {
            if (Input.GetMouseButtonUp(0))
            {
                LMB = false;
                if (Time.timeSinceLevelLoad - currentLMBTime > maxLMBTime)
                {
                    maxLMBTime = Time.timeSinceLevelLoad - currentLMBTime;
                }
                LMBUp++;
            }
        }
        if (RMB)
        {
           
            if (Input.GetMouseButtonUp(1))
            {
                RMB = false;
                RMBUp++;
                if (Time.timeSinceLevelLoad - currentRMBTime > maxRMBTime)
                {
                    maxRMBTime = Time.timeSinceLevelLoad - currentRMBTime;
                }
            }
        }
    }
   

    private void OnMouseEnter()
    {
        currentHoverTime = Time.timeSinceLevelLoad;
        Debug.Log("MouseOver");


    }
    private void OnMouseExit()
    {

        if (Time.timeSinceLevelLoad - currentHoverTime > maxHoverTime)
        {
            maxHoverTime = (Time.timeSinceLevelLoad - currentHoverTime);
        }
    }

}
