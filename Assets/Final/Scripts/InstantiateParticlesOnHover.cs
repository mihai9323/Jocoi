using UnityEngine;
using System.Collections;

public class InstantiateParticlesOnHover : MonoBehaviour {

	public Vector3 offset;
	public GameObject ParticleSystemObject;
	
	private GameObject  instantiated;
	private bool isOver = false;
	// Update is called once per frame
	void Update () {
		if(isOver){
			if(instantiated == null){
				if(ParticleSystemObject!=null)instantiated = Instantiate(ParticleSystemObject,transform.position+offset, ParticleSystemObject.transform.rotation) as GameObject;
			}
		}else{
			if(instantiated !=null){
				Destroy(instantiated);
				instantiated = null;
			}
		}
	}
	void OnMouseEnter(){
		isOver = true;
	}
	void OnMouseExit(){
		isOver = false;
	}
}
