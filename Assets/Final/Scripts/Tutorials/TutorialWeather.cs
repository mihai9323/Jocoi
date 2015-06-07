using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialWeather :GoToNextScene {

	public static TutorialWeather Instance;
	
	
	public WeatherIcon[] weathers;
	public float weatherTime;
	
	public int weathersToChangeToFinishTutorial = 4;
	public int stopChanginWeathersThisAmountOfTimesToFinishTutorial = 1;
	
	private int weathersChanged;
	private int stoppedCuddling;
	
	private bool weatherSwitching;
	
	private float lastWeatherChange;
	private int currentWeather;
	private void Awake(){
		Instance = this; 
	}
	private void Start(){
		currentWeather = 0;
		weathersChanged = 0;
		stoppedCuddling = 0;
		if(weathers != null) if(currentWeather<weathers.Length){
			weathers[currentWeather].SetWeatherActive();
		}
	}
	private void Update(){
		if(weatherSwitching){
		
			if(lastWeatherChange + weatherTime< Time.time){
				lastWeatherChange = Time.time;
				if(weathers != null) if(currentWeather<weathers.Length){
					weathers[currentWeather].SetWeatherInactive();
					currentWeather ++;
					if(currentWeather >= weathers.Length){
						currentWeather = 0;
						
					}
					weathers[currentWeather].SetWeatherActive();
					weathersChanged ++;
					if(stoppedCuddling >= stopChanginWeathersThisAmountOfTimesToFinishTutorial && weathersChanged>=weathersToChangeToFinishTutorial){
						goToNextScene();
					}
				}
				
				
			}
		}
	}
	public void StartChangingWeathers(){
		weatherSwitching = true;
		Debug.Log("Switching");
		lastWeatherChange = Time.time;
	}
	public void StopChangingWeathers(){
		weatherSwitching = false;
		Debug.Log("StoppedSwitching");
		stoppedCuddling ++;
		if(stoppedCuddling >= stopChanginWeathersThisAmountOfTimesToFinishTutorial && weathersChanged>=weathersToChangeToFinishTutorial){
			goToNextScene();
		}
	}
}
[System.Serializable]
public class WeatherIcon{
	public Image Panel;
	public Sprite WeatherActiveImage;
	public Sprite WeatherInactiveImage;
	
	public GameObject[] flowers; //This should be in the scene set as inactive
	
	public void SetWeatherActive(){
		if(Panel!=null)Panel.sprite = WeatherActiveImage;
		foreach(GameObject f in flowers){
			f.SetActive(true);
		}
	}
	public void SetWeatherInactive(){
		if(Panel!=null)Panel.sprite = WeatherInactiveImage;
		foreach(GameObject f in flowers){
			f.SetActive (false);
		}
	}
}
