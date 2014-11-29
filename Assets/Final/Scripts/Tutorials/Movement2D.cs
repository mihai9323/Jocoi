using UnityEngine;
using System.Collections;

public class Movement2D : MonoBehaviour {

    public AnimationCurve yMovement;
    public Transform minX;
    public Transform maxX;
    public float yAmplitude;
    [Range(0.01f,1.2f)]
    public float speed;
    public Animator anim;
    public string walkingAnimation;
    internal float initialY;
    private float ct;
    private void Awake()
    {
        initialY = minX.position.y;
        
    }
    private void Start()
    {
        ct = (transform.position.x-minX.position.x)/(maxX.position.x - minX.position.x);
        float x = Mathf.Lerp(minX.position.x, maxX.position.x, ct);
        transform.position = new Vector3(
                x,
                yMovement.Evaluate(ct) * yAmplitude + initialY,
                transform.position.z
                );
    }
    public void MoveToPosition(Vector3 position)
    {
        anim.SetBool(walkingAnimation, true);
        StartCoroutine(StartMoving());
    }
    public void StopMovement()
    {
        anim.SetBool(walkingAnimation, false);
        StopAllCoroutines();
    }
    private IEnumerator StartMoving()
    {
       
        while(ct<1){
            ct += Time.deltaTime * speed;
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
}
