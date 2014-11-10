using UnityEngine;
using System.Collections;



public class Puzzle : MonoBehaviour {

    public static Puzzle Instance;
    public PuzzleGrass GrassPrefab;
    public GameObject[] FlowerModels;
    internal PuzzleGrass[] Grass;
    internal PuzzleGrass[] IncorrectGrass;
    public GameObject[] spawnPoints;
    internal int progress;
    private bool PuzzleFinished;
    public event GameData.VOID_FUNCTION PuzzleCompleted;
    private void Awake()
    {
        if (Instance == null)
        {

            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            progress = 0;
            PuzzleFinished = false;
            spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPositions");
            PlaceGrass();
            
        }
        else
        {
            Puzzle.Instance.gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
    }
  
    public void PlaceGrass()
    {
        if (GameData.Memory != null)
        {
            Grass = new PuzzleGrass[GameData.Memory.Count];

            for (int i = 0; i < GameData.Memory.Count; i++)
            {
                int r = Random.Range(0,spawnPoints.Length);
                while(spawnPoints[r].GetComponent<Spawner>().occupied == true){
                    r = Random.Range(0,spawnPoints.Length);
                }
                GameObject aux = Instantiate(GrassPrefab.gameObject, spawnPoints[r].transform.position, GrassPrefab.transform.rotation) as GameObject;
                aux.gameObject.transform.parent = gameObject.transform;
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
            PlaceIncorrectGrass();
        }
    }
    private void PlaceIncorrectGrass()
    {
        IncorrectGrass = new PuzzleGrass[2];
        for (int i = 0; i < IncorrectGrass.Length; i++)
        {
            int r = Random.Range(0, spawnPoints.Length);
            while (spawnPoints[r].GetComponent<Spawner>().occupied == true)
            {
                r = Random.Range(0, spawnPoints.Length);
            }

            GameObject aux = Instantiate(GrassPrefab.gameObject, spawnPoints[r].transform.position, GrassPrefab.transform.rotation) as GameObject;
            aux.gameObject.transform.parent = gameObject.transform;
           // aux.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            IncorrectGrass[i] = aux.GetComponent<PuzzleGrass>();
            int randomIndex = GetRandomNotCorrectID();
            IncorrectGrass[i].flowerGraphic = Instantiate(FlowerModels[randomIndex].gameObject, IncorrectGrass[i].gameObject.transform.position, FlowerModels[randomIndex].transform.rotation) as GameObject;
            IncorrectGrass[i].flowerGraphic.transform.parent = IncorrectGrass[i].transform;
            IncorrectGrass[i].flowerGraphic.SetActive(false);
            IncorrectGrass[i].trackID = FlowerModels[randomIndex].GetComponent<Flower>().patternToAdd.trackID;
            IncorrectGrass[i].instrumentID = FlowerModels[randomIndex].GetComponent<Flower>().patternToAdd.instrumentID;
            if (IncorrectGrass[i].flowerGraphic.GetComponent<Flower>())
            {
                Destroy(IncorrectGrass[i].flowerGraphic.GetComponent<Flower>());
            }
        }
    }

    private int GetRandomNotCorrectID()
    {
        bool ok = false;
        int randomIndex = 0;
        while (!ok)
        {
            randomIndex = Random.Range(0, FlowerModels.Length);
            foreach (PuzzleGrass pg in Grass)
            {
                if (pg.trackID != FlowerModels[randomIndex].GetComponent<Flower>().patternToAdd.trackID || pg.instrumentID != FlowerModels[randomIndex].GetComponent<Flower>().patternToAdd.instrumentID)
                {
                    ok = true;
                    break;
                }
            }
        }
        return randomIndex;
    }
    public bool CheckFlower(PuzzleGrass pg)
    {
        if (!PuzzleFinished)
        {
            bool correct = false;
            foreach (PuzzleGrass _pg in Grass)
            {
                if (_pg.instrumentID == pg.instrumentID && _pg.trackID == pg.trackID)
                {
                    correct = true;
                    break;
                }
            }
            if (correct)
            {
                progress++;
                if (progress == Grass.Length)
                {
                    CompletePuzzle();
                }
            }
            else
            {
                progress = 0;
                foreach (PuzzleGrass _pg in Grass)
                {
                    _pg.state = PuzzleGrass.GrassStates.TallGrass;
                }
                foreach (PuzzleGrass _pg in IncorrectGrass)
                {
                    _pg.state = PuzzleGrass.GrassStates.TallGrass;
                }
                SoundManager.Instance.StopAll();
            }
            return correct;
        }
        else return true;
    }
    public void CompletePuzzle()
    {
       if(PuzzleCompleted!=null) PuzzleCompleted();
       PuzzleFinished = true;
    }
    
}
