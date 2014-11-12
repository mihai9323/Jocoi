using UnityEngine;
using System.Collections;

public class GoToFPR : InteractableObject {

    public Transform targetPosition;
    public int riverLevel;

    public override void StartLMB()
    {
        
    }

    public override void StartRMB()
    {
		//throw new System.NotImplementedException();
		LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().StartMoving(
			targetPosition.position,
			GoToRiverStage,
			0,
			"Walk",
			LevelData.Instance.MotherSpeed,
			2
			);
    }

    public override void StopLMB()
    {
       // throw new System.NotImplementedException();
    }

    public override void StopRMB()
    {
       // throw new System.NotImplementedException();
    }

    public override void StopAllInteractions()
    {
        //throw new System.NotImplementedException();
    }

    private void GoToRiverStage()
    {
        Puzzle.Instance.gameObject.SetActive(false);
        //AutoFade.LoadLevel(riverLevel,.5f,1.0f,Color.black);
        Application.LoadLevel(riverLevel);
    }
}
