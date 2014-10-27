using UnityEngine;
using System.Collections;

public class Flower : Plant {

    public PatternInfo patternToAdd;
   
    protected override void LambEat()
    {
        Debug.Log("jos1");
        base.LambEat();
        Debug.Log("jos2");
        LevelData.Instance.Lamb.GetComponent<PatternAnimator>().AddPattern(patternToAdd);
        LevelData.Instance.Lamb.GetComponent<PatternAnimator>().StartAnimation();
       
    }
    protected override void LambFinishEat()
    {
        LevelData.Instance.Lamb.GetComponent<PatternAnimator>().StopAnimation();
        base.LambFinishEat();
    }
}
