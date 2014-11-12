using UnityEngine;
using System.Collections;

public class WalkToRiver : MonoBehaviour {

    public AudioClip bahSound;
	public float movementSpeed = 2f;
	public float maxZoom = 20f;
    public Transform BabyWolf;
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
	private Vector3 initialCameraPos;

	void Start () {
		//set variables to positions of different objects
		initialCameraPos = camera.transform.position;
		eyelidTopPos = eyelidTop.transform.position;
		eyelidBottomPos = eyelidBottom.transform.position;
		startZoom = camera.fieldOfView;
		//calculate the distance each eyelid need to move
		eyelidDistance = eyelidTop.transform.position.y - eyelidBottom.transform.position.y;
		eyelidLength = eyelidDistance / 2;
		//start function to calculate distance to move each time a step is taken
		MoveDistance();
		//set eyelid move distance as part of vector
		eyelidMove.Set(0f, moveDistance, 0f);
	}
	
	void Update () {
        if (Input.GetMouseButtonDown(0)){
			//play sound when is button pressed
            if(bahSound!=null){
				Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if(Physics.Raycast(r,out hit)){
				
				}else{
					if(GetComponent<AudioSource>()){
						if(!GetComponent<AudioSource>().isPlaying){
							GetComponent<AudioSource>().clip = bahSound;
							
							GetComponent<AudioSource>().Play();
						}
						
					}else{
						
						gameObject.AddComponent<AudioSource>().PlayOneShot(bahSound);
					}
				}
            }
        }
		if (Input.GetMouseButton (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				//Baby wolf was hit
				if (hit.transform == BabyWolf) {
					//not reached maxZoom
					//zoom in
					if (camera.fieldOfView > maxZoom) {
						//eyelid move closer to each other
						eyelidTop.transform.position = Vector3.Lerp (eyelidTop.transform.position, (eyelidTop.transform.position - eyelidMove), 1 * Time.deltaTime);
						eyelidBottom.transform.position = Vector3.Lerp (eyelidBottom.transform.position, (eyelidBottom.transform.position + eyelidMove), 1 * Time.deltaTime);
						//change camera field of view gradually
						forwardZoom = camera.fieldOfView - movementSpeed;
						camera.fieldOfView = Mathf.Lerp (camera.fieldOfView, forwardZoom, 1 * Time.deltaTime);
						//head moving according to animationcurve from initial position
						Vector3 localPosition = camera.transform.localPosition;
						localPosition.y = headMovement.Evaluate (timer) + initialCameraPos.y;
						camera.transform.localPosition = localPosition;
						// Increase the timer by the time since last frame
						timer += Time.deltaTime;
					}
					//reached maxZoom
					else {
						AutoFade.LoadLevel (endScene,0.0f,1.0f,Color.black);
					}
				//if wolf was not hit
				} else {
					//not reached start zoom
					//zoom out
					if(camera.fieldOfView < startZoom){
						//eyelid move closer to each other
						eyelidTop.transform.position = Vector3.Lerp (eyelidTop.transform.position, (eyelidTop.transform.position + eyelidMove), 1 * Time.deltaTime);
						eyelidBottom.transform.position = Vector3.Lerp (eyelidBottom.transform.position, (eyelidBottom.transform.position - eyelidMove), 1 * Time.deltaTime);
						//change camera field of view gradually
						backwardZoom = camera.fieldOfView + movementSpeed;
						camera.fieldOfView = Mathf.Lerp (camera.fieldOfView, backwardZoom, 1 * Time.deltaTime);
						//head moving according to animationcurve from initial position
						Vector3 localPosition = camera.transform.localPosition;
						localPosition.y = headMovement.Evaluate (timer)  + initialCameraPos.y;
						camera.transform.localPosition = localPosition ;
						// Increase the timer by the time since last frame
						timer += Time.deltaTime;
					}
				}
			}
		} else{
			//not reached start zoom
			//zoom out
			if(camera.fieldOfView < startZoom){
				//eyelid move closer to each other
				eyelidTop.transform.position = Vector3.Lerp (eyelidTop.transform.position, (eyelidTop.transform.position + eyelidMove), 1 * Time.deltaTime);
				eyelidBottom.transform.position = Vector3.Lerp (eyelidBottom.transform.position, (eyelidBottom.transform.position - eyelidMove), 1 * Time.deltaTime);
				//change camera field of view gradually
				backwardZoom = camera.fieldOfView + movementSpeed;
				camera.fieldOfView = Mathf.Lerp (camera.fieldOfView, backwardZoom, 1 * Time.deltaTime);
				//head moving according to animationcurve from initial position
				Vector3 localPosition = camera.transform.localPosition;
				localPosition.y = headMovement.Evaluate (timer)  + initialCameraPos.y;
				camera.transform.localPosition = localPosition ;
				// Increase the timer by the time since last frame
				timer += Time.deltaTime;
			}
		}
	}

	void MoveDistance(){
		float zoom = (startZoom - maxZoom);
		float parts = (zoom/movementSpeed);
		moveDistance = (eyelidLength/parts);
	}
}
