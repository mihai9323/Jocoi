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
        Instantiate(path, transform.position, path.transform.rotation);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
