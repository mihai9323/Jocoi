using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class ReplacePrefabs : MonoBehaviour {

    public GameObject ReplaceWith;

    public bool StartWorking;
	

	// Update is called once per frame
	void Update () {
        if (StartWorking)
        {
            StartWorking = false;
            replacePrefabs();
        }
	}

    void replacePrefabs()
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        Vector3 scale = transform.localScale;
        
        
        GameObject aux = Instantiate(
                ReplaceWith,
                position,
                rotation
            ) as GameObject;
        if (transform.parent != null)
        {
            aux.transform.parent = transform.parent;
        }
        aux.name = ReplaceWith.name;
        DestroyImmediate(this.gameObject);
    }
}
