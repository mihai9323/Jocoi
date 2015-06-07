using UnityEngine;
using System.Collections;

public class Puzzle : MonoBehaviour {
	public static Puzzle Instance;
	
	public event GameData.VOID_FUNCTION PuzzleCompleted;
	
	// Use this for initialization
	void Awake () {
		if(Instance == null){
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
		}else{
			Destroy (this.gameObject);
		}
	}
	
	public void CheckCompleted(){
		bool completed = true;
		
		if(LevelData.Instance !=null){
			foreach(CreatePannelFromMemory pannel in LevelData.Instance.flowerPannels){
				if(pannel.isSet){
					if(pannel.colorIsApplied){
						Debug.Log("good");
					}else{
						Debug.Log("not good");
						completed = false;
					}
				}else Debug.Log("good");
				
			}
			if(completed){
				Debug.Log("Completed");
				if(PuzzleCompleted!=null)PuzzleCompleted();
			}else Debug.Log("Not completed");
		}
	}
	
}
