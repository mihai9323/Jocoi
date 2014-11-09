using UnityEngine;
using System.Collections;

public class AnimateTree : MonoBehaviour {

    public Animator anim;
    public float timeBetween = 2.0f;
    public float variedTime = 5.0f;
	// Use this for initialization
	void Start () {
        StartCoroutine(PlayAnimation());
	}

    IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(Random.Range(0,variedTime));
        while (true)
        {
            switch (WeatherCycle.Instance.currentWeather)
            {
                case 0: break;
                case 1: anim.SetTrigger("Rain"); break;
                case 2: anim.SetTrigger("Snow"); break;
                case 3: anim.SetTrigger("Fog"); break;
            }
            yield return new WaitForSeconds(timeBetween + Random.Range(0, variedTime));
        }
    }
}
