using UnityEngine;
using System.Collections;

public class AnimateTree : MonoBehaviour {

    public Animator anim;
    public float minTime = 2.0f;
    public float variance = 5.0f;
	private string season;
	// Use this for initialization
	void Start () {
		season = "Sunny";
		if (WeatherCycle.Instance != null)
						WeatherCycle.Instance.ChangeWeather += ChangeSeason;
        StartCoroutine(PlayAnimation());

	}
	void OnDestroy(){
		if (WeatherCycle.Instance != null)
			WeatherCycle.Instance.ChangeWeather -= ChangeSeason;
	}
    IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(Random.Range(0,1.0f));

		if (WeatherCycle.Instance != null) {
				while (true) {
					anim.SetTrigger (season);
					yield return new WaitForSeconds (minTime + Random.Range (0, variance));
					}
				}
    }
	private void ChangeSeason(){
		switch (WeatherCycle.Instance.currentWeather) {
		case 0:
			season = "Sunny";
			break;
		case 1:
			season="Rain";
			break;
		case 2:
			season = "Snow";
			break;
		case 3:
			season = "Fog";
			break;
		}
	}
}
