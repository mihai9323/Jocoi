using UnityEngine;
using System.Collections;

public class SheepInHerd : InteractableObject {

    private MoveToPosition mtp;
    private bool moving;
    private bool weatherStartedChanging;

    private int inPosition;
    public float clickCoolDown = 1.5f;
    private bool walkingToTarget;
	// Use this for initialization
	void Awake () {
        weatherStartedChanging = false;
        moving = false;
        inPosition = 0;
        if (GetComponent<MoveToPosition>() != null)
        {
            mtp = GetComponent<MoveToPosition>();
        }
        else mtp = gameObject.AddComponent<MoveToPosition>();
        StartCoroutine(checkDistanceFromTarget());
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    private IEnumerator checkDistanceFromTarget()
    {
        yield return new WaitForSeconds(Random.value);
        while (true)
        {
            if(!moving){
                if (Vector3.Distance(transform.position, LevelData.Instance.herdTarget.transform.position) > 3.0f)
                {
                    MoveToNewPosition(LevelData.Instance.herdTarget.transform.position);
                }
            }
            yield return new WaitForSeconds(1f);

        }
    }
    public void MoveToNewPosition(Vector3 position)
    {
        moving = true;

        mtp.StartMovingOnPath(position, ReachedDestination, 0, "Walk", Random.Range(.8f, 1.2f) * LevelData.Instance.MotherSpeed,2.5f);
    }
    private void ReachedDestination()
    {
        moving = false;
    }

    public override void StartLMB()
    {
        StartCoroutine(callAllSheep());
        weatherStartedChanging = false;
    }
    private IEnumerator callAllSheep()
    {
        walkingToTarget = true;
        Inputs.Instance.canInteract = false;
        Invoke("CheckInteractions", clickCoolDown);

        inPosition = 0;
        LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().StartMoving(
               transform.position,
               MotherCuddle,
               0,
               "Walk",
               LevelData.Instance.MotherSpeed,
               1.5f
           );
        yield return new WaitForSeconds(.2f);
        LevelData.Instance.Lamb.GetComponent<MoveToPosition>().StartMoving(
                transform.position,
                LambCuddle,
                0,
                "Walk",
                LevelData.Instance.LambSpeed,
                1.5f
            );
        LevelData.Instance.herdTarget.canMove = false;
        foreach (SheepInHerd sh in LevelData.Instance.Sheep)
        {
            sh.GetComponent<MoveToPosition>().StopMovement();
            sh.GetComponent<MoveToPosition>().anim.SetBool("Lay", true);
            yield return new WaitForSeconds(2 * Random.Range(.3f,.6f));
        }
       
    }
    public override void StartRMB()
    {
        
    }

    public override void StopLMB()
    {
        
    }

    public override void StopRMB()
    {
        
    }

    public override void StopAllInteractions()
    {
        walkingToTarget = false;
        inPosition = 0;
        StopAllCoroutines();
        LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().anim.SetBool("Lay", false);
        LevelData.Instance.Lamb.GetComponent<MoveToPosition>().anim.SetBool("Lay", false);
        LevelData.Instance.herdTarget.canMove = true;
        foreach (SheepInHerd sh in LevelData.Instance.Sheep)
        {
            sh.GetComponent<MoveToPosition>().StopMovement();
            sh.GetComponent<MoveToPosition>().anim.SetBool("Lay", false);
        }
        if(weatherStartedChanging) WeatherCycle.Instance.ChangeTheWeather();
        weatherStartedChanging = false;
    }

    private void StartCuddle()
    {
        inPosition++;
        
        //both mother and child ar in cuddling position
        if (inPosition >1)
        {
            walkingToTarget = false;
            WeatherCycle.Instance.FadeWeathers();
            weatherStartedChanging = true;
            Inputs.Instance.canInteract = true;
        }
       
       
    }
    private void MotherCuddle()
    {

        LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().anim.SetBool("Lay", true);
        StartCuddle();
    }
    private void LambCuddle()
    {
        LevelData.Instance.Lamb.GetComponent<MoveToPosition>().anim.SetBool("Lay", true);
        StartCuddle();
    }

    private void CheckInteractions()
    {

        if (walkingToTarget) Inputs.Instance.canInteract = true;


    }
}
