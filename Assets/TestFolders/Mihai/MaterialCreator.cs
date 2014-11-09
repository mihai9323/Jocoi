using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MaterialCreator : MonoBehaviour {

    public Color[] colors;
	
	// Update is called once per frame
	void Update () {
        if (colors != null)
        {
            for (int i = 0; i < colors.Length; i++)
            {
                renderer.materials[i].color = colors[i];
            }
        }
	}
}
