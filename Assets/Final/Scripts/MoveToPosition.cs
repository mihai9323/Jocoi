﻿using UnityEngine;
using System.Collections;

public class MoveToPosition : MonoBehaviour {

    private GameData.VOID_FUNCTION complete;
    private AudioSource audioSource;

    public AudioClip[] sounds;
    
    internal string animation;
    
    private float speed;
    private float successDistance;
    public Animator anim;
    public Vector3 offSet;
    private void Awake(){
        if (GetComponent<AudioSource>() != null) audioSource = GetComponent<AudioSource>(); 
        animation = "";
        
        anim = null;
    }
    //move with animation and sound
    public void StartMoving(Vector3 position, GameData.VOID_FUNCTION complete, int sound, string animation, float speed = 1.0f,float acceptedDistance = 1.0f)
    {
        successDistance = acceptedDistance;
        position = new Vector3(position.x, transform.position.y, position.z);
        StopMovement();
        if (sounds != null) if (sounds.Length > sound)
            {
                audioSource.clip = sounds[sound];
                audioSource.Play();

            }
        this.complete = complete;
        this.animation = animation;
        this.speed = speed;
        if (animation != "" && anim != null) anim.SetBool(animation, true);
        StartCoroutine(Move(position + offSet));
    }
    //move without animation and sound
    public void StartMoving(Vector3 position, GameData.VOID_FUNCTION complete, float speed = 1.0f, float acceptedDistance = 1.0f)
    {
        position = new Vector3(position.x,transform.position.y,position.z);
        successDistance = acceptedDistance;
        StopMovement();
        this.complete = complete;
        this.speed = speed;
        StartCoroutine(Move(position+offSet));
    }
    private void AtDestination()
    {
        StopAllCoroutines();
        StopMovement();
        if(complete!=null)complete();      
    }
    private IEnumerator Move(Vector3 pos)
    {
        while(Vector3.Distance(transform.position,pos)>successDistance){
            transform.position += (pos - transform.position).normalized * Time.deltaTime * speed;
            transform.LookAt(pos);
            transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y,0);
            yield return new WaitForFixedUpdate();
        }
        AtDestination();
        
    }
    public void StopMovement()
    {
        StopAllCoroutines();
        if (animation != "" && anim!=null) anim.SetBool(animation, false);
        if (audioSource != null) audioSource.Stop();
       
    }
}