/* Code by Mihai-Ovidu ANTON 10/24/2014
 * 
 * */
using UnityEngine;
using System.Collections;

public class Inputs : MonoBehaviour {

    public static Inputs Instance;
    public InteractableObject ActiveObject{
        get
        {
            return _activeObject;
        }      
    }

    private InteractableObject _activeObject;
    private int activeClick;
    private Vector3 lastPosition;
    private void Awake(){
        lastPosition = transform.position;
        Instance = this;
        activeClick = -1;
    }
    private void Update()
    {
        Vector3 mPos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mPos);
        RaycastHit hit;
        //LMB button Down
        if (Input.GetMouseButtonDown(0) && activeClick == -1)
        {
            InteractableObject clickedObject;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<InteractableObject>() != null)
                {
                    clickedObject = hit.collider.gameObject.GetComponent<InteractableObject>();
                    DeactivateObject(_activeObject);
                    ActivateObject(clickedObject,0);
                    
                }

            }
        }
        else
        {
            //LMB UP
            if (Input.GetMouseButtonUp(0) && activeClick == 0)
            {
                _activeObject.StopLMB();
                activeClick = -1;
            }
        }
        //RMB down
        if (Input.GetMouseButtonDown(1) && activeClick == -1)
        {
            InteractableObject clickedObject;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<InteractableObject>() != null)
                {
                    clickedObject = hit.collider.gameObject.GetComponent<InteractableObject>();
                    DeactivateObject(_activeObject);
                    ActivateObject(clickedObject, 1);

                }

            }
        }
        else
        {
            //RMB UP
            if (Input.GetMouseButtonUp(1) && activeClick == 1)
            {
                _activeObject.StopRMB();
                activeClick = -1;
            }
        }


    }
    private void ActivateObject(InteractableObject obj, int mb)
    {
        _activeObject = obj;
        activeClick = mb;
        switch (mb)
        {
            case 0: _activeObject.StartLMB(); break;
            case 1: _activeObject.StartRMB(); break;
        }
        
    }
    private void DeactivateObject(InteractableObject obj)
    {
        if(obj!=null)obj.StopAllInteractions();
    }

    public Vector3 GetMouseOnScreen()
    {
       
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            lastPosition = hit.point;
            return  hit.point;

        }
        else return lastPosition;
    }
    
    
}
