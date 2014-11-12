using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {

	public Color initialColor;
	public Color changedColor;
	public string nextLevelName;
	public float fadeOutTime;
	public float fadeInTime;
	public Color fadeColor;

	void Start () {
		//set to initial color
		renderer.material.color = initialColor;
	}

	void OnMouseOver(){
		//change color on mouse over
		renderer.material.color = changedColor;
	}

	void OnMouseExit (){
		//change to initial color, when not mouse over
		renderer.material.color = initialColor;
	}

	void OnMouseDown(){
		//go to level when button pressed
		AutoFade.LoadLevel (nextLevelName, fadeOutTime, fadeInTime, fadeColor);
	}
}
