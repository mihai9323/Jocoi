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
		FPR,Normal,Static
	}
	public Mode mode;
	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
		ct = 0.02f;
		mouseDown = true;
	}
	void OnGUI(){
		Event current = Event.current;
		if(current!=null){
			if(current.type == EventType.MouseDown ){
				mouseDown = true;
			}
			if(current.type == EventType.MouseUp){
				mouseDown = false;
			}
		}
	}
	// Update is called once per frame
	void Update () {
	
		if(mode == Mode.FPR){
			if(mouseDown){
			    if(ct<1)
			    
			    ct+= Time.deltaTime * speed;
				transform.position = Vector3.Lerp (initialPosition,finalPosition, ct);
			}else{
				if(ct>0)
				{ 
					ct-= Time.deltaTime * speed*20;
				}else{
					
					AutoFade.LoadLevel(NextScene,1.5f,.5f, Color.black);
					
				}
				transform.position = Vector3.Lerp (initialPosition,finalPosition,ct);
				
			}
			
			
		}else if(mode == Mode.Normal){
			if(ct<1){
				ct+= Time.deltaTime * speed;
			 }else{
				AutoFade.LoadLevel(NextScene,10f,.5f, Color.white);
			 }
			transform.position = Vector3.Lerp (initialPosition,finalPosition, ct);
		}else if(mode == Mode.Static){
			if(ct<1){
				ct+= Time.deltaTime * speed;
			}else{
				AutoFade.LoadLevel(NextScene,10f,.5f, Color.white);
			}
		}
	}
}
