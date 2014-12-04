using UnityEngine;
using System.Collections;

public class GoToNextSceneOnNumberOfActions : GoToNextScene {
	
	private bool ok = false;
	
	
	// Update is called once per frame
	void Update () {
		if(!ok)if(LevelData.Instance!=null) if(LevelData.Instance.tutorialMode) if(LevelData.Instance.loggedActions>4){ ok = true; goToNextScene();}
	}
}
