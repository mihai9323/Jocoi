using UnityEngine;
using System.Collections;

public class TutorialMoveAlone : MonoBehaviour {

    public Movement2D motherSheep;
    public Transform destinationX;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            motherSheep.StartMovement(
                new Vector3(
                        destinationX.position.x,
                        WorldCurve.Instance.amplitude * WorldCurve.Instance.worldCurve.Evaluate(1.0f)+ motherSheep.initialY,
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
