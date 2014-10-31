using UnityEngine;
using System.Collections;

public class LevelData:MonoBehaviour  {
    
    public static LevelData Instance;


    public int stageNumber;
    public int nextSceneNumber;

    public GameObject Lamb;
    public GameObject MotherSheep;
    public HerdTarget herdTarget;
    public SheepInHerd[] Sheep;
    public Transform[] HerdWaypoints;
    public float LambSpeed;
    public float MotherSpeed;
	public float JumpSpeedMultiplier;
    public Transform LambHead;

    public Flower[] flowers;
    public Grass[] grass;

    public Camera cam;

    internal string currentAnimation;
    void Awake()
    {
        Instance = this;
        
    }
   

}
