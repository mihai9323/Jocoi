using UnityEngine;
using System.Collections;

public class WalkToRiver : MonoBehaviour {

	public float movementSpeed = 2f;
	public float maxZoom = 20f;

	public AnimationCurve headMovement;
	public AudioClip sound;
	public int endScene;
	public Camera camera;
	private float timer = 0;

	private float forwardZoom;
	private float backwardZoom;
	private float startZoom;

	private float eyelidDistance;
	private float eyelidLength;
	private float eyelidSteps;
	private float moveDistance;
	public GameObject eyelidTop;
	public GameObject eyelidBottom;
	private Vector3 eyelidTopPos;
	private Vector3 eyelidBottomPos;
	private Vector3 eyelidMove;

	void Start () {
		eyelidTopPos = eyelidTop.transform.position;
		eyelidBottomPos = eyelidBottom.transform.position;
		startZoom = camera.fieldOfView;
		eyelidDistance = eyelidTop.transform.position.y - eyelidBottom.transform.position.y;
		eyelidLength = eyelidDistance / 2;
		MoveDistance();
		eyelidMove.Set(0f, moveDistance,0f);
	}
	
	void Update () {
		//   wolf/sheep/lamb pressed
		if (Input.GetMouseButton (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.name == "Cube") {
					Debug.Log ("My object is clicked by mouse");
					//not reached maxZoom
					if (camera.fieldOfView > maxZoom) {
						//eyelid
						eyelidTop.transform.position = Vector3.Lerp (eyelidTop.transform.position, (eyelidTop.transform.position - eyelidMove), 1 * Time.deltaTime);
						eyelidBottom.transform.position = Vector3.Lerp (eyelidBottom.transform.position, (eyelidBottom.transform.position + eyelidMove), 1 * Time.deltaTime);
						//zoom
						forwardZoom = camera.fieldOfView - movementSpeed;
						camera.fieldOfView = Mathf.Lerp (camera.fieldOfView, forwardZoom, 1 * Time.deltaTime);

						//head
						Vector3 localPosition = camera.transform.localPosition;
						localPosition.y = headMovement.Evaluate (timer);
						camera.transform.localPosition = localPosition;
						// Increase the timer by the time since last frame
						timer += Time.deltaTime;
					}
					//reached maxZoom
					else {
						Application.LoadLevel (endScene);
					}
				}
			}
		} else{
			if(camera.fieldOfView < startZoom){
				//eyelids
				eyelidTop.transform.position = Vector3.Lerp (eyelidTop.transform.position, (eyelidTop.transform.position + eyelidMove), 1 * Time.deltaTime);
				eyelidBottom.transform.position = Vector3.Lerp (eyelidBottom.transform.position, (eyelidBottom.transform.position - eyelidMove), 1 * Time.deltaTime);
				//Zoom
				backwardZoom = camera.fieldOfView + movementSpeed;
				camera.fieldOfView = Mathf.Lerp (camera.fieldOfView, backwardZoom, 1 * Time.deltaTime);
				//head
				Vector3 localPosition = camera.transform.localPosition;
				localPosition.y = headMovement.Evaluate (timer);
				camera.transform.localPosition = localPosition;
				// Increase the timer by the time since last frame
				timer += Time.deltaTime;
			}
		}
	}

	void MoveDistance(){
		float zoom = (startZoom - maxZoom);
		Debug.Log ("zoom is " + zoom);
		float parts = (zoom/movementSpeed);
		Debug.Log ("parts is " + parts);
		moveDistance = (eyelidLength/parts);
		Debug.Log ("moveDistance is " + moveDistance);
	}
	
}
