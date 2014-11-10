using UnityEngine;
using System.Collections;

public class KillTheSheep : MonoBehaviour {

    public static KillTheSheep Instance;

    public int actionLimit = 15;
    internal int currentActions = 0;
    private bool startEnd = false;
    public GameObject[] enableThis;
    public float fadeOut = 5.0f;
    public float fadeIn = 5.0f;
    public CameraShake cs;
    public AudioClip thunderStorm;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (currentActions > actionLimit && !startEnd && GameData.Memory !=null && GameData.Memory.Count>1 )
        {
            startEnd = true;
            StartCoroutine(EndStage());
        }
    }
    private IEnumerator EndStage()
    {
        yield return new WaitForSeconds(Random.Range(4, 20));
        cs.StartShake();
        foreach (GameObject go in enableThis)
        {
            go.SetActive(true);
        }
        AudioSource.PlayClipAtPoint(thunderStorm, transform.position);
        AutoFade.LoadLevel(LevelData.Instance.nextSceneNumber, fadeOut,fadeIn,Color.black);
    }
}
