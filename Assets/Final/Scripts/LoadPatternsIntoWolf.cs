using UnityEngine;
using System.Collections;

public class LoadPatternsIntoWolf : MonoBehaviour {

    public PatternAnimator pa;

    private void Start()
    {
        foreach (FlowersInMemory f in GameData.Memory)
        {
            pa.AddPattern(new PatternInfo(f.color,f.pattern,f.instrumentID,f.trackID));
        }
        pa.StartAnimation();
    }
}
