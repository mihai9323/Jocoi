using UnityEngine;
using System.Collections;

public class starGrow : MonoBehaviour {
	public float amplitude;
	Vector3 scale;
	float r;
	void Start()
	{ 

		scale = transform.localScale;
		r = Random.Range(11.0f, 2.0f);
	}

	void Update ()


	{
		float t = Time.deltaTime;
		transform.localScale = new Vector3 ((transform.localScale.x - (Mathf.Sin(Time.time*r)*amplitude)), (transform.localScale.y - (Mathf.Sin(Time.time*r)*amplitude)), (transform.localScale.z - (Mathf.Sin(Time.time*r)*amplitude)));


		//transform.localScale += Vector3.Lerp(scale,  (transform.localScale * (Mathf.Sin(Time.time)*amplitude)), Time.deltaTime);

	



	}
}
