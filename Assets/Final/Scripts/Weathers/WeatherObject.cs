using UnityEngine;
using System.Collections;

public class WeatherObject : MonoBehaviour {

    public MaterialColor[] WeatherColors;

    private int currentWeather, nextWeather;

    public void Start()
    {
        WeatherCycle.Instance.FadeToWeather += FadeTo;
        for (int i = 0; i < renderer.materials.Length; i++)
        {
            renderer.materials[i].color = WeatherColors[nextWeather].materialColor[i];
            currentWeather = nextWeather = 0;
        }
       
    }

    public void FadeTo(int c){
        StopAllCoroutines();
        nextWeather = c;
        for (int i = 0; i < renderer.materials.Length; i++)
        {
            StartCoroutine(Fade(nextWeather,i));
        }
            
    }
   
    private IEnumerator Fade(int c, int index)
    {
        float ct = 0;
        while (ct < .9f)
        {
            ct += Time.fixedDeltaTime/ WeatherCycle.Instance.weatherTransitionTime;
            renderer.materials[index].color = Color.Lerp(renderer.materials[index].color,WeatherColors[c].materialColor[index],ct);
            yield return new WaitForFixedUpdate();
        }
        renderer.materials[index].color = WeatherColors[nextWeather].materialColor[index];
        currentWeather = nextWeather;
    }
   
}
[System.Serializable]
public class MaterialColor
{
    public Color[] materialColor;
}
