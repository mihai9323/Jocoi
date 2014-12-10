using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CreatePannelFromMemory : FlowerPannel {

	
	public void LoadFromMemory(){
		base.Start();
		Debug.Log("Load from memory");
		if(GameData.Memory!=null && this.pannelID != -1){
			if(this.pannelID<GameData.Memory.Length){
				this.trackId = GameData.Memory[this.pannelID].trackID;
				this.instrumentId = GameData.Memory[this.pannelID].instrumentID;
				this.appliedColor = GameData.Memory[this.pannelID].color;
				
			    if(this.Outline!=null)this.Outline.color = this.appliedColor;
			    if(this.OutlineNoFlower!=null)this.OutlineNoFlower = this.appliedColor;
			}
		}
	}
	public virtual void OnMouseEnter()
	{
		
		if(trackId != -1){
			if(particleSystem!=null)particleSystem.SetActive(true);
			SoundManager.Instance.FadeAllDown(instrumentId);
			SoundManager.Instance.instruments[instrumentId].FlowerSources[trackId].FadeSoundTo(1.0f);
			//this.gameObject.GetComponent<Animator>().SetBool("Hover", true);
			if(LevelData.Instance.tutorialMode) LevelData.Instance.loggedActions++;
		}
		
		
		
		
	}
	public virtual void OnMouseExit()
	{
		
		if(trackId != -1){
			if(particleSystem!=null)particleSystem.SetActive(false);
			SoundManager.Instance.FadeAllUp();
			if (SoundManager.Instance.instruments[instrumentId].currentTrack != trackId) SoundManager.Instance.instruments[instrumentId].FlowerSources[trackId].FadeSoundTo(0.0f);
		//	this.gameObject.GetComponent<Animator>().SetBool("Hover", false);
			SoundManager.Instance.instruments[instrumentId].CleanUp();
		}
	}
	
}
