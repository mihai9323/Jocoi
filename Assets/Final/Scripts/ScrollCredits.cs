using UnityEngine;
using System.Collections;

public class ScrollCredits : MonoBehaviour {

	public Vector3 finalPosition;
	private Vector3 initialPosition;
	private float ct;
	public int NextScene;
	[Range(0,.1f)]
	public float speed;
	
	private bool mouseDown;
	public enum Mode{
		FPR,Normal
	}
	public Mode mode;
	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
		ct = 0.02f;
		mouseDown = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0)){
			mouseDown = false;
		}
		if(Input.GetMouseButtonDown(0)){
			mouseDown = true;
		}
		if(mode == Mode.FPR){
			if(mouseDown){
			    if(ct<1)
			    
			    ct+= Time.deltaTime * speed;
				transform.position = Vector3.Lerp (initialPosition,finalPosition, ct);
			}else{
				if(ct>0)
				{ 
					ct-= Time.deltaTime * speed;
				}else{
					
						AutoFade.LoadLevel(NextScene,5.5f,.5f, Color.black);
					
				}
				transform.position = Vector3.Lerp (initialPosition,finalPosition,ct);
				
			}
			
			
		}else{
			if(ct<1){
				ct+= Time.deltaTime * speed;
			 }else{
				AutoFade.LoadLevel(NextScene,10f,.5f, Color.white);
			 }
			transform.position = Vector3.Lerp (initialPosition,finalPosition, ct);
		}
	}
}
