using UnityEngine;
using System.Collections;

public class MouseUI : MonoBehaviour {
	
	public float timeBeforeStart = 2;
	public AnimationCurve move;
	private float timer = 1;
	private Vector3 showPos;
	private Vector3 hidePos;
	private float heightObject;
	private float objectMove;

	void Start () {
		//determining the amount the object should move, to be half hidden
		objectMove = transform.localScale.y/2;
		//vector for the distance to move for the object to be showing
		showPos = transform.position;
		//vector for the distance to move for the object to be half hidden
		hidePos = transform.position - Vector3.up * objectMove;
		//starting coroutine to move the object in place
		StartCoroutine (InitialShow());
	}

	void OnMouseEnter(){
		// Start coroutines for showing object
		StopAllCoroutines ();
		StartCoroutine(MoveObject(1));
	}
	
	void OnMouseExit(){
		// Start coroutines for hiding object
		StopAllCoroutines ();
		StartCoroutine(MoveObject(0));
	}

	IEnumerator InitialShow (){
		// wait some set time, before starting routine to hide object
		yield return new WaitForSeconds (timeBeforeStart);
		StartCoroutine(MoveObject(0));
	}

	IEnumerator MoveObject(float direction){
		//check for direction, 1=up, 0=down
		if (direction == 1) {
			//run the whole of the animation curve from 0 to 1
			while (timer < 1) {
				//gradually changing the position of the object's y
				//according to the animationcurve and the y coordinate of the show position.
				Vector3 localPosition = transform.localPosition;
				localPosition.y = move.Evaluate (timer) * showPos.y;
				transform.localPosition = localPosition;
				// Increase the timer by the time since last frame
				timer += Time.deltaTime;
				yield return new WaitForEndOfFrame ();
			}
		} else if (direction == 0) {
			//run the whole of the animation curve  backward from 1 to 0
			while (timer > 0) {
				//graduallychanging the position of the object's y
				//according to the animationcurve and the y coordinate of the hiding position, and the amount to move to be hidden.
				Vector3 localPosition = transform.localPosition;
				localPosition.y = (move.Evaluate (timer) * objectMove) - hidePos.y;
				transform.localPosition = localPosition;
				// Increase the timer by the time since last frame
				timer -= Time.deltaTime;
				yield return new WaitForEndOfFrame ();
			}
		}
	}
	

}
