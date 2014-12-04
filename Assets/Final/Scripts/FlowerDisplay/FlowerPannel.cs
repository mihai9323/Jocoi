using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FlowerPannel : MonoBehaviour {

	public Image Outline;
	public Image FlowerImage;
	public GameObject particleSystem;
	public Color OutlineNoFlower, FlowerImageColorNoFlower;
	public int instrumentId;
	
	internal int trackId;
	
	private Color appliedColor;
	
	private void Awake(){
		trackId = -1;
	}
	private void Start(){
		particleSystem.transform.position = Camera.main.ScreenToWorldPoint(this.transform.position);
		
		particleSystem.transform.position += Camera.main.transform.forward *1.5f;
		particleSystem.SetActive(false);
	}
	public void SetTemporaryOutline(Color color){
		appliedColor = Outline.color;
		Outline.color = color;
	}
	public void ResetOutline(){
		Outline.color = appliedColor;
	}
	
	public void SetFlowerImageColor(Color color, bool keepOutline = false){
		FlowerImage.color = color;
		if(!keepOutline){
			ResetOutline();
		}
	}
	public void RemoveFlowerColor(){
		FlowerImage.color = FlowerImageColorNoFlower;
		
	}
	
	public void OnMouseEnter()
	{
		
		if(trackId != -1){
			particleSystem.SetActive(true);
			SoundManager.Instance.FadeAllDown();
			SoundManager.Instance.instruments[instrumentId].FlowerSources[trackId].FadeSoundTo(1.0f);
			//this.gameObject.GetComponent<Animator>().SetBool("Hover", true);
			if(LevelData.Instance.tutorialMode) LevelData.Instance.loggedActions++;
		}
		
		
		
		
	}
	public void OnMouseExit()
	{
		if(trackId != -1){
			particleSystem.SetActive(false);
			SoundManager.Instance.FadeAllUp();
			if (SoundManager.Instance.instruments[instrumentId].currentTrack != trackId) SoundManager.Instance.instruments[instrumentId].FlowerSources[trackId].FadeSoundTo(0.0f);
		//	this.gameObject.GetComponent<Animator>().SetBool("Hover", false);
			SoundManager.Instance.instruments[instrumentId].CleanUp();
		}
	}
}
