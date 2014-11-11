using UnityEngine;
using System.Collections;

public class MouseUI : MonoBehaviour {
	
	public float timeBeforeStart = 2;
	public AnimationCurve move;
	private float timer = 1;
	private Vector3 startPos;
	private Vector3 endPos;
	private float heightObject;
	private float objectMove;

	void Start () {
		heightObject = transform.localScale.y;
		Debug.Log ("heightObject " + heightObject);
		objectMove = heightObject / 2;
		Debug.Log ("objectMove " + objectMove);
		startPos = transform.position;
		Debug.Log ("startpos " + startPos);
		endPos = transform.position - Vector3.up * objectMove;
		Debug.Log ("endpos " + endPos);
		StartCoroutine (InitialShow());
	}

	void OnMouseEnter(){
		StopAllCoroutines ();
		StartCoroutine(MoveObject(endPos, 1));
	}
	
	void OnMouseExit(){
		StopAllCoroutines ();
		StartCoroutine(MoveObject(startPos, 0));
	}

	IEnumerator InitialShow (){
		yield return new WaitForSeconds (timeBeforeStart);
		StartCoroutine(MoveObject(startPos, 0));
	}

	IEnumerator MoveObject(Vector3 start, float direction){
		if (direction == 1) {
			while (timer < 1) {
				Vector3 localPosition = transform.localPosition;
				localPosition.y = move.Evaluate (timer) * startPos.y;
				transform.localPosition = localPosition;
				// Increase the timer by the time since last frame
				timer += Time.deltaTime;
				yield return new WaitForEndOfFrame ();
			}
		} else if (direction == 0) {
			while (timer > 0) {
				Vector3 localPosition = transform.localPosition;
				localPosition.y = (move.Evaluate (timer) * objectMove) - endPos.y;
				transform.localPosition = localPosition;
				// Increase the timer by the time since last frame
				timer -= Time.deltaTime;
				yield return new WaitForEndOfFrame ();
			}
		}
	}
	

}
