using UnityEngine;
using System.Collections;

public class ShowPath : MonoBehaviour {

    public GameObject path;
	// Use this for initialization
	void Start () {

        Puzzle.Instance.PuzzleCompleted += showPath;

	}
    void OnDestroy()
    {
        Puzzle.Instance.PuzzleCompleted -= showPath;
    }
    void showPath()
    {
        GameObject gob = Instantiate(path, transform.position, path.transform.rotation) as GameObject; 
        gob.transform.parent = transform;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
