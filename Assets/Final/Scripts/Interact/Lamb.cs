using UnityEngine;
using System.Collections;

public class Lamb : InteractableObject {

    public PatternAnimator patternAnimator;
    private MoveToPosition mtp;

    public Transform motherBelly;
    public Transform motherHead;

    private bool isEating;
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

        isEating = true;
        StopCoroutine("FollowMother");
        this.gameObject.GetComponent<MoveToPosition>().StartMoving(motherBelly.position, patternAnimator.StartAnimation, LevelData.Instance.LambSpeed, 1f);
        patternAnimator.StartAnimation();
        
    }
    private void StopPatternAnimation()
    {
        isEating = false;
        patternAnimator.StopAnimation();
       
    }

    public override void StartLMB()
    {
      if(!isEating)
        LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().StartMoving(transform.position+new Vector3(-1,0,2), PlayPatternAnimation, LevelData.Instance.MotherSpeed, 2.3f);
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
            if (Inputs.Instance.ActiveObject != null) if (Inputs.Instance.ActiveObject.GetType() == typeof(Plant) && Inputs.Instance.activeMode == 0) ok = false;
            if (ok )
            {
               
                float m = 1;
                if (LevelData.Instance.currentAnimation == "Jump") m = 2;
                this.gameObject.GetComponent<MoveToPosition>().StartMoving(LevelData.Instance.MotherSheep.transform,
                                                                           null,
                                                                           (int)(m-1),
                                                                           LevelData.Instance.currentAnimation,
                                                                           LevelData.Instance.LambSpeed*2,
                                                                           3.0f);

            }
           
           
            yield return new WaitForSeconds(1.0f);
        }
    }

}
