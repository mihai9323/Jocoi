using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FlowerPannel : MonoBehaviour {

	public Image Outline;
	public Image FlowerImage;
	public GameObject particleSystem;
	public Color OutlineNoFlower, FlowerImageColorNoFlower;
	public int instrumentId;
	public int pannelID = -1;
	public int trackId;
	public Texture2D pattern;
	internal Color appliedColor;
	
	
	protected virtual void Start(){
	
		if(pannelID == -1){
			pannelID = instrumentId;
		}
		particleSystem.transform.position = Camera.main.ScreenToWorldPoint(this.transform.position);
		
		particleSystem.transform.position += Camera.main.transform.forward *1.5f;
		particleSystem.SetActive(false);
	}
	public virtual void SetTemporaryOutline(Color color){
		appliedColor = Outline.color;
		Outline.color = color;
	}
	public virtual void ResetOutline(){
		Outline.color = appliedColor;
	}
	
	public virtual void SetFlowerImageColor(Color color, bool keepOutline = false){
		FlowerImage.color = color;
		if(!keepOutline){
			ResetOutline();
		}
	}
	public virtual void RemoveFlowerColor(){
		FlowerImage.color = FlowerImageColorNoFlower;
		
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
