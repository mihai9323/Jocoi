using UnityEngine;
using System.Collections;

public class TutorialMoveAlone : MonoBehaviour {

    public Movement2D motherSheep;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            motherSheep.StartMovement(
                new Vector3(
                        motherSheep.maxX.position.x,
                        motherSheep.yAmplitude * motherSheep.yMovement.Evaluate(1.0f)+ motherSheep.initialY,
                        motherSheep.transform.position.z
                    )
                );
        }
        else if(Input.GetMouseButtonUp(1))
        {
            motherSheep.StopMovement();
        }
    }
}
