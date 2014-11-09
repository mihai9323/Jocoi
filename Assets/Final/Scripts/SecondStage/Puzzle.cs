using UnityEngine;
using System.Collections;



public class Puzzle : MonoBehaviour {

    public static Puzzle Instance;
    public PuzzleGrass GrassPrefab;
    public GameObject[] FlowerModels;
    internal PuzzleGrass[] Grass;
    public Spawner[] spawnPoints;

    public event GameData.VOID_FUNCTION PuzzleCompleted;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            PlaceGrass();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlaceGrass()
    {
        if (GameData.Memory != null)
        {
            for (int i = 0; i < GameData.Memory.Count; i++)
            {
                int r = Random.Range(0,spawnPoints.Length);
                while(spawnPoints[r].occupied == true){
                    r = Random.Range(0,spawnPoints.Length);
                }
                GameObject aux = Instantiate(GrassPrefab, spawnPoints[r].transform.position, GrassPrefab.transform.rotation) as GameObject;
                aux.transform.parent = this.transform;
                Grass[i] = aux.GetComponent<PuzzleGrass>();
                Grass[i].flowerGraphic = Instantiate(FlowerModels[GameData.Memory[i].flowerID].gameObject,Grass[i].gameObject.transform.position,FlowerModels[GameData.Memory[i].flowerID].transform.rotation) as GameObject;
                Grass[i].flowerGraphic.transform.parent = Grass[i].transform;
                Grass[i].flowerGraphic.SetActive(false);
                Grass[i].trackID = GameData.Memory[i].patternToAdd.trackID;
                Grass[i].instrumentID = GameData.Memory[i].patternToAdd.instrumentID;
                if (Grass[i].flowerGraphic.GetComponent<Flower>())
                {
                    Destroy(Grass[i].flowerGraphic.GetComponent<Flower>());
                }
            }
        }
    }

    public bool CheckFlower(PuzzleGrass pg)
    {

        return true;
    }
    public void CompletePuzzle()
    {
       if(PuzzleCompleted!=null) PuzzleCompleted();
    }
    
}
