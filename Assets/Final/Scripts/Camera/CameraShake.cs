using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	private float timer = 0;
	public float shakeTime;
    public AnimationCurve shake;
    private Vector3 initialPos;

    private void Start(){
        initialPos = transform.position;
    }

	public void StartShake(){
        StartCoroutine("properShakeLookAtMe");
	}

    private IEnumerator properShakeLookAtMe(){
        while (timer < shakeTime){
			//gradually change the y position according to the animationcurve from the initial position
            Vector3 localPosition = transform.localPosition;
            localPosition.y = shake.Evaluate(timer)+initialPos.y;
            transform.localPosition = localPosition;
            // Increase the timer by the time since last frame
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}