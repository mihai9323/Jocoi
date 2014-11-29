using UnityEngine;
using System.Collections;

public class Movement2D : MoveToPosition {

    public AnimationCurve yMovement;
    public Transform minX;
    public Transform maxX;
    public float yAmplitude;
    [Range(0.01f,1.2f)]
    public float Speed;
    
    public string walkingAnimation;
    internal float initialY;
    private float ct;

    private GameData.VOID_FUNCTION complete;
   
    private void Awake()
    {
        initialY = minX.position.y;
        
    }
    private void Start()
    {
        ct = (transform.position.x - minX.position.x) / (maxX.position.x - minX.position.x);
        float x = Mathf.Lerp(minX.position.x, maxX.position.x, ct);
        transform.position = new Vector3(
                x,
                yMovement.Evaluate(ct) * yAmplitude + initialY,
                transform.position.z
                );
       
    }
    public void StartMovement(Vector3 position)
    {
        ct = (transform.position.x - minX.position.x) / (maxX.position.x - minX.position.x);
        float x = Mathf.Lerp(minX.position.x, maxX.position.x, ct);
        transform.position = new Vector3(
                x,
                yMovement.Evaluate(ct) * yAmplitude + initialY,
                transform.position.z
                );
        anim.SetBool(walkingAnimation, true);
        StartCoroutine(Move());
    }

    public override void StartMoving(Vector3 position, GameData.VOID_FUNCTION complete, int sound, string animation, float speed = 1.0f, float acceptedDistance = 1.0f)
    {
        ct = (transform.position.x - minX.position.x) / (maxX.position.x - minX.position.x);
        float x = Mathf.Lerp(minX.position.x, maxX.position.x, ct);
        transform.position = new Vector3(
                x,
                yMovement.Evaluate(ct) * yAmplitude + initialY,
                transform.position.z
                );
        anim.SetBool(walkingAnimation, true);
      

        
        position = new Vector3(position.x, transform.position.y, position.z);
        StopMovement();
       
        this.complete = complete;
        this.animation = animation;
        
        if (animation != "" && anim != null) anim.SetBool(animation, true);
        StartCoroutine(Move(position + offSet));
    }

    public void MovementCompleted()
    {
        if(complete!=null)complete();
        StopMovement();
    }
    public override void StopMovement()
    {
        anim.SetBool(walkingAnimation, false);
        StopAllCoroutines();
    }
    private IEnumerator Move()
    {
       
        while(ct<1){
            ct += Time.deltaTime * Speed;
            yield return new WaitForEndOfFrame();
            float x = Mathf.Lerp(minX.position.x, maxX.position.x, ct);
            transform.position = new Vector3(
                    x,
                    yMovement.Evaluate(ct)*yAmplitude + initialY ,
                    transform.position.z
                    );
        }
        StopMovement();
    }
    private IEnumerator Move(Vector3 position)
    {
        float finish = (position.x - minX.position.x) / (maxX.position.x - minX.position.x); 
        while (ct < finish-.09f)
        {
           
            ct += Time.deltaTime * Speed;
            
            float x = Mathf.Lerp(minX.position.x, maxX.position.x, ct);
            transform.position = new Vector3(
                    x,
                    yMovement.Evaluate(ct) * yAmplitude + initialY,
                    transform.position.z
                    );
            yield return new WaitForEndOfFrame();
        }
        MovementCompleted();
        
    }
}
