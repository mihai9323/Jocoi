using UnityEngine;
using System.Collections;

public class GoToNextSceneOnPosition : MonoBehaviour {

    public enum ParameterType
    {
        String,Int
    }
    
    public ParameterType parameterType = ParameterType.String;
    public string SceneName;
    public int SceneNumber;

    public Transform minPosition;
    public Transform maxPosition;
    public Transform CheckedObject;

    public float fadeInTime = .5f;
    public float fadeOutTime = .5f;

    public Color fadeColor = Color.black;

    private void Start()
    {

        StartCoroutine(CheckEndState());
    }
    private IEnumerator CheckEndState()
    {
        while (true)
        {
            yield return new WaitForSeconds(.4f);
            if(
                CheckedObject.position.x>minPosition.position.x &&
                CheckedObject.position.x<maxPosition.position.x &&
                CheckedObject.position.y<maxPosition.position.y &&
                CheckedObject.position.y>minPosition.position.y &&
                CheckedObject.position.z>minPosition.position.z &&
                CheckedObject.position.z < maxPosition.position.z)
            {
                switch (parameterType)
                {
                    case ParameterType.String:  AutoFade.LoadLevel(SceneName,   fadeInTime, fadeOutTime, fadeColor); break;
                    case ParameterType.Int:     AutoFade.LoadLevel(SceneNumber, fadeInTime, fadeOutTime, fadeColor); break;
                }
            }
        }
    }

}
