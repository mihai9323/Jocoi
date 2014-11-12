using UnityEngine;
using System.Collections;

public class ScrollCredits : MonoBehaviour {

	public Vector3 finalPosition;
	private Vector3 initialPosition;
	private float ct;
	public int NextScene;
	[Range(0,.1f)]
	public float speed;
	public enum Mode{
		FPR,Normal
	}
	public Mode mode;
	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
		ct = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(mode == Mode.FPR){
			if(Input.GetMouseButton(0)){
			    if(ct<1)
			    
			    ct+= Time.deltaTime * speed;
				transform.position = Vector3.Lerp (initialPosition,finalPosition, ct);
			}else{
				if(ct>0)
				{ 
					ct-= Time.deltaTime * speed;
				}else{
					
						AutoFade.LoadLevel(NextScene,0.5f,.5f, Color.black);
					
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
