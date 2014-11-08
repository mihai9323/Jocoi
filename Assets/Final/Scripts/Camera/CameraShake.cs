using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	private float timer = 0;
	public float shakeTime;
	public AnimationCurve shake;

	void Update () {

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