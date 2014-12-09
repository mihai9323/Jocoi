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
				if(pannel.appliedColor != pannel.FlowerImage.color){
					completed = false;
				}
			}
			if(completed){
				if(PuzzleCompleted!=null)PuzzleCompleted();
			}
		}
	}
	
}
