﻿using UnityEngine;
using System.Collections;

public class Plant : InteractableObject {

    public string DieAnimation = "Die";
    public string GrowAnimation = "Grow";
    public string HoverAnimation = "Hover";
    public string pickUpAnimation;
    public string eatAnimation;
    public string feedAnimation;
	public string giveAnimation;
	
    public Transform PlantInMouth; //model object
    private Transform plantInMouth; //real object

    public AudioClip DieSound;
    public AudioClip GrowSound;
    public AudioClip BeEatenSound;
    public AudioClip BePickedSound;
    public AudioClip LambFedSound;
    public AudioClip MotherEatSound;

    public Animator anim;
    public AudioSource audioSource;
    internal Spawner spawner;
    public float clickCoolDown = .5f;
    
    private bool walkingToTarget;
    private void Awake()
    {
        walkingToTarget = false;   
        if (anim == null)
        {
            if (this.gameObject.GetComponent<Animator>() != null)
            {
                anim = this.gameObject.GetComponent<Animator>();
            }
            else anim = this.gameObject.AddComponent<Animator>();
        }
        if (audioSource == null)
        {
            if (this.gameObject.GetComponent<AudioSource>() != null)
            {
                audioSource = this.gameObject.GetComponent<AudioSource>();
            }
            else audioSource = this.gameObject.AddComponent<AudioSource>();
        }
        StartGorw();
        
        
    }
  
    
    public override void StartLMB()
    {
        LevelData.Instance.currentAnimation = "Jump";
        LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().StartMoving(
                transform.position,
                Feed,
                0,
			"Jump",
                LevelData.Instance.MotherSpeed,
                1.8f

            );
        LevelData.Instance.Lamb.GetComponent<MoveToPosition>().StartMoving(
            LevelData.Instance.Lamb.GetComponent<Lamb>().motherHead.transform,
            null,
            1,
			"Jump",
            LevelData.Instance.LambSpeed ,
           2f
            );
        walkingToTarget = true;
        Inputs.Instance.canInteract = false;
        Invoke("CheckInteractions", clickCoolDown);
    }
    
    public override void StartRMB()
    {
        walkingToTarget = true;
        
        LevelData.Instance.currentAnimation = "Walk";
        LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().StartMoving(
               transform.position,
               Eat,
               0,
               "Walk",
               LevelData.Instance.MotherSpeed,
               1.8f

           );
        Inputs.Instance.canInteract = false;
        Invoke("CheckInteractions", clickCoolDown);
    }

    public override void StopLMB()
    {
        
    }

    public override void StopRMB()
    {
       
    }

    public override void StopAllInteractions()
    {
        
    }

    public void StartGorw()
    {
        if (GrowSound != null)
        {
            audioSource.clip = GrowSound;
            audioSource.volume = 0.5f;
            audioSource.Play();
        }
        anim.SetTrigger(GrowAnimation);
        Invoke("FinishGrow", 1.5f);
    }
    private void FinishGrow()
    {
        
    }
    public void FinishedGrowing()
    {
        audioSource.Stop();
    }
    public void StartDie()
    {
        if (DieSound != null)
        {
            if(audioSource!=null){
	            audioSource.clip = DieSound;
	            audioSource.volume = 1f;
				audioSource.Play();
            }
        }
        if(anim!=null)anim.SetBool("Die", true);
        Invoke("FinishedDying", 1.0f);
    }
    public void FinishedDying()
    {
        spawner.occupied = false;
        audioSource.Stop();
        Destroy(this.gameObject);
    }
    public virtual void Eat()
    {
        //Debug.Log("eat");
        walkingToTarget = false;
        LevelData.Instance.MotherSheep.transform.FaceObjectOnAxis(this.transform, new Vector3(0, 1, 0));
        LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().anim.SetBool(eatAnimation,true);
        Inputs.Instance.canInteract = false;
        if (MotherEatSound != null)
        {
            AudioSource.PlayClipAtPoint(MotherEatSound, transform.position);
        }
        Invoke("FinishEat", 2.0f);
    }
    public virtual void Feed()
    {
        walkingToTarget = false;
        Inputs.Instance.canInteract = false;
        LevelData.Instance.MotherSheep.transform.FaceObjectOnAxis(this.transform, new Vector3(0, 1, 0));
        Invoke ("FinishPickUp", 1.0f);
        LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().anim.SetBool(pickUpAnimation,true);
       // Debug.Log("feed");
        if (BePickedSound != null)
        {
            AudioSource.PlayClipAtPoint(BePickedSound, transform.position);
        }
       
    }
    private void FinishPickUp(){
    
		plantInMouth = Instantiate(PlantInMouth, LevelData.Instance.Lamb.GetComponent<Lamb>().motherHead.position, LevelData.Instance.MotherSheep.transform.rotation) as Transform;
		plantInMouth.parent = LevelData.Instance.Lamb.GetComponent<Lamb>().motherHead;
		Renderer[] renders =  this.gameObject.GetComponentsInChildren<Renderer>();
		foreach (Renderer r in renders) {
			r.enabled = false;
				}
//		this.gameObject.renderer.enabled = false;
        LevelData.Instance.Lamb.GetComponent<MoveToPosition>().StartMoving(
			LevelData.Instance.Lamb.GetComponent<Lamb>().motherHead.transform.position + LevelData.Instance.Lamb.GetComponent<Lamb>().motherHead.transform.right*2.0f,
			this.LambEat,
			1,
			"Jump",
			LevelData.Instance.LambSpeed*2,
			4f
			);
		LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().anim.SetBool(pickUpAnimation,false);
		
    }
    protected virtual void OnDestroy()
    {

        if (Inputs.Instance.ActiveObject == this)
        {
            Inputs.Instance.ActiveObjectDestroyed();
        }
    }
    protected virtual void LambEat()
    {
		LevelData.Instance.Lamb.GetComponent<MoveToPosition>().anim.SetBool(feedAnimation,true);
        LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().anim.SetBool(giveAnimation,true);
        
        plantInMouth.transform.position = LevelData.Instance.LambHead.transform.position;
		plantInMouth.transform.rotation = LevelData.Instance.LambHead.transform.rotation;
        plantInMouth.transform.parent = LevelData.Instance.LambHead.transform;
        
        LevelData.Instance.MotherSheep.transform.FaceObjectOnAxis(LevelData.Instance.LambHead, new Vector3(0, 1, 0));
        
        LevelData.Instance.Lamb.transform.FaceObjectOnAxis(LevelData.Instance.Lamb.GetComponent<Lamb>().motherHead.position, new Vector3(0, 1, 0));
        if (LevelData.Instance.Lamb.GetComponent<Animator>() != null)
        {
         
        }
        if (LambFedSound != null)
        {
            AudioSource.PlayClipAtPoint(LambFedSound, transform.position);
        }
        Invoke("LambFinishEat", 1.8f);
    }
    protected virtual void LambFinishEat()
    {
        Inputs.Instance.canInteract = true;
        Destroy(plantInMouth.gameObject);
		LevelData.Instance.Lamb.GetComponent<MoveToPosition>().anim.SetBool(feedAnimation,false);
		LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().anim.SetBool(giveAnimation,false);
        Destroy(this.gameObject, 0.1f);
    }
    protected virtual void FinishEat()
    {
		
		LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().anim.SetBool(eatAnimation,false);
        Inputs.Instance.canInteract = true;
       
		Destroy(this.gameObject, 0.1f);
    }

    private void CheckInteractions()
    {
       
            if (walkingToTarget) Inputs.Instance.canInteract = true;
           
      
    }
   
}
