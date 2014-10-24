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
        
        while (true)
        {
            if (Inputs.Instance.ActiveObject != this)
            {
                this.gameObject.GetComponent<MoveToPosition>().StartMoving(LevelData.Instance.MotherSheep.transform.position, null, LevelData.Instance.LambSpeed, 3.0f);

            }
           
           
            yield return new WaitForSeconds(1.0f);
        }
    }

}
