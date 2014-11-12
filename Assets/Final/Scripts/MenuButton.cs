using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {

	
	public int nextLevel;
	public float fadeOutTime;
	public float fadeInTime;
	public Color fadeColor;
	public bool quit;
	public Material[] m;

	void Start () {
		//set to initial color
		renderer.material = m[0];
	}

	

	void OnMouseDown(){
		//go to level when button pressed
		if(quit) Application.Quit();
		else{
			AutoFade.LoadLevel (nextLevel, fadeOutTime, fadeInTime, fadeColor);
			}
	}
	void OnMouseOver(){
		renderer.material = m[1];
	}
	void OnMouseExit(){
		renderer.material = m[0];
	}
}
