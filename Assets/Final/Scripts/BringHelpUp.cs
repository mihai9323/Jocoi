using UnityEngine;
using System.Collections;

public class BringHelpUp : MonoBehaviour {

	public float stayOnScreen = 5.0f;
	public float animSpeed = 1.0f;
	public float amplitude = 1.0f;
	
	public AnimationCurve animCurve;
	private float ct = 0;
	private bool mouseOver{
		set{
			if(value != _mouseOver){
				_mouseOver = value;
				if(_mouseOver){
					MouseOver ();
				}else{
					MouseOut ();
				}
			}
		}
		get{
			return _mouseOver;
		}
	}
	
	private bool _mouseOver;
	
	private Vector3 initialPosition;
	public Color initialColor;
	public Color otherColor;
	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
		mouseOver = false;
		StartCoroutine(startMovingDown());
	}
	
	void Update(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider.gameObject == gameObject) { 
				mouseOver = true;
			}else mouseOver = false;
		}else mouseOver = false;
	}
	
	IEnumerator startMovingDown(){
		
		yield return new WaitForSeconds(stayOnScreen);
		stayOnScreen = 0;
		while(ct<1){
			ct += Time.fixedDeltaTime * animSpeed;
			transform.position = initialPosition + new Vector3(0,amplitude,0) * animCurve.Evaluate(ct);
			renderer.material.color = Color.Lerp(initialColor,otherColor,ct);
				
			yield return new WaitForFixedUpdate();
		}
	}
	IEnumerator startMovingUp(){
		
		
		while(ct>0){
			ct -= Time.fixedDeltaTime * animSpeed;
			transform.position = initialPosition +new Vector3(0,amplitude,0) * animCurve.Evaluate(ct);
			renderer.material.color = Color.Lerp(initialColor,otherColor,ct);
			
			yield return new WaitForFixedUpdate();
		}
	}
	void MouseOver(){
		StopAllCoroutines();
		Debug.Log ("start");
		StartCoroutine(startMovingUp());
	}
	void MouseOut(){
		StopAllCoroutines();
		StartCoroutine(startMovingDown());
	}
}
