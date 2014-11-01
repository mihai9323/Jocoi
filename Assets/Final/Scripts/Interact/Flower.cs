using UnityEngine;
using System.Collections;

public class Flower : Plant {

    public PatternInfo patternToAdd;

    public override void Feed()
    {
        base.Feed();
       
    }
    protected override void LambEat()
    {
       
        base.LambEat();
       
        LevelData.Instance.Lamb.GetComponent<PatternAnimator>().AddPattern(patternToAdd);
        LevelData.Instance.Lamb.GetComponent<PatternAnimator>().StartAnimation();
       
    }
    protected override void LambFinishEat()
    {
        LevelData.Instance.Lamb.GetComponent<PatternAnimator>().StopAnimation();
        base.LambFinishEat();
        
    }

}
