using UnityEngine;
using System.Collections;

public class SheepBehaviour : MonoBehaviour {

	Animator anim;

	int walkHash = Animator.StringToHash("Walking");
	int jumpHash = Animator.StringToHash("Jumping");
	int layHash = Animator.StringToHash("Lay");
	int pickUpHash = Animator.StringToHash("PickUp");
	int giveHash = Animator.StringToHash("Give");
	int eatHash = Animator.StringToHash("Eat");


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Animations ();
	}

	void Animations () {


		//Walking
		if (Input.GetKeyDown (KeyCode.W)) {
			anim.SetBool (walkHash, true);
		}
		if (Input.GetKeyUp (KeyCode.W)) {
			anim.SetBool (walkHash, false);
		}

		//Jumping
		if (Input.GetKeyDown (KeyCode.Space)) {
			anim.SetBool (jumpHash, true);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			anim.SetBool (jumpHash, false);
		}

		//Lay
		if (Input.GetKeyDown (KeyCode.L)) {
			anim.SetBool (layHash, true);
		}
		if (Input.GetKeyUp (KeyCode.L)) {
			anim.SetBool (layHash, false);
		}

		//PickUp
		if (Input.GetKeyDown (KeyCode.P)) {
			anim.SetBool (pickUpHash, true);
		}
		if (Input.GetKeyUp (KeyCode.P)) {
			anim.SetBool (pickUpHash, false);
		}

		//Give
		if (Input.GetKeyDown (KeyCode.G)) {
			anim.SetBool (giveHash, true);
		}
		if (Input.GetKeyUp (KeyCode.G)) {
			anim.SetBool (giveHash, false);
		}

		//Eat
		if (Input.GetKeyDown (KeyCode.E)) {
			anim.SetBool (eatHash, true);
		}
		if (Input.GetKeyUp (KeyCode.E)) {
			anim.SetBool (eatHash, false);
		}
	}
}

