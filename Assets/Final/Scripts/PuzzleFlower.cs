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
				
				HighGrass.SetActive(false);
				TallGrass.SetActive(false);
				MediumGrass.SetActive(false);
				LowGrass.SetActive(false);
				Flower.SetActive(false);
				
				switch(value){
					case GrassState.HighGrass: HighGrass.SetActive(true); activeObject = HighGrass; break;
					case GrassState.TallGrass: TallGrass.SetActive(true); activeObject = TallGrass; break;
					case GrassState.MediumGrass: MediumGrass.SetActive(true); activeObject = MediumGrass; break;
					case GrassState.LowGrass: LowGrass.SetActive(true); activeObject = LowGrass; break;
				    case GrassState.Flower: Flower.SetActive(true); activeObject = Flower; break;
				}
			}
		}
		get{
			return _grassState;
		}
	}
	private GrassState _grassState;
	
	public GameObject HighGrass,TallGrass,MediumGrass,LowGrass,Flower;
	private GameObject activeObject;
	
	
	
	
	
	
	
	
	
	
	
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
			if(this.patternToAdd.trackID == LevelData.Instance.flowerPannels[this.patternToAdd.instrumentID].trackId){
				LevelData.Instance.flowerPannels[this.patternToAdd.instrumentID].SetFlowerImageColor(this.patternToAdd.color);
				Puzzle.Instance.CheckCompleted();
			}else{
				this.grassState = GrassState.HighGrass;
			}
		}
		Destroy(this.gameObject, 0.1f);
	}
	
	
	protected override void OnMouseEnter()
	{
		if(LevelData.Instance!=null)if(LevelData.Instance.tutorialMode) LevelData.Instance.loggedActions++;
		SoundManager.Instance.FadeAllDown(patternToAdd.instrumentID);
		SoundManager.Instance.instruments[patternToAdd.instrumentID].FlowerSources[patternToAdd.trackID].FadeSoundTo(1.0f);
		activeObject.GetComponent<Animator>().SetBool("Hover", true);
		if(LevelData.Instance!=null)if(LevelData.Instance.flowerPannels!=null) LevelData.Instance.flowerPannels[patternToAdd.flowerPannel].SetTemporaryOutline(this.patternToAdd.color);
		
		
	}
	protected override void OnMouseExit()
	{
		
		SoundManager.Instance.FadeAllUp();
		if (SoundManager.Instance.instruments[patternToAdd.instrumentID].currentTrack != patternToAdd.trackID) SoundManager.Instance.instruments[patternToAdd.instrumentID].FlowerSources[patternToAdd.trackID].FadeSoundTo(0.0f);
		activeObject.GetComponent<Animator>().SetBool("Hover", false);
		SoundManager.Instance.instruments[patternToAdd.instrumentID].CleanUp();
		if(LevelData.Instance!=null)if(LevelData.Instance.flowerPannels!=null)LevelData.Instance.flowerPannels[patternToAdd.flowerPannel].ResetOutline();
	}
}
