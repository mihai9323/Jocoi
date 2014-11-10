using UnityEngine;
using System.Collections;

public class GoOnPath : InteractableObject {

    public Transform targetPosition;
    public Transform targetPosition2;
    public float fadeTimeOut = 5.0f;
    public float fadeTimeIn = 3.0f;
    private int countSheep;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void StartLMB()
    {
        Inputs.Instance.canInteract = false;
        StartCoroutine(callAllSheep());
     
    }
    private IEnumerator callAllSheep()
    {
        
       

       
        LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().StartMoving(
               targetPosition,
               FinishGame,
               0,
               "Walk",
               LevelData.Instance.MotherSpeed,
               1.5f
           );
        yield return new WaitForSeconds(.2f);
       
        LevelData.Instance.herdTarget.canMove = false;

        foreach (HelperSheep sh in LevelData.Instance.HelperSheep)
        {
            sh.GetComponent<MoveToPosition>().StartMoving(
               targetPosition,
               FinishGame,
               0,
               "Walk",
               LevelData.Instance.MotherSpeed,
               1.5f
           );
        }

    }
    private void FinishGame()
    {
        countSheep++;
        if (countSheep >= 4)
        {
            AutoFade.LoadLevel(3, fadeTimeOut, fadeTimeIn, Color.white);

            LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().StartMoving(
              targetPosition2,
              null,
              0,
              "Walk",
              LevelData.Instance.MotherSpeed,
              1.5f
             );
            

            LevelData.Instance.herdTarget.canMove = false;

            foreach (HelperSheep sh in LevelData.Instance.HelperSheep)
            {
                sh.GetComponent<MoveToPosition>().StartMoving(
                   targetPosition2,
                   null,
                   0,
                   "Walk",
                   LevelData.Instance.MotherSpeed,
                   1.5f
               );
            }

        }
    }


    public override void StartRMB()
    {
        Inputs.Instance.canInteract = false;
        StartCoroutine(callAllSheep());
    }

    public override void StopLMB()
    {
        //throw new System.NotImplementedException();
    }

    public override void StopRMB()
    {
       // throw new System.NotImplementedException();
    }

    public override void StopAllInteractions()
    {
       // throw new System.NotImplementedException();
    }
}
