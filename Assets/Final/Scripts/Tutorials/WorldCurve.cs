using UnityEngine;
using System.Collections;

public class WorldCurve : MonoBehaviour {
    
    public static WorldCurve Instance;
    public AnimationCurve worldCurve;
    public float amplitude;

    public Transform minX, maxX;

    // Use this for initialization
	void Awake () {
        Instance = this;
	}
	

}
