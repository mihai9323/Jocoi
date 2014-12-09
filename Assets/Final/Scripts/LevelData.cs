using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
	internal bool cuddling;
    public List<Flower> flowers;
    public List<Grass> grass;
	public FlowerPannel[] flowerPannels;
	public GameObject particle;

    public Camera cam;
	public bool tutorialMode = false;
	internal int loggedActions = 0;
    internal string currentAnimation = "Jump";
    void Awake()
    {
        Instance = this;
		cuddling = false;
    }
	
}
