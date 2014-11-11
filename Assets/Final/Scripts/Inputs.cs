/* Code by Mihai-Ovidu ANTON 10/24/2014
 * 
 * */
using UnityEngine;
using System.Collections;

public class Inputs : MonoBehaviour {

    public static Inputs Instance;
    public bool canInteract = true;

    public bool canUseLeftClick = true;

    public int activeMode;
    public Texture2D defaultCursor;
    public Texture2D handCursor;
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
        activeMode = -1;
        canInteract = true;
        
    }
    private void Start(){
		Cursor.SetCursor(defaultCursor,new Vector2(0,0),CursorMode.ForceSoftware);
    }
    private void Update()
    {
        if (canInteract)
        {
            Vector3 mPos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mPos);
            RaycastHit hit;
            if (canUseLeftClick)
            {
                //LMB button Down
                if (Input.GetMouseButtonDown(0))
                {
                    InteractableObject clickedObject;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.GetComponent<InteractableObject>() != null)
                        {
                            clickedObject = hit.collider.gameObject.GetComponent<InteractableObject>();
                            DeactivateObject(clickedObject);
                            ActivateObject(clickedObject, 0);

                        }

                    }
                }
                else
                {
                    //LMB UP
                    if (Input.GetMouseButtonUp(0))
                    {
                        if (_activeObject != null) _activeObject.StopLMB();
                        activeClick = -1;
                    }
                }
            }
            //RMB down
            if (Input.GetMouseButtonDown(1))
            {
                InteractableObject clickedObject;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.GetComponent<InteractableObject>() != null)
                    {
                        clickedObject = hit.collider.gameObject.GetComponent<InteractableObject>();
                        DeactivateObject(clickedObject);
                        ActivateObject(clickedObject, 1);

                    }

                }
            }
            else
            {
                //RMB UP
                if (Input.GetMouseButtonUp(1))
                {
                    if(_activeObject!=null)_activeObject.StopRMB();
                    activeClick = -1;
                }
            }
        }


    }
    private void ActivateObject(InteractableObject obj, int mb)
    {
        _activeObject = obj;
        activeClick = mb;
        activeMode = mb;
        switch (mb)
        {
            case 0: _activeObject.StartLMB(); break;
            case 1: _activeObject.StartRMB(); break;
        }
        
    }
    private void DeactivateObject(InteractableObject obj)
    {
        activeMode = -1;
        if (_activeObject != null)
        {
            if (_activeObject != obj) _activeObject.StopAllInteractions();
        } 
    }
    public void ActiveObjectDestroyed()
    {
        activeMode = -1;
        _activeObject = null;
    }
    public Vector3 GetMouseOnScreen()
    {
       
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == _activeObject.gameObject) { 
                lastPosition = hit.point;
                return  hit.point;
            }
            else return lastPosition;

        }
        else return lastPosition;
    }
    
    
}
