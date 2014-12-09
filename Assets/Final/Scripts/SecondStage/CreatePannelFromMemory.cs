using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CreatePannelFromMemory : FlowerPannel {

	
	public void LoadFromMemory(){
		base.Start();
		if(GameData.Memory!=null && this.pannelID != -1){
			if(this.pannelID<GameData.Memory.Length){
				this.trackId = GameData.Memory[this.pannelID].trackID;
				this.instrumentId = GameData.Memory[this.pannelID].instrumentID;
				this.appliedColor = GameData.Memory[this.pannelID].color;
			    SetTemporaryOutline(this.appliedColor);
			    SetTemporaryOutline(this.appliedColor); //This is intended to over right the previous color to the new one	
			}
		}
	}
	
	
}
