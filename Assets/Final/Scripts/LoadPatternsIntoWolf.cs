using UnityEngine;
using System.Collections;

public class LoadPatternsIntoWolf : MonoBehaviour {

    public PatternAnimator pa;

    private void Start()
    {
        foreach (Flower f in GameData.Memory)
        {
            pa.AddPattern(f.patternToAdd);
        }
    }
}
