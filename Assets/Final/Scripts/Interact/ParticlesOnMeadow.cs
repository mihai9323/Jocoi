using UnityEngine;
using System.Collections;

public class ParticlesOnMeadow : MonoBehaviour {
	
	public enum HasFeature{
		enabled,
		disabled
	}
	public HasFeature weathers = HasFeature.disabled;
	public GameObject[] particlesToSpawn;
	public float particleLifeTime = 1.0f;
	public float Delay = 0.5f;
	
	public void Update(){
	if(particlesToSpawn !=null){
		if(Input.GetMouseButton(0)){
			if(particlesToSpawn.Length>0){
					if(Inputs.Instance.ActiveObject !=null){
						if(Inputs.Instance.ActiveObject.gameObject == this.gameObject){
							if(weathers == HasFeature.disabled){
								GameObject gob = Instantiate(particlesToSpawn[0],Inputs.Instance.GetMouseOnScreen(),particlesToSpawn[0].transform.rotation) as GameObject;
								gob.layer = 2;
								Destroy(gob,particleLifeTime);
								}
							}else{
								GameObject gob = Instantiate(particlesToSpawn[WeatherCycle.Instance.currentWeather],Inputs.Instance.GetMouseOnScreen(),particlesToSpawn[0].transform.rotation) as GameObject;
							    gob.layer = 2;
							    Destroy(gob,particleLifeTime);
							
							}
						}
				
				}
			}
		}
	}
}
