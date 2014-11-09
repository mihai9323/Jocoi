using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flower : Plant {

    public PatternInfo patternToAdd;
	public Color trunchiColor; 
	public int TrunchiNr;
    
    public int flowerType; 
    

    private void Start()
    {
        LevelData.Instance.flowers.Add(this);
		for (int i =0; i<renderer.materials.Length; i++) {
			if(i==TrunchiNr) renderer.materials[i].color = trunchiColor;
			else{
				//Color c= patternToAdd.color.RGBtoHSI();
				//c.g = c.g - (float)i * 0.1f;
				//c = c.HSItoRGB();
				renderer.materials[i].color =  patternToAdd.color;

			}
		}
        InvokeRepeating("CheckForDistance", .0f,.3f);
    }
    protected override void OnDestroy()
    {
        LevelData.Instance.flowers.Remove(this);
        base.OnDestroy();
        
    }
    public override void Feed()
    {
        base.Feed();
       
    }
    protected override void LambEat()
    {
       
        base.LambEat();
       
        LevelData.Instance.Lamb.GetComponent<PatternAnimator>().AddPattern(patternToAdd);
        GameData.addToMemory(this);
        LevelData.Instance.Lamb.GetComponent<PatternAnimator>().StartAnimation();
       
    }
    protected override void LambFinishEat()
    {
        LevelData.Instance.Lamb.GetComponent<PatternAnimator>().StopAnimation();
        SoundManager.Instance.FlowerSources[patternToAdd.flowerSourceType].fixedLevel = SoundManager.Instance.FlowerSources[patternToAdd.flowerSourceType].IncreaseVolume(SoundManager.Instance.FlowerSources[patternToAdd.flowerSourceType].fixedLevel);
        SoundManager.Instance.FlowerSources[patternToAdd.flowerSourceColor].fixedLevel = SoundManager.Instance.FlowerSources[patternToAdd.flowerSourceColor].IncreaseVolume(SoundManager.Instance.FlowerSources[patternToAdd.flowerSourceColor].fixedLevel);
        base.LambFinishEat();
        
    }

    private void OnMouseOver()
    {
        
           
        SoundManager.Instance.FlowerSources[patternToAdd.flowerSourceType].volumeLevel = SoundManager.Instance.FlowerSources[patternToAdd.flowerSourceType].IncreaseVolume(SoundManager.Instance.FlowerSources[patternToAdd.flowerSourceType].fixedLevel);
        SoundManager.Instance.FlowerSources[patternToAdd.flowerSourceColor].volumeLevel = SoundManager.Instance.FlowerSources[patternToAdd.flowerSourceColor].IncreaseVolume(SoundManager.Instance.FlowerSources[patternToAdd.flowerSourceColor].fixedLevel);
        this.gameObject.GetComponent<Animator>().SetBool("Hover", true);
               
            
           
    }
    private void OnMouseExit()
    {
        SoundManager.Instance.FlowerSources[patternToAdd.flowerSourceType].volumeLevel = SoundManager.Instance.FlowerSources[patternToAdd.flowerSourceType].fixedLevel;
        SoundManager.Instance.FlowerSources[patternToAdd.flowerSourceColor].volumeLevel = SoundManager.Instance.FlowerSources[patternToAdd.flowerSourceColor].fixedLevel;
        this.gameObject.GetComponent<Animator>().SetBool("Hover", false);
    }

}
