using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class Puzzle : MonoBehaviour {

    public static Puzzle Instance;

    public GameObject[] FlowerTypes; // have to match the numbers on the flowers
    public PuzzleGrass PuzzleGrassModel;

    private Flower[] Memories;
    

    internal PuzzleGrass[] Grass; //the puzzleGrass placed
    public PlaceObjects2 objectPlacer;
    public int progress;

    private void Awake()
    {
        Instance = this;
    }

    private void LoadMemories()
    {
        Memories = GameData.Memory.ToArray();
        PlaceGrass();
    }

    private void PlaceGrass(){
        Grass = new PuzzleGrass[Memories.Length];
        GameObject[] aux = new GameObject[Grass.Length];
        for(int i = 0; i<Grass.Length; i++){
            GameObject go = Instantiate(PuzzleGrassModel.gameObject) as GameObject;
            go.transform.parent = this.transform;
            aux[i] = go;
            Grass[i] = go.GetComponent<PuzzleGrass>();
            Grass[i].flowerGraphic = Instantiate(FlowerTypes[Memories[i].flowerType].gameObject, Grass[i].transform.position, FlowerTypes[Memories[i].flowerType].gameObject.transform.rotation) as GameObject;
            Grass[i].flowerGraphic.transform.parent = Grass[i].transform;
            Grass[i].flowerGraphic.SetActive(false);
            Grass[i].state = PuzzleGrass.GrassStates.TallGrass;

        }
        objectPlacer.ObjectsToPlace = aux;
        objectPlacer.PlaceObjects();
        progress = 0; 
    }
    public bool CheckFlower(PuzzleGrass flower)
    {
        bool returnValue = false;
        if (Grass != null)
        {
            if (flower == Grass[progress])
            {
                progress++;
                returnValue = true;
            }
            if (progress == Grass.Length)
            {
                CompletePuzzle();
            }
        }
        else
        {
            CompletePuzzle();
        }
       
        return returnValue;
    }

    private void CompletePuzzle()
    {

    }


   

    
}
