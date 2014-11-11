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
		renderer.material.color = initialColor;
	}
	void Update () {}

	void OnMouseOver(){
		renderer.material.color = changedColor;
	}

	void OnMouseExit (){
		renderer.material.color = initialColor;
	}

	void OnMouseDown(){
		AutoFade.LoadLevel (nextLevelName, fadeOutTime, fadeInTime, fadeColor);
	}
}
