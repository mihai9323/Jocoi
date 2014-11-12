using UnityEngine;
using System.Collections;

public class Particles : InteractableObject {

	public AudioClip sound;

	public override void StartLMB(){
		//Move particle to lamb
		LevelData.Instance.particle.GetComponent<MoveToPosition>().StartMoving(
			LevelData.Instance.Lamb.transform.position,
			FollowLamb,
			2.0f,
			1.8f			
			);
	}
	
	public override void StartRMB(){}	
	public override void StopLMB(){}	
	public override void StopRMB(){}	
	public override void StopAllInteractions(){}

	private void FollowLamb (){
		//Parent particle to lamb
		LevelData.Instance.particle.transform.parent = LevelData.Instance.Lamb.transform;
	}

	private void Destroy (){
		//destry particle
		Destroy (this.gameObject);
	}
}
