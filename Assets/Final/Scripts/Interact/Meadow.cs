/* Code by Mihai-Ovidiu Anton 10/24/2014
 * 
 * */
using UnityEngine;
using System.Collections;

public class Meadow : InteractableObject {

    private Vector3 lastPosition;

    private int activeClick;

    private void Awake(){
        activeClick = -1;
        lastPosition = transform.position;
    }

    private void Update()
    {
       
        if(activeClick != -1)
        {
            Vector3 currentMouse = Inputs.Instance.GetMouseOnScreen();
            if (Vector3.Distance(currentMouse, lastPosition) > 1.0f)
            {
                lastPosition = currentMouse;
                switch(activeClick){
                    case 0: StartLMB(); break;
                    case 1: StartRMB(); break;
                }
            }
           
        }
    }

    public override void StartLMB()
    {
        LevelData.Instance.currentAnimation = "Jump";
        activeClick = 0;
        LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().StartMoving(
            Inputs.Instance.GetMouseOnScreen(),
            null,
            0,
            "Jump",
			LevelData.Instance.MotherSpeed * LevelData.Instance.JumpSpeedMultiplier
            );
    }

    public override void StartRMB()
    {
        LevelData.Instance.currentAnimation = "Walk";
        activeClick = 1;
        LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().StartMoving(
            Inputs.Instance.GetMouseOnScreen(),
            null,
            1,
            "Walk",
            LevelData.Instance.MotherSpeed
            );
    }

    public override void StopLMB()
    {
        activeClick = -1;
        LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().StopMovement();
    }

    public override void StopRMB()
    {
        activeClick = -1;
        LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().StopMovement();
    }

    public override void StopAllInteractions()
    {
        activeClick = -1;
        LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().StopMovement();
    }
}
