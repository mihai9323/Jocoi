using UnityEngine;
using System.Collections;
using System;
public class Lamb : InteractableObject {

    public PatternAnimator patternAnimator;
    private MoveToPosition mtp;

    public Transform motherBelly;
    public Transform motherHead;

    private bool isEating;
    public float distanceToMother = 2.0f;
    [Range(0.03f,2.0f)]
    public float refreshFrequency = .4f;
    private void Awake()
    {
        isEating = false;
        if (this.gameObject.GetComponent<MoveToPosition>() == null) mtp = this.gameObject.AddComponent<MoveToPosition>();
        else mtp = this.gameObject.GetComponent<MoveToPosition>();
    }
    private void Start()
    {
        StartCoroutine("FollowMother");
    }
    private void PlayPatternAnimation()
    {

       // isEating = true;
        StopCoroutine("FollowMother");
        this.gameObject.GetComponent<MoveToPosition>().StartMoving(motherBelly.position, StartAnimation, LevelData.Instance.LambSpeed, 2f);
        patternAnimator.StartAnimation();
        LevelData.Instance.Lamb.GetComponent<MoveToPosition>().anim.SetBool("Breastfeed", true);
        
    }
    private void StartAnimation()
    {
        patternAnimator.StartAnimation();
        isEating = true;
    }
    private void StopPatternAnimation()
    {
        isEating = false;
        patternAnimator.StopAnimation();
        LevelData.Instance.Lamb.GetComponent<MoveToPosition>().anim.SetBool("Breastfeed", false);
       
    }

    public override void StartLMB()
    {
      if(!isEating){
        LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().StartMoving(
            transform.position+new Vector3(-1,0,2),
            PlayPatternAnimation,
            0,
            "Walk",
            LevelData.Instance.MotherSpeed,
            2.5f);
        }else{
            
            StopPatternAnimation();
        }       
    }

    public override void StartRMB()
    {
        LevelData.Instance.cam.GetComponent<CameraZoom>().StartZoom(this.gameObject);
    }

    public override void StopLMB()
    {
        
        
        
    }

    public override void StopRMB()
    {
        LevelData.Instance.cam.GetComponent<CameraZoom>().StopZoom();
    }

    public override void StopAllInteractions()
    {
        Debug.Log("stopLamb");
        StopPatternAnimation();
        LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().StopMovement();
        LevelData.Instance.cam.GetComponent<CameraZoom>().StopZoom();
        this.GetComponent<MoveToPosition>().StopMovement();
        StartCoroutine("FollowMother"); 
    }

    private IEnumerator FollowMother()
    {
        bool ok = true;
        while (true)
        {
            ok = true;
            if (Inputs.Instance.ActiveObject == this) ok = false;
            if (Inputs.Instance.ActiveObject != null) 
                if ((IsSameOrSubclass(typeof(Plant),Inputs.Instance.ActiveObject.GetType())|| IsSameOrSubclass(typeof(SheepInHerd),Inputs.Instance.ActiveObject.GetType()))  && Inputs.Instance.activeMode == 0) ok = false;
            if (ok )
            {
               
                float m = 1;
				if (LevelData.Instance.currentAnimation == "Jump") m = LevelData.Instance.JumpSpeedMultiplier;
                this.gameObject.GetComponent<MoveToPosition>().StartMoving(LevelData.Instance.MotherSheep.transform,
                                                                           null,
                                                                           (int)(m-1),
                                                                           LevelData.Instance.currentAnimation,
                                                                           LevelData.Instance.LambSpeed*m,
                                                                           distanceToMother);

            }
           
           
            yield return new WaitForSeconds(refreshFrequency);
        }
    }
    public bool IsSameOrSubclass(Type potentialBase, Type potentialDescendant)
    {
        return potentialDescendant.IsSubclassOf(potentialBase)
               || potentialDescendant == potentialBase;
    }
}
