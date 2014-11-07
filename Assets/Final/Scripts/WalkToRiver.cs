using UnityEngine;
using System.Collections;

public class WalkToRiver : MonoBehaviour {

	public float movementSpeed;
	public float maxZoom = 20f;
	private float newZoom;
	private float startZoom;
	private float moveDistance;

	public AnimationCurve headMovement;
	public float animationCurveTime;

	public AudioClip sound;
	public int endScene;
	public Camera camera;

	public float eyelidLength;

	void Start () {
		startZoom = camera.fieldOfView;
		MoveDistance();
	}
	
	void Update () {
		//   wolf/sheep/lamb pressed
		if(true){
			//not reached maxZoom
			if(camera.fieldOfView > maxZoom){
				AudioSource.PlayClipAtPoint(sound, camera.transform.position);
				//eyelid thingy goes here

				//zoom
				newZoom = camera.fieldOfView - movementSpeed;
				camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, newZoom, (animationCurveTime * Time.deltaTime));
			}
			//reached maxZoom
			else{
				Application.LoadLevel(endScene);
			}
		}
	}

	void MoveDistance(){
		float zoom = startZoom - maxZoom;
		float parts = zoom/movementSpeed;
		moveDistance = eyelidLength/parts;
	}
	
}
