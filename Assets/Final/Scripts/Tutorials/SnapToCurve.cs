using UnityEngine;
using System.Collections;

public class SnapToCurve : MonoBehaviour {

    private float yAmplitude;
    private Transform minX, maxX;
    private float initialY;
	
	public bool RotateToCurve = true;

	void Start () {
        minX = WorldCurve.Instance.minX;
        maxX = WorldCurve.Instance.maxX;
        yAmplitude = WorldCurve.Instance.amplitude;
        initialY = minX.position.y;
        float ct = (transform.position.x - minX.position.x) / (maxX.position.x - minX.position.x);
        float x = Mathf.Lerp(minX.position.x, maxX.position.x, ct);
        transform.position = new Vector3(
                x,
                WorldCurve.Instance.worldCurve.Evaluate(ct) * yAmplitude + initialY,
                transform.position.z
                );
        if(RotateToCurve) RotateObject(ct);       
	}
	
	private void RotateObject(float ct){
		
		Vector3 currPos = new Vector3(Mathf.Lerp(minX.position.x,maxX.position.x,ct),WorldCurve.Instance.worldCurve.Evaluate(ct) * yAmplitude + initialY,transform.position.z);
		Vector3 nextPos = new Vector3(Mathf.Lerp(minX.position.x,maxX.position.x,ct+Time.fixedDeltaTime),WorldCurve.Instance.worldCurve.Evaluate(ct+Time.fixedDeltaTime) * yAmplitude + initialY,transform.position.z);
		
		Vector3 direction = (nextPos - currPos).normalized;
		float dot = Vector3.Dot(direction,Vector3.right);
		
		
		transform.rotation = Quaternion.Euler(Mathf.Sign(currPos.y - nextPos.y) * Mathf.Acos(dot)* 180/ Mathf.PI,transform.rotation.eulerAngles.y, 0);
		
		
	}
	
	
}
