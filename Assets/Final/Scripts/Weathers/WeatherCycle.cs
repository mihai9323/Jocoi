﻿using UnityEngine;
using System.Collections;

public class WeatherCycle : MonoBehaviour {

    public static WeatherCycle Instance;

    private Spawner[] spawnPositions;

    public event GameData.VOID_FUNCTION_INT FadeToWeather;
    public event GameData.VOID_FUNCTION ChangeWeather;
    public Weather[] weathers;
    public int currentWeather{
        set{

            _currentWeather = value % weathers.Length;
            
        }
        get
        {
            return _currentWeather;
        }
    }

    private int _currentWeather;
    public float weatherTransitionTime;

    private void Awake()
    {
        Instance = this;
        
    }
    private void Start()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("SpawnPositions");
        spawnPositions = new Spawner[go.Length];
        for (int i = 0; i < go.Length; i++)
        {
            spawnPositions[i] = go[i].GetComponent<Spawner>();
        }
        ChangeTheWeather();
    }
    public void FadeWeathers()
    {
        foreach(Flower f in LevelData.Instance.flowers)
        {
            f.StartDie();
        }
        foreach (Grass g in LevelData.Instance.grass)
        {
            g.StartDie();
        }
        foreach (GameObject go in weathers[currentWeather].ObjectsToEnable)
        {
            go.SetActive(false);

        }
        StartCoroutine("ZapWeathers");
    }

    public void ChangeTheWeather()
    {
        StopAllCoroutines();
        if(ChangeWeather!=null)ChangeWeather();
        for (int i = 0; i < weathers[currentWeather].flowersToSpawn; i++)
        {
            int randIndex = Random.Range(0, weathers[currentWeather].flowers.Length);
            int randSI = Random.Range(0, spawnPositions.Length);
            while (spawnPositions[randSI].occupied)
            {
                randSI = Random.Range(0, spawnPositions.Length);
            }
            Vector3 positionToSpawn = spawnPositions[randSI].transform.position;
            GameObject auxGo = Instantiate(weathers[currentWeather].flowers[randIndex].gameObject, positionToSpawn, weathers[currentWeather].flowers[randIndex].gameObject.transform.rotation) as GameObject;
           
        }
        for (int i = 0; i < weathers[currentWeather].grassToSpawn; i++)
        {
            int randIndex = Random.Range(0, weathers[currentWeather].grass.Length);
            int randSI = Random.Range(0, spawnPositions.Length);
            while (spawnPositions[randSI].occupied)
            {
                randSI = Random.Range(0, spawnPositions.Length);
            }
            Vector3 positionToSpawn = spawnPositions[randSI].transform.position;
            GameObject auxGo = Instantiate(weathers[currentWeather].grass[randIndex].gameObject, positionToSpawn, weathers[currentWeather].grass[randIndex].gameObject.transform.rotation) as GameObject;

        }
        foreach (GameObject go in weathers[currentWeather].ObjectsToEnable)
        {
            go.SetActive(true);

        }
    }

    private IEnumerator ZapWeathers()
    {
        while (true)
        {
            if (FadeToWeather != null)
            {
                FadeToWeather(currentWeather++);
            }
            yield return new WaitForSeconds(weatherTransitionTime);

        }
    }
}

[System.Serializable]
public class Weather
{
    public string name;
    public GameObject[] ObjectsToEnable;
    public Flower[] flowers;
    public Grass[] grass;

    public int flowersToSpawn, grassToSpawn;


}

