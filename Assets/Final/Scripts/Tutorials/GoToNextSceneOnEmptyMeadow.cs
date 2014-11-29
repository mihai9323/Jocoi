using UnityEngine;
using System.Collections;

public class GoToNextSceneOnEmptyMeadow : GoToNextScene
{

   
	// Use this for initialization
	void Start () {
        StartCoroutine(checkRefresh());
	}
    private IEnumerator checkRefresh()
    {
        yield return new WaitForSeconds(.5f);
        while (true)
        {
            yield return new WaitForSeconds(.5f);
            if (LevelData.Instance.grass == null && LevelData.Instance.flowers == null)
            {
                goToNextScene();
            }
            else
            {
                if (LevelData.Instance.grass == null)
                {
                    if (LevelData.Instance.flowers.Count == 0)
                    {
                        goToNextScene();
                    }
                }
                else if (LevelData.Instance.flowers == null)
                {
                    if (LevelData.Instance.grass.Count == 0)
                    {
                        goToNextScene();
                    }
                }
                else
                {
                    if (LevelData.Instance.flowers.Count == 0 && LevelData.Instance.grass.Count == 0)
                    {
                        goToNextScene();
                    }
                }
            }
        }
    }
    
	
}
