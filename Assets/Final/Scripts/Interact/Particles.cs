using UnityEngine;
using System.Collections;

public class Particles : InteractableObject {

	public AudioClip sound;

	public override void StartLMB(){
		//attatch to lamb

	}
	
	public override void StartRMB(){}
	
	public override void StopLMB(){
		
	}
	
	public override void StopRMB(){}
	
	public override void StopAllInteractions(){
		
	}

	private void FollowLamb (){
		//play some kind of floating animation?

	}

	private void Destroy (){
		Destroy (this.gameObject);
	}

	private void Start (){
		//Particle over ground
	}

}
