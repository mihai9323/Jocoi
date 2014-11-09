using UnityEngine;
using System.Collections;

public class GoToNextSceneOnInput : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //Application.LoadLevel(Application.loadedLevel + 1);
            AutoFade.LoadLevel(Application.loadedLevel+1, 10,10, Color.black);
        }
	}
}
