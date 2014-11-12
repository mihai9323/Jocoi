using UnityEngine;
using System.Collections;

public class ChangeCursor : MonoBehaviour {

	void OnMouseEnter(){
		Cursor.SetCursor(Inputs.Instance.handCursor,new Vector2(0,0),CursorMode.ForceSoftware);
	}
	
	void OnMouseExit(){
		Cursor.SetCursor(Inputs.Instance.defaultCursor,new Vector2(0,0),CursorMode.ForceSoftware);
	}
}
