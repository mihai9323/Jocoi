using UnityEngine;
using System.Collections;

public class Blend : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void BlendFloat(float start, float end, float time, string animationFloat, Animator anim)
    {
		//Get total length of animation.
		AnimatorStateInfo animInfo = anim.GetCurrentAnimatorStateInfo(0);
		float length = animInfo.length;

		//Find the length for this animation.
		float blendLength = length / (1 + start + end);
		//Calculate the play speed of the animation.
		float blendSpeed = blendLength / time;
		
		//Play animation.
		anim.Play(animationFloat, -1, start);
		anim.speed = blendSpeed;
		//Stops animation after set time.
		StartCoroutine(EndBlend(time, anim));

			
    }

    public void StopBlend()
    {
		GetComponent<Animator>().speed = 0f;		
    }
	
	private IEnumerator EndBlend(float duration, Animator anim)
	{
			yield return new WaitForSeconds(duration);
			anim.speed = 0f;
	}

}
