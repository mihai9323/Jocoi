using UnityEngine;
using System.Collections;

public class TutorialMoveWithLamb : MonoBehaviour {

    public Movement2D motherSheep;
    public Transform destinationX;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            motherSheep.StartMovement(
                new Vector3(
                        destinationX.position.x,
                        WorldCurve.Instance.amplitude * WorldCurve.Instance.worldCurve.Evaluate(1.0f)+ motherSheep.initialY,
                        motherSheep.transform.position.z
                    )
                );
			//LevelData.Instance.Lamb.GetComponent<Lamb>().StopAllInteractions();
				
				
        }
        else if(Input.GetMouseButtonUp(0))
        {
            motherSheep.StopMovement();
        }
    }
}
