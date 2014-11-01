using UnityEngine;
using System.Collections;

public class WeatherObject : MonoBehaviour {

    public Color[] WeatherColors;

    private int currentWeather, nextWeather;

    public void Start()
    {
        WeatherCycle.Instance.FadeToWeather += FadeTo;
        
    }

    public void FadeTo(int c){
        nextWeather = c;
        StartCoroutine(Fade(nextWeather));
    }
   
    private IEnumerator Fade(int c)
    {
        float ct = 0;
        while (ct < .9f)
        {
            ct += Time.fixedDeltaTime/ WeatherCycle.Instance.weatherTransitionTime;
            renderer.material.color = Color.Lerp(renderer.material.color,WeatherColors[c],ct);
            yield return new WaitForFixedUpdate();
        }
        renderer.material.color = WeatherColors[nextWeather];
        currentWeather = nextWeather;
    }
   
}
