using UnityEngine;
using System.Collections;

public class SnapToCurve : MonoBehaviour {

    private float yAmplitude;
    private Transform minX, maxX;
    private float initialY;


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
	}
	
	
}
