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
		StartCoroutine (refresh());
    }
    IEnumerator refresh(){
		while(true){
			yield return new WaitForSeconds(1.0f);
			DetectPanels();
		}
    }
	void DetectPanels(){
		if(flowerPannels == null){
			flowerPannels = new FlowerPannel[4];
		}
		if(flowerPannels[0] == null){
			GameObject aux = GameObject.Find("HolderCircle");
			if(aux!=null) flowerPannels[0] = aux.GetComponent<FlowerPannel>();
		}
		if(flowerPannels[1] == null){
			GameObject aux = GameObject.Find("HolderDancing");
			if(aux!=null) flowerPannels[1] = aux.GetComponent<FlowerPannel>();
		}
		if(flowerPannels[2] == null){
			GameObject aux = GameObject.Find("HolderSnake");
			if(aux!=null) flowerPannels[2] = aux.GetComponent<FlowerPannel>();
		}
		if(flowerPannels[3] == null){
			GameObject aux = GameObject.Find("HolderSpots");
			if(aux!=null) flowerPannels[3] = aux.GetComponent<FlowerPannel>();
		}
	}
}
