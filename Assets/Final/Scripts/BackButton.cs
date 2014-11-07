using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour {

	public float timeUntilBack;
	public int sceneToLoad;
	public Transform button;
	private bool canPress;
	private float xButton;
	private float yButton;

	void Start () {
		xButton = ((Screen.width/4)*3);
		yButton = ((Screen.height/4)*3);
		canPress = false;
		StartCoroutine("enableButton", timeUntilBack);
	}

	void OnGUI(){
		if(canPress){
			if(GUI.Button(new Rect(xButton, yButton, 100f, 50f), "Back to normal")){
				Application.LoadLevel(sceneToLoad);
			}
		}
	}

	private IEnumerator enableButton(float timeUntilBack){
		yield return new WaitForSeconds(timeUntilBack);
		canPress = true;
		Debug.Log("button enabled");
	}
}
