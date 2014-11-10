using UnityEngine;
using System.Collections;

public class WolfHowl : MonoBehaviour {

   
    public AudioClip HowlSound;
    public float timeBetweenSounds = 5.0f;
    public float variance;
    private float time;

    private bool howled;
	// Use this for initialization
	void Start () {
        time = Time.time;
        howled = false;
	}
    void Update()
    {
        if (howled && time + timeBetweenSounds < Time.time)
        {
            AudioSource.PlayClipAtPoint(HowlSound, transform.position);
            time = Time.time+ Random.Range(0,variance);
        }
    }
    void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Mother")
        {

           
                howled = true;
               
           
           
           
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Mother")
        {


            howled = false;
            


        }
    }
    
}
