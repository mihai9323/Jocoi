using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flower : Plant {

    public PatternInfo patternToAdd;
    
    public GameObject movingToPannelFeedback;
    public int flowerID;
    /// <summary>
    /// The flower pannel.
    /// The ID of the flowerPannel as the key of the array found in LevelData holding this UI elements
    /// </summary>
    
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
		if(movingToPannelFeedback !=null){
			GameObject gob = Instantiate(movingToPannelFeedback,transform.position,movingToPannelFeedback.transform.rotation) as GameObject;
			gob.AddComponent<MoveToPanel>().moveToPanel(patternToAdd.flowerPannel);
		}
        if(LevelData.Instance!=null)LevelData.Instance.Lamb.GetComponent<PatternAnimator>().AddPattern(this);
        
        if(LevelData.Instance!=null)LevelData.Instance.Lamb.GetComponent<PatternAnimator>().StartAnimation();
        if(KillTheSheep.Instance!=null)KillTheSheep.Instance.currentActions++;
        if(LevelData.Instance!=null) {
		//	LevelData.Instance.flowerPannels[patternToAdd.flowerPannel].SetTemporaryOutline(this.patternToAdd.color);
			if(LevelData.Instance.flowerPannels!=null)LevelData.Instance.flowerPannels[patternToAdd.flowerPannel].SetFlowerImageColor(this.patternToAdd.color);
			if(LevelData.Instance.flowerPannels!=null)LevelData.Instance.flowerPannels[patternToAdd.flowerPannel].trackId = this.patternToAdd.trackID;
		}
    }
    protected override void LambFinishEat()
    {
        if (LevelData.Instance != null) LevelData.Instance.Lamb.GetComponent<PatternAnimator>().StopAnimation();
       
        base.LambFinishEat();
        
    }

    private void OnMouseEnter()
    {
		if(LevelData.Instance!=null)if(LevelData.Instance.tutorialMode) LevelData.Instance.loggedActions++;
        SoundManager.Instance.FadeAllDown();
        SoundManager.Instance.instruments[patternToAdd.instrumentID].FlowerSources[patternToAdd.trackID].FadeSoundTo(1.0f);
        this.gameObject.GetComponent<Animator>().SetBool("Hover", true);
		if(LevelData.Instance!=null)if(LevelData.Instance.flowerPannels!=null) LevelData.Instance.flowerPannels[patternToAdd.flowerPannel].SetTemporaryOutline(this.patternToAdd.color);
            
           
    }
    private void OnMouseExit()
    {

        SoundManager.Instance.FadeAllUp();
        if (SoundManager.Instance.instruments[patternToAdd.instrumentID].currentTrack != patternToAdd.trackID) SoundManager.Instance.instruments[patternToAdd.instrumentID].FlowerSources[patternToAdd.trackID].FadeSoundTo(0.0f);
        this.gameObject.GetComponent<Animator>().SetBool("Hover", false);
		SoundManager.Instance.instruments[patternToAdd.instrumentID].CleanUp();
		if(LevelData.Instance!=null)if(LevelData.Instance.flowerPannels!=null)LevelData.Instance.flowerPannels[patternToAdd.flowerPannel].ResetOutline();
    }

}
