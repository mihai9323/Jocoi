using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	private float timer = 0;
	public float shakeTime;
	public AnimationCurve shake;

	void Update () {
		if (Input.GetMouseButton (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.name == "Cube") {
					Debug.Log ("My object is clicked by mouse");
					Vector3 localPosition = transform.localPosition;
					localPosition.y = shake.Evaluate (timer);
					transform.localPosition = localPosition;
					// Increase the timer by the time since last frame
					timer += Time.deltaTime;
					Debug.Log (timer);
					}
				}
		}
	}

	void StartShake(){
		while (timer < shakeTime) {
			Vector3 localPosition = transform.localPosition;
			localPosition.y = shake.Evaluate (timer);
			transform.localPosition = localPosition;
			// Increase the timer by the time since last frame
			timer += Time.deltaTime;
		}
	}
}