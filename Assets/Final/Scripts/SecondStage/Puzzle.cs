using UnityEngine;
using System.Collections;



public class Puzzle : MonoBehaviour {

    public static Puzzle Instance;

    public GameObject[] FlowerTypes; // have to match the numbers on the flowers
    public GameObject StoneModel; //
    public GameObject Path;
    public PuzzleGrass PuzzleGrassModel;

    private Flower[] Memories;
    

    internal PuzzleGrass[] Grass; //the puzzleGrass placed
    internal RiverStone[] Stones;
    public PlaceObjects2 GrassPlacer;
    public PlaceObjects2 StonePlacer;
    public int progress;

    public event GameData.VOID_FUNCTION PuzzleCompleted;
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
        if (GameData.Memory != null)
        {
            Memories = GameData.Memory.ToArray();

            PlaceGrass();
        }
    }

    private void PlaceGrass(){
        Grass = new PuzzleGrass[Memories.Length];

        GameObject[] aux = new GameObject[Grass.Length];
        GameObject[] stones = new GameObject[Grass.Length];
        Stones = new RiverStone[Grass.Length];
        for (int i = 0; i < Grass.Length; i++)
            {
                
                GameObject go = Instantiate(PuzzleGrassModel.gameObject) as GameObject;
                go.transform.parent = this.transform;
                aux[i] = go;
                Grass[i] = go.GetComponent<PuzzleGrass>();
                Grass[i].flowerGraphic = Instantiate(FlowerTypes[Memories[i].flowerType].gameObject, Grass[i].transform.position, FlowerTypes[Memories[i].flowerType].gameObject.transform.rotation) as GameObject;
                Grass[i].flowerGraphic.transform.parent = Grass[i].transform;
                Grass[i].flowerGraphic.SetActive(false);
                if (StoneModel != null)
                {
                    stones[i] = Instantiate(StoneModel.gameObject) as GameObject;

                    Stones[i] = stones[i].gameObject.GetComponent<RiverStone>();
                    Stones[i].colors = new Color[1]{ Memories[i].patternToAdd.color};
                    Stones[i].audioSourceInfluenced1 = Memories[i].patternToAdd.flowerSourceColor;
                    Stones[i].audioSourceInfluenced1 = Memories[i].patternToAdd.flowerSourceType;
                }
            /*
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
             * */

                if (Grass[i].flowerGraphic.GetComponent<Flower>())
                {
                    Destroy(Grass[i].flowerGraphic.GetComponent<Flower>());
                }
                Grass[i].state = PuzzleGrass.GrassStates.TallGrass;

                
                

            }
        aux = ShuffleGameObjects(aux);
        GrassPlacer.ObjectsToPlace = ShuffleGameObjects(aux);
        GrassPlacer.PlaceObjects();


        StonePlacer.ObjectsToPlace = stones;
        StonePlacer.PlaceObjects();
        progress = 0; 
    }
    public bool CheckFlower(PuzzleGrass flower)
    {
        bool returnValue = false;
        if (Grass != null)
        {
            if (flower == Grass[progress])
            {
                if(Stones!=null)Stones[progress].ActivateStone();
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
        if (PuzzleCompleted != null) PuzzleCompleted();
        Path.SetActive(true);
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
