using UnityEngine;
using System.Collections;

public class textureOffset : MonoBehaviour {


	public Vector2 Scroll = new Vector2 (0.00f , 0.2f);
	Vector2 Offset = new Vector2 (0f, 0f);
	
	void Update () {
		Offset +=  Scroll * Time.deltaTime;
		renderer.material.SetTextureOffset ("_MainTex", Offset);
	}
}



