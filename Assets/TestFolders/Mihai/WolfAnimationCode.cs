using UnityEngine;
using System.Collections;

public class WolfAnimationCode : MonoBehaviour {

    public float minimumAnimationChange = 5.0f;
    public float variationTime = 3.0f;
    public Animator anim;

    public AudioClip Howl;

	// Use this for initialization
	void Start () {
        StartCoroutine(ChangeAnimation());
	}

    IEnumerator ChangeAnimation()
    {
        while (true)
        {
            yield return new WaitForSeconds(minimumAnimationChange + Random.Range(0, variationTime));

            switch (Random.Range(0, 4))
            {
                case 0: anim.SetTrigger("HowlTrigger"); AudioSource.PlayClipAtPoint(Howl, transform.position); break;
                case 1: anim.SetTrigger("WolfLieDown"); break;
                case 2: anim.SetTrigger("Sniff"); break;

                    
            }
        }
    }
}
