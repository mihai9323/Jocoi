using UnityEngine;
using System.Collections;

public class LevelData:MonoBehaviour  {
    
    public static LevelData Instance;

    public GameObject MotherSheep;

    void Awake()
    {
        Instance = this;
    }

}
