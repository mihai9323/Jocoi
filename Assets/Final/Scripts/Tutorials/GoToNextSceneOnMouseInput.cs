using UnityEngine;
using System.Collections;

public class GoToNextSceneOnMouseInput : GoToNextScene {

    public CountMouseInput cmi;

    public float rmbTime, lmbTime, hoverTime, rmbNr, lmbNr;

    private void Start()
    {
        StartCoroutine(refreshTest());
    }
    private IEnumerator refreshTest()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);
            if (cmi.maxRMBTime >= rmbTime && cmi.maxLMBTime >= lmbTime && cmi.RMBUp >= rmbNr && cmi.LMBUp >= lmbNr)
            {
                goToNextScene();
            }
        }
    }
}
