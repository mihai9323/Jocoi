using UnityEngine;
using System.Collections;

public class WhiteBush : Plant {

    protected override void LambEat()
    {
       
        base.LambEat();

        LevelData.Instance.Lamb.GetComponent<PatternAnimator>().RemoveLastPattern();
        LevelData.Instance.Lamb.GetComponent<PatternAnimator>().StartAnimation();

    }
    protected override void LambFinishEat()
    {
        LevelData.Instance.Lamb.GetComponent<PatternAnimator>().StopAnimation();
        

        base.LambFinishEat();
    }
}
