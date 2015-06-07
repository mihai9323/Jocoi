using UnityEngine;
using System.Collections;

public class GoToNextScene : MonoBehaviour {

    public enum ParameterType
    {
        String, Int
    }
    public ParameterType parameterType = ParameterType.String;
    public string SceneName;
    public int SceneNumber;

    public float fadeInTime = .5f;
    public float fadeOutTime = .5f;

    public Color fadeColor = Color.black;

	// Use this for initialization
	void Start () {
	
	}
	
	
    public void goToNextScene()
    {
        StopAllCoroutines();
        switch (parameterType)
        {
            case ParameterType.String: AutoFade.LoadLevel(SceneName, fadeInTime, fadeOutTime, fadeColor); break;
            case ParameterType.Int: AutoFade.LoadLevel(SceneNumber, fadeInTime, fadeOutTime, fadeColor); break;
        }
    }
}
