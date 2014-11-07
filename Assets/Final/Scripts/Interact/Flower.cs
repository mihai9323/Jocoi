﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flower : Plant {

    public PatternInfo patternToAdd;
	public Color trunchiColor; 
	public int TrunchiNr;
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
        GameData.addToMemory(patternToAdd);
        LevelData.Instance.Lamb.GetComponent<PatternAnimator>().StartAnimation();
       
    }
    protected override void LambFinishEat()
    {
        LevelData.Instance.Lamb.GetComponent<PatternAnimator>().StopAnimation();
        SoundManager.Instance.FlowerSources[patternToAdd.flowerSource].fixedLevel = SoundManager.Instance.FlowerSources[patternToAdd.flowerSource].IncreaseVolume(SoundManager.Instance.FlowerSources[patternToAdd.flowerSource].fixedLevel);
        base.LambFinishEat();
        
    }

    private void CheckForDistance()
    {
        
            if (transform.position.SquaredDistance(LevelData.Instance.MotherSheep.transform.position) < 25.0f)
            {
                SoundManager.Instance.FlowerSources[patternToAdd.flowerSource].volumeLevel = SoundManager.Instance.FlowerSources[patternToAdd.flowerSource].IncreaseVolume(SoundManager.Instance.FlowerSources[patternToAdd.flowerSource].fixedLevel);
            }
            else
            {
                SoundManager.Instance.FlowerSources[patternToAdd.flowerSource].volumeLevel = SoundManager.Instance.FlowerSources[patternToAdd.flowerSource].fixedLevel;
            }
           
    }

}
