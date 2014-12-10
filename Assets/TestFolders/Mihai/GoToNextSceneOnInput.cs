using UnityEngine;
using System.Collections;

public class GoToNextSceneOnInput : GoToNextScene
{


	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Space))
        {
			GameData.LoadFlowersInMemory();
            goToNextScene();
			
        }
	}
}
