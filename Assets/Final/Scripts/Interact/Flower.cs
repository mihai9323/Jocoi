using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flower : Plant {

    public PatternInfo patternToAdd;
    public int flowerID;
	//public Color trunchiColor; 
	//public int TrunchiNr;

    
    

    private void Start()
    {
       if(LevelData.Instance!=null) LevelData.Instance.flowers.Add(this);


		/*for (int i =0; i<renderer.materials.Length; i++) {
=======
        /*
		for (int i =0; i<renderer.materials.Length; i++) {
>>>>>>> origin/master
			if(i==TrunchiNr) renderer.materials[i].color = trunchiColor;
			else{
				//Color c= patternToAdd.color.RGBtoHSI();
				//c.g = c.g - (float)i * 0.1f;
				//c = c.HSItoRGB();
				renderer.materials[i].color =  patternToAdd.color;

			}
		}
<<<<<<< HEAD
*/

       
    }
    protected override void OnDestroy()
    {
        if (LevelData.Instance != null) LevelData.Instance.flowers.Remove(this);
        base.OnDestroy();
        
    }
    public override void Feed()
    {
        base.Feed();
       
    }
    protected override void LambEat()
    {
       
        base.LambEat();
       
        if(LevelData.Instance!=null)LevelData.Instance.Lamb.GetComponent<PatternAnimator>().AddPattern(this);
        
        if(LevelData.Instance!=null)LevelData.Instance.Lamb.GetComponent<PatternAnimator>().StartAnimation();
        if(KillTheSheep.Instance!=null)KillTheSheep.Instance.currentActions++;
    }
    protected override void LambFinishEat()
    {
        if (LevelData.Instance != null) LevelData.Instance.Lamb.GetComponent<PatternAnimator>().StopAnimation();
        


        base.LambFinishEat();
        
    }

    private void OnMouseEnter()
    {

        SoundManager.Instance.FadeAllDown();
        SoundManager.Instance.instruments[patternToAdd.instrumentID].FlowerSources[patternToAdd.trackID].FadeSoundTo(1.0f);
        this.gameObject.GetComponent<Animator>().SetBool("Hover", true);
        
            
           
    }
    private void OnMouseExit()
    {

        SoundManager.Instance.FadeAllUp();
        if (SoundManager.Instance.instruments[patternToAdd.instrumentID].currentTrack != patternToAdd.trackID) SoundManager.Instance.instruments[patternToAdd.instrumentID].FlowerSources[patternToAdd.trackID].FadeSoundTo(0.0f);
        this.gameObject.GetComponent<Animator>().SetBool("Hover", false);
		SoundManager.Instance.instruments[patternToAdd.instrumentID].CleanUp();
    }

}
