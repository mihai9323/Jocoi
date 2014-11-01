using UnityEngine;
using System.Collections;

public class Grass : Plant {

    void Start()
    {
        LevelData.Instance.grass.Add(this);
    }
    protected override void OnDestroy(){
        
        LevelData.Instance.grass.Remove(this);
        base.OnDestroy();
    }
}
