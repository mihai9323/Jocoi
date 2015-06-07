using UnityEngine;
using System.Collections;

public class HighlightOnHover : MonoBehaviour {
	public Material overMat,outMat;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseEnter(){
		renderer.material = overMat;
	}
	void OnMouseExit(){
		renderer.material = outMat;
	}
}
