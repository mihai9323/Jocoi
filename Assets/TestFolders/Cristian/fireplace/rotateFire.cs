using UnityEngine;
using System.Collections;

public class rotateFire : MonoBehaviour {
	public float speed;
	public AnimationCurve animCurve;

	public AnimationCurve animateY;
	private float sample;
	private Vector3 initPos;
	// Use this for initialization
	void Start () {
		speed = Random.Range (speed, speed * 4);
		sample = 0;
		initPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate(Vector3.back, speed * Time.deltaTime * animCurve.Evaluate(sample));
		sample += Time.deltaTime;

		transform.position = new Vector3 (0, animateY.Evaluate (sample), 0) + initPos;
	
	}
}
