using UnityEngine;
using System.Collections;

public class Movement2D : MoveToPosition {

   
    private Transform minX;
    private Transform maxX;
    private float yAmplitude;
    [Range(0.01f,1.2f)]
    public float Speed;
    
    public string walkingAnimation;
    internal float initialY;
    private float ct;

    private GameData.VOID_FUNCTION complete;
    
   
    private void Start()
    {

       
       minX = WorldCurve.Instance.minX;
       maxX = WorldCurve.Instance.maxX;
       yAmplitude = WorldCurve.Instance.amplitude;
       initialY = minX.position.y;
        ct = (transform.position.x - minX.position.x) / (maxX.position.x - minX.position.x);
        float x = Mathf.Lerp(minX.position.x, maxX.position.x, ct);
        transform.position = new Vector3(
                x,
                WorldCurve.Instance.worldCurve.Evaluate(ct) * yAmplitude + initialY,
                transform.position.z
                );
       
    }
    public void StartMovement(Vector3 position)
    {
        ct = (transform.position.x - minX.position.x) / (maxX.position.x - minX.position.x);
        float x = Mathf.Lerp(minX.position.x, maxX.position.x, ct);
        transform.position = new Vector3(
                x,
                WorldCurve.Instance.worldCurve.Evaluate(ct) * yAmplitude + initialY,
                transform.position.z
                );
        anim.SetBool(walkingAnimation, true);
        this.animation = walkingAnimation;
        StartCoroutine(Move());
    }

    public override void StartMoving(Vector3 position, GameData.VOID_FUNCTION complete, int sound, string animation, float speed = 1.0f, float acceptedDistance = 1.0f)
    {
        ct = (transform.position.x - minX.position.x) / (maxX.position.x - minX.position.x);
        float x = Mathf.Lerp(minX.position.x, maxX.position.x, ct);
        transform.position = new Vector3(
                x,
                WorldCurve.Instance.worldCurve.Evaluate(ct) * yAmplitude + initialY,
                transform.position.z
                );
       

        
        position = new Vector3(position.x, transform.position.y, position.z);
        StopMovement();
       
        this.complete = complete;
        this.animation = animation;
        
        if (animation != "" && anim != null) anim.SetBool(animation, true);
        StartCoroutine(Move(position + offSet));
    }
    public override void StartMoving(Transform pos, GameData.VOID_FUNCTION complete, int sound, string animation, float speed = 1.0f, float acceptedDistance = 1.0f)
    {
        ct = (transform.position.x - minX.position.x) / (maxX.position.x - minX.position.x);
        float x = Mathf.Lerp(minX.position.x, maxX.position.x, ct);
        transform.position = new Vector3(
                x,
                WorldCurve.Instance.worldCurve.Evaluate(ct) * yAmplitude + initialY,
                transform.position.z
                );




        
        StopMovement();

        this.complete = complete;
        this.animation = animation;

        if (animation != "" && anim != null) anim.SetBool(animation, true);
        StartCoroutine(Move(pos));
    }
    //move without animation and sound
    public override void StartMoving(Transform position, GameData.VOID_FUNCTION complete, float speed = 1.0f, float acceptedDistance = 1.0f)
    {

        successDistance = acceptedDistance;
        StopMovement();
        this.complete = complete;
     
        StartCoroutine(Move(position));
    }
    public void MovementCompleted()
    {
        if(complete!=null)complete();
        StopMovement();
    }
    public override void StopMovement()
    {
        anim.SetBool(animation, false);
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
                    WorldCurve.Instance.worldCurve.Evaluate(ct) * yAmplitude + initialY,
                    transform.position.z
                    );
        }
        StopMovement();
    }
    private IEnumerator Move(Vector3 position)
    {
        float finish = (position.x - minX.position.x) / (maxX.position.x - minX.position.x); 
        while (ct < finish)
        {
           
            ct += Time.deltaTime * Speed;
            
            float x = Mathf.Lerp(minX.position.x, maxX.position.x, ct);
            transform.position = new Vector3(
                    x,
                    WorldCurve.Instance.worldCurve.Evaluate(ct) * yAmplitude + initialY,
                    transform.position.z
                    );
            yield return new WaitForEndOfFrame();
        }
        MovementCompleted();
        
    }

    private IEnumerator Move(Transform trans)
    {
        Vector3 initialTransPosition = trans.position;
        float finish = (trans.position.x+offSet.x - minX.position.x) / (maxX.position.x - minX.position.x);
        while (ct < finish)
        {

            ct += Time.deltaTime * Speed;

            float x = Mathf.Lerp(minX.position.x, maxX.position.x, ct);
            transform.position = new Vector3(
                    x,
                    WorldCurve.Instance.worldCurve.Evaluate(ct) * yAmplitude + initialY,
                    transform.position.z
                    );
            yield return new WaitForEndOfFrame();
            if ((initialTransPosition).sqrMagnitude != trans.position.sqrMagnitude)
            {
                finish = (trans.position.x - minX.position.x) / (maxX.position.x - minX.position.x);
            }
        }
        MovementCompleted();

    }
}
