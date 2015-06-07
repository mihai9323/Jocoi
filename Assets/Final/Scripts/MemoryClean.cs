using UnityEngine;
using System.Collections;

public class MemoryClean : MonoBehaviour {

	private void Start(){
		GameData.Memory = new FlowersInMemory[4];
	}
}
