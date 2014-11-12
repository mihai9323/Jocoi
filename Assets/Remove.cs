using UnityEngine;
using System.Collections;

public class Remove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(Puzzle.Instance != null){
			Destroy(Puzzle.Instance.gameObject);
		}
	}
	
	
}
