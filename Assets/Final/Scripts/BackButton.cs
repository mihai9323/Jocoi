using UnityEngine;
using System.Collections;

public class BackButton : MonoBehaviour {

	public float timeUntilBack;
	public int sceneToLoad;
	public Transform button;
	private bool canPress;
	public float xButtonPos = ((Screen.width/4)*3);
	public float yButtonPos = ((Screen.height/4)*3);
	public float widthButton;
	public float heightButton;
	public Texture textureButton;
	//for unity 4.6 only
	//public RectTransform  rectButton;

	void Start () {
		//make button unable to be pressed
		canPress = false;
		//start coroutine to enable button after set time
		StartCoroutine("enableButton", timeUntilBack);
	}

	void OnGUI(){
		if(canPress){
			//make button to take the user back to set scene
			if(GUI.Button(new Rect(xButtonPos, yButtonPos, widthButton, heightButton), textureButton)){
                Puzzle.Instance.gameObject.SetActive(true);
				Application.LoadLevel(sceneToLoad);
			}
		}
		/* for using 4.6 only thingy i think

		 if(canPress){
			if(GUI.Button(new Rect(rectButton), textureButton)){
				Application.LoadLevel(sceneToLoad);
			}
		}*/
	}

	private IEnumerator enableButton(float timeUntilBack){
		yield return new WaitForSeconds(timeUntilBack);
		canPress = true;
		Debug.Log("button enabled");
	}
}
