using UnityEngine;
using System.Collections;



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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            LoadMemories();

        }
        else
        {
            Destroy(this.gameObject);
        }
    }
   
    private void LoadMemories()
    {

        Memories = GameData.Memory.ToArray();
        
        PlaceGrass();
    }

    private void PlaceGrass(){
        Grass = new PuzzleGrass[Memories.Length];
        GameObject[] aux = new GameObject[Grass.Length];


        for (int i = 0; i < Grass.Length; i++)
            {
                
                GameObject go = Instantiate(PuzzleGrassModel.gameObject) as GameObject;
                go.transform.parent = this.transform;
                aux[i] = go;
                Grass[i] = go.GetComponent<PuzzleGrass>();
                Grass[i].flowerGraphic = Instantiate(FlowerTypes[Memories[i].flowerType].gameObject, Grass[i].transform.position, FlowerTypes[Memories[i].flowerType].gameObject.transform.rotation) as GameObject;
                Grass[i].flowerGraphic.transform.parent = Grass[i].transform;
                Grass[i].flowerGraphic.SetActive(false);

                for (int j = 0; j < Grass[i].flowerGraphic.renderer.materials.Length; j++)
                {
                    if (j == Memories[i].TrunchiNr) Grass[i].flowerGraphic.renderer.materials[j].color = Memories[i].trunchiColor;
                    else
                    {
                        //Color c= patternToAdd.color.RGBtoHSI();
                        //c.g = c.g - (float)i * 0.1f;
                        //c = c.HSItoRGB();
                        Grass[i].flowerGraphic.renderer.materials[j].color = Memories[i].patternToAdd.color;

                    }
                }
                if (Grass[i].flowerGraphic.GetComponent<Flower>())
                {
                    Destroy(Grass[i].flowerGraphic.GetComponent<Flower>());
                }
                Grass[i].state = PuzzleGrass.GrassStates.TallGrass;

            }
        aux = ShuffleGameObjects(aux);
        objectPlacer.ObjectsToPlace = ShuffleGameObjects(aux);
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
        Debug.Log("PuzzleCompleted");
    }

    private GameObject[] ShuffleGameObjects(GameObject[] toShuffle)
    {


        for (int i = 0; i < toShuffle.Length; i++)
        {
            GameObject tmp = toShuffle[i];
            int r = Random.Range(i, toShuffle.Length);
            toShuffle[i] = toShuffle[r];
            toShuffle[r] = tmp;
        }


        return toShuffle;
    }


    
    
}
