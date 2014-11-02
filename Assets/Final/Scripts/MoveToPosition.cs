using UnityEngine;
using System.Collections;

public class MoveToPosition : MonoBehaviour {

    private GameData.VOID_FUNCTION complete;
    private AudioSource audioSource;

    public AudioClip[] sounds;
    
    internal string animation;
    private int sound;
    private float speed;
    private float successDistance;
    public Animator anim;
    public Vector3 offSet;
    private void Awake(){
        if (GetComponent<AudioSource>() != null) audioSource = GetComponent<AudioSource>(); 
        animation = "";
        
        //anim = null;
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

    //Transform overlaods
    public void StartMoving(Transform pos, GameData.VOID_FUNCTION complete, int sound, string animation, float speed = 1.0f, float acceptedDistance = 1.0f)
    {
        successDistance = acceptedDistance;
       // position = new Vector3(pos.position.x, transform.position.y, pos.position.z);
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
        StartCoroutine(Move(pos ));
    }
    //move without animation and sound
    public void StartMoving(Transform position, GameData.VOID_FUNCTION complete, float speed = 1.0f, float acceptedDistance = 1.0f)
    {
       
        successDistance = acceptedDistance;
        StopMovement();
        this.complete = complete;
        this.speed = speed;
        StartCoroutine(Move(position ));
    }

    private void AtDestination()
    {
        StopAllCoroutines();
        StopMovement();
        if(complete!=null)complete();      
    }
    private IEnumerator Move(Vector3 pos)
    {
        while(   transform.position.SquaredDistance(pos)> Mathf.Pow(successDistance,2.0f)){
            transform.position += (pos - transform.position).normalized * Time.deltaTime * speed;
            transform.FaceObjectOnAxis(pos, new Vector3(0, 1, 0));
            
            yield return new WaitForFixedUpdate();
        }
        AtDestination();
        
    }

    private IEnumerator Move(Transform pos)
    {
        while (transform.position.SquaredDistance(pos.position) > Mathf.Pow(successDistance, 2.0f))
        {
            transform.position += (pos.position - transform.position).normalized * Time.deltaTime * speed;
            transform.FaceObjectOnAxis(pos, new Vector3(0, 1, 0));

            yield return new WaitForFixedUpdate();
        }
        AtDestination();

    }
    public void StopMovement()
    {
        StopAllCoroutines();
        if (animation != "" && anim!=null) anim.SetBool(animation, false);
        if (audioSource != null) audioSource.Stop();
        this.gameObject.GetComponent<Grid>().RemoveMap();
    }


    public void StartMovingOnPath(Vector3 position, GameData.VOID_FUNCTION complete, int sound, string animation, float speed = 1.0f, float acceptedDistance = 1.0f)
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
        this.sound = sound;
        StartCoroutine(this.GetComponent<Grid>().CalculatePath(transform.position,position,StartMovementOnPath));

        
    }
    public void StartMovingOnPath(Transform tpos, GameData.VOID_FUNCTION complete, int sound, string animation, float speed = 1.0f, float acceptedDistance = 1.0f)
    {
        successDistance = acceptedDistance;
        Vector3 pos = new Vector3(tpos.position.x, transform.position.y, tpos.position.z);
        StopMovement();
        if (sounds != null) if (sounds.Length > sound)
            {
                audioSource.clip = sounds[sound];
                audioSource.Play();

            }
        this.complete = complete;
        this.animation = animation;
        this.speed = speed;

        StartCoroutine(this.GetComponent<Grid>().CalculatePath(transform.position, pos, StartMovementOnPath));


    }
    void StartMovementOnPath(Vector3[] path)
    {
        StartCoroutine(MoveOnPath(path));
    }
    private  IEnumerator MoveOnPath(Vector3[] path){
        if (animation != "" && anim != null) anim.SetBool(animation, true);
        for(int i=0; i<path.Length; i++){
            
            while(transform.position.SquaredDistance(path[i])>= successDistance*successDistance ){
                transform.position += (path[i] - transform.position).normalized * Time.fixedDeltaTime * speed;
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                transform.FaceObjectOnAxis(path[i], new Vector3(0, 1, 0));
                RaycastHit hit;
                
                if(Physics.Raycast(transform.position,(path[i]-transform.position).normalized,out hit,Vector3.Distance(path[i],transform.position))){
                    if (hit.collider.transform.position != path[path.Length - 1] && hit.collider.tag != "Node")
                    {

                        StartMovingOnPath(path[path.Length - 1], complete, sound, animation, speed, successDistance);
                    }
                }
                 
                yield return new WaitForFixedUpdate();
            }
            i++;
        }
        AtDestination();
    }
}
