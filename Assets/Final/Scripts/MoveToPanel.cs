using UnityEngine;
using System.Collections;

public class MoveToPanel : MonoBehaviour {

	public void moveToPanel(int panelID){
		if(LevelData.Instance != null){
			if(LevelData.Instance.flowerPannels !=null){
				if(LevelData.Instance.flowerPannels.Length > panelID){
					Vector3 getFinalPosition = Camera.main.ScreenToWorldPoint(LevelData.Instance.flowerPannels[panelID].transform.position);
					
					StartCoroutine(startMovement(getFinalPosition));
				}else noMove();
			}else noMove();
		}else noMove();
	}
	
	private void noMove(){
		
		Destroy(this.gameObject);
	}
	
	private IEnumerator startMovement(Vector3 finalPosition){
		float ct = 0;
		Vector3 initPos = transform.position;
		
		while(ct<1){
			transform.position = Vector3.Lerp(initPos,finalPosition,ct);
			ct+= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		Destroy (this.gameObject,0.2f);
	}
}
