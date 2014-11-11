using UnityEngine;
using System.Collections;

public class MemoryClean : MonoBehaviour {

	private void Start(){
		GameData.Memory = new System.Collections.Generic.List<Flower>();
	}
}
