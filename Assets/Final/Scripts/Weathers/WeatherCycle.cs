using UnityEngine;
using System.Collections;

public class WeatherCycle : MonoBehaviour {

    public static WeatherCycle Instance;

    private void Awake()
    {
        Instance = this;
    }
}

[System.Serializable]
public class Weather
{
     
}

[System.Serializable]
public class WeatherObject
{
    public Renderer Object;

}
