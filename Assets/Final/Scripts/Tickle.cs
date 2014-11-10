using UnityEngine;
using System.Collections;

public class Tickle : MonoBehaviour {

   
    public AudioClip TickleSound;
    public float timeBetweenSounds = 5.0f;
    public float variance;
    private float time;

    private bool tickled;
	// Use this for initialization
	void Start () {
        time = Time.time;
        tickled = false;
	}
    void Update()
    {
        if (tickled && time + timeBetweenSounds < Time.time)
        {
            AudioSource.PlayClipAtPoint(TickleSound, transform.position);
            time = Time.time+ Random.Range(0,variance);
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Mother")
        {

           
                tickled = true;
               
           
           
           
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.collider.tag == "Mother")
        {


            tickled = false;
            


        }
    }
    
}
