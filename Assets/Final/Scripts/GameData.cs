using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameData  {
    public delegate void VOID_FUNCTION();
    public delegate void VOID_FUNCTION_INT(int i);
    public delegate void VOID_FUNCITON_PATH(Vector3[] path);
    
	public static FlowersInMemory[] Memory;
    
	public static void LoadFlowersInMemory(){
		if(LevelData.Instance != null){
			if(LevelData.Instance.flowerPannels != null){
		
				Memory = new FlowersInMemory[LevelData.Instance.flowerPannels.Length];
				for(int i = 0; i<LevelData.Instance.flowerPannels.Length;i ++){
					Memory[i] = new FlowersInMemory(
						LevelData.Instance.flowerPannels[i].instrumentId,
						LevelData.Instance.flowerPannels[i].trackId,
						LevelData.Instance.flowerPannels[i].FlowerImage.color,
						LevelData.Instance.flowerPannels[i].pattern
						);
				}
			}
		}
	}
    
}

public class FlowersInMemory{
	
	public int instrumentID;
	public int trackID;
	public Color color;
	public Texture2D pattern;
	
	public FlowersInMemory(int instrumentID, int trackID, Color color, Texture2D pattern){
		this.instrumentID = instrumentID;
		this.trackID = trackID;
		this.color = color;
		this.pattern = pattern;
	}
}
