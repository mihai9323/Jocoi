using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
public class SaveMaterials : MonoBehaviour {
	
	public string folder;
	public bool save;
	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
		if (save)
		{
			save = false;
			foreach (Material mat in renderer.materials)
			{
				AssetDatabase.CreateAsset(mat, "Assets/"+folder+"/"+mat.name+".mat");
			}
		}
	}
}