using UnityEngine;
using System.Collections;

public class FlowerPannelHolder : MonoBehaviour {

	public static FlowerPannelHolder Instance;
	public int[] showInScenes;
	public int[] hideInScenes;
	public int[] destroyInScenes;
	void Awake(){
	
		if(FlowerPannelHolder.Instance == null){
			Instance = this;
			DontDestroyOnLoad(this.gameObject);
			StartCoroutine(refresh ());
		}else{
			Destroy(this.gameObject);
		}
	}
	
	private IEnumerator refresh(){
		while(true){
			yield return new WaitForSeconds(.8f);
			int thisLevel = Application.loadedLevel;
			foreach(int i in destroyInScenes){
				if(i == thisLevel){
					Destroy(this.gameObject);
				}
			}
			foreach(int i in hideInScenes){
				if(i == thisLevel){
					if(this.gameObject.activeSelf){
						for(int c = 0; c<this.transform.childCount; c++){
							this.transform.GetChild(c).gameObject.SetActive(false);
						}
					}
				}
			}
			foreach(int i in showInScenes){
				if(i == thisLevel){
					
					if(this.gameObject.activeSelf){
						for(int c = 0; c<this.transform.childCount; c++){
							this.transform.GetChild(c).gameObject.SetActive(true);
						}
					}
						
					
				}
			}
			
			
		}
	}
	
}
