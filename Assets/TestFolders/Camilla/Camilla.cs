using UnityEngine;
using System.Collections;

public class Camilla : MonoBehaviour {
	public GameObject nr1;
	public GameObject nr2;

	// Use this for initialization
	void Start () {
		Debug.Log (ExtensionMethods.SquaredDistance(nr1.transform.position, nr2.transform.position));
	}
	
	// Update is called once per frame
	void Update () {
		ExtensionMethods.FaceObjectOnAxis(nr1.transform, nr2.transform, Vector3.up);
	}
}
