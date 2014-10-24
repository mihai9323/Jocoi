using UnityEngine;
using System.Collections;

public class MoveToPosition : MonoBehaviour {

    private GameData.VOID_FUNCTION complete;
    private AudioClip sound;
    private string animation;
    private float speed;
    public Vector3 offSet;

    public void StartMoving(Vector3 position, GameData.VOID_FUNCTION complete, AudioClip sound, string animation, float speed = 1.0f)
    {
        this.complete = complete;
        this.sound = sound;
        this.animation = animation;
        this.speed = speed;
        GetComponent<Animator>().SetBool(animation, true);
    }
    public void StartMoving(Vector3 position, GameData.VOID_FUNCTION complete, float speed = 1.0f)
    {
        StopMovement();
        this.complete = complete;
     //   this.sound = sound;
     //   this.animation = animation;
        this.speed = speed;
        StartCoroutine(Move(position+offSet));
    }
    private void AtDestination()
    {
        StopAllCoroutines();
        complete();
      
    }
    private IEnumerator Move(Vector3 pos)
    {
        while(Vector3.Distance(transform.position,pos)>2){
            transform.position += (pos - transform.position).normalized * Time.deltaTime * speed;
            transform.LookAt(pos);
            yield return new WaitForEndOfFrame();
        }
        AtDestination();
        
    }
    public void StopMovement()
    {
        StopAllCoroutines();
       
    }
}
