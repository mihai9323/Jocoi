using UnityEngine;
using System.Collections;

public class PuzzleFlower : Flower {
	
	public enum GrassState{
		HighGrass,
		TallGrass,
		MediumGrass,
		LowGrass,
		Flower
	}
	public GrassState grassState{
		set{
			if(value != _grassState){
				_grassState = value;
				
				highGrass.SetActive(false);
				tallGrass.SetActive(false);
				mediumGrass.SetActive(false);
				lowGrass.SetActive(false);
			
				
				switch(value){
					case GrassState.HighGrass: highGrass.SetActive(true);break;
					case GrassState.TallGrass: tallGrass.SetActive(true);  break;
					case GrassState.MediumGrass: mediumGrass.SetActive(true);  break;
					case GrassState.LowGrass: lowGrass.SetActive(true);  break;
				    case GrassState.Flower:   break;
				}
			}
		}
		get{
			return _grassState;
		}
	}
	private GrassState _grassState;
	
	private GameObject highGrass,tallGrass,mediumGrass,lowGrass;
	
	public GameObject HighGrass, TallGrass, MediumGrass, LowGrass;
	
	private GameObject activeObject;
	
	
	
	protected override void Start ()
	{
		highGrass   = Instantiate(HighGrass,transform.position,HighGrass.transform.rotation) as GameObject;
		tallGrass   = Instantiate(TallGrass,transform.position,TallGrass.transform.rotation) as GameObject;
		mediumGrass = Instantiate(MediumGrass,transform.position,MediumGrass.transform.rotation) as GameObject;
		lowGrass    = Instantiate(LowGrass,transform.position,LowGrass.transform.rotation) as GameObject;
		
		highGrass.SetActive(true);
		tallGrass.SetActive(false);
		mediumGrass.SetActive(false);
		lowGrass.SetActive(false);
		
		
	}
	
	
	
	public override void StartLMB()
	{
		
	}
	
	
	
	
	public override void Feed()
	{
		
	}
	protected override void LambEat()
	{
		
	}
	protected override void LambFinishEat()
	{
		
	}
	
	protected override void FinishEat()
	{
		
		LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().anim.SetBool(eatAnimation,false);
		Inputs.Instance.canInteract = true;
		
		if(grassState == GrassState.HighGrass) grassState = GrassState.TallGrass;
		else if(grassState == GrassState.TallGrass) grassState = GrassState.MediumGrass;
		else if(grassState == GrassState.MediumGrass) grassState = GrassState.LowGrass;
		else if(grassState == GrassState.LowGrass) grassState = GrassState.Flower;
		else if(grassState == GrassState.Flower){
			if(this.patternToAdd.trackID == LevelData.Instance.flowerPannels[this.patternToAdd.flowerPannel].trackId){
				LevelData.Instance.flowerPannels[this.patternToAdd.instrumentID].SetFlowerImageColor(this.patternToAdd.color,this.patternToAdd.texture);
				Puzzle.Instance.CheckCompleted();
				
			}else{
				
				
				
				foreach(CreatePannelFromMemory cpfm in LevelData.Instance.flowerPannels){
					cpfm.SetFlowerImageColor(cpfm.FlowerImageColorNoFlower,cpfm.pattern);
				}
			}
			Destroy (highGrass);
			Destroy (tallGrass);
			Destroy (mediumGrass);
			Destroy (lowGrass);
			Destroy(this.gameObject, 0.1f);
		}
		
		
	}
	
	
	protected override void OnMouseEnter()
	{
		if(LevelData.Instance!=null)if(LevelData.Instance.tutorialMode) LevelData.Instance.loggedActions++;
		SoundManager.Instance.FadeAllDown(patternToAdd.instrumentID);
		SoundManager.Instance.instruments[patternToAdd.instrumentID].FlowerSources[patternToAdd.trackID].FadeSoundTo(SoundManager.Instance.highVolume);
		//activeObject.GetComponent<Animator>().SetBool("Hover", true);
		//if(LevelData.Instance!=null)if(LevelData.Instance.flowerPannels!=null) LevelData.Instance.flowerPannels[patternToAdd.flowerPannel].SetTemporaryOutline(this.patternToAdd.color);
		
		
	}
	protected override void OnMouseExit()
	{
		
		SoundManager.Instance.FadeAllUp();
		if (SoundManager.Instance.instruments[patternToAdd.instrumentID].currentTrack != patternToAdd.trackID) SoundManager.Instance.instruments[patternToAdd.instrumentID].FlowerSources[patternToAdd.trackID].FadeSoundTo(SoundManager.Instance.lowVolume);
		//activeObject.GetComponent<Animator>().SetBool("Hover", false);
		SoundManager.Instance.instruments[patternToAdd.instrumentID].CleanUp();
		//if(LevelData.Instance!=null)if(LevelData.Instance.flowerPannels!=null)LevelData.Instance.flowerPannels[patternToAdd.flowerPannel].ResetOutline();
	}
}
