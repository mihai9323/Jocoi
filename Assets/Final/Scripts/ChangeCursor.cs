using UnityEngine;
using System.Collections;

public class ChangeCursor : MonoBehaviour {

	void OnMouseEnter(){
		if(Inputs.Instance!=null)if(Inputs.Instance.handCursor!=null)Cursor.SetCursor(Inputs.Instance.handCursor,new Vector2(0,0),CursorMode.ForceSoftware);
	}
	
	void OnMouseExit(){
		if(Inputs.Instance!=null)if(Inputs.Instance.defaultCursor!=null)Cursor.SetCursor(Inputs.Instance.defaultCursor,new Vector2(0,0),CursorMode.ForceSoftware);
	}
}
