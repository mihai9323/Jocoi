using UnityEngine;
using System.Collections;

public class Plant : InteractableObject {

    public string DieAnimation;
    public string GrowAnimation;
    public string BeEatenAnimation;
    public string BePickedAnimation;
    public string LambFedAnimation;
    public string MotherEatAnimation;

    public Transform PlantInMouth;

    public AudioClip DieSound;
    public AudioClip GrowSound;
    public AudioClip BeEatenSound;
    public AudioClip BePickedSound;
    public AudioClip LambFedSound;
    public AudioClip MotherEatSound;

    public Animator anim;
    public AudioSource audioSource;

    private void Awake()
    {
        if (anim == null)
        {
            if (this.gameObject.GetComponent<Animator>() != null)
            {
                anim = this.gameObject.GetComponent<Animator>();
            }
            else anim = this.gameObject.AddComponent<Animator>();
        }
        if (audioSource == null)
        {
            if (this.gameObject.GetComponent<AudioSource>() != null)
            {
                audioSource = this.gameObject.GetComponent<AudioSource>();
            }
            else audioSource = this.gameObject.AddComponent<AudioSource>();
        }
    }

    
    public override void StartLMB()
    {
        LevelData.Instance.currentAnimation = "Walk";
        LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().StartMoving(
                transform.position,
                Feed,
                0,
                "Walk",
                LevelData.Instance.MotherSpeed,
                1.8f

            );
        LevelData.Instance.Lamb.GetComponent<MoveToPosition>().StartMoving(
            LevelData.Instance.Lamb.GetComponent<Lamb>().motherHead.transform,
            null,
            1,
            "Walk",
            LevelData.Instance.LambSpeed ,
           2f
            );
    }
    
    public override void StartRMB()
    {
        LevelData.Instance.currentAnimation = "Walk";
        LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().StartMoving(
               transform.position,
               Eat,
               0,
               "Walk",
               LevelData.Instance.MotherSpeed,
               1.8f

           );
    }

    public override void StopLMB()
    {
        
    }

    public override void StopRMB()
    {
       
    }

    public override void StopAllInteractions()
    {
        
    }

    public void StartGorw()
    {
        if (GrowSound != null)
        {
            audioSource.clip = GrowSound;
            audioSource.Play();
        }
    }
    public void FinishedGrowing()
    {
        audioSource.Stop();
    }
    public void StartDie()
    {
        if (DieSound != null)
        {
            audioSource.clip = DieSound;
            audioSource.Play();
        }
    }
    public void FinishedDying()
    {
        audioSource.Stop();
    }
    public virtual void Eat()
    {
        //Debug.Log("eat");
        Inputs.Instance.canInteract = false;
        if (MotherEatSound != null)
        {
            AudioSource.PlayClipAtPoint(MotherEatSound, transform.position);
        }
        Invoke("FinishEat", 1.0f);
    }
    public virtual void Feed()
    {
        Inputs.Instance.canInteract = false;
        PlantInMouth = Instantiate(PlantInMouth, LevelData.Instance.Lamb.GetComponent<Lamb>().motherHead.position, LevelData.Instance.MotherSheep.transform.rotation) as Transform;
        PlantInMouth.parent = LevelData.Instance.Lamb.GetComponent<Lamb>().motherHead;
        this.gameObject.renderer.enabled = false;
       // Debug.Log("feed");
        if (BePickedSound != null)
        {
            AudioSource.PlayClipAtPoint(BePickedSound, transform.position);
        }
        LevelData.Instance.Lamb.GetComponent<MoveToPosition>().StartMoving(
            LevelData.Instance.Lamb.GetComponent<Lamb>().motherHead.transform.position + LevelData.Instance.Lamb.GetComponent<Lamb>().motherHead.transform.right*2.0f,
            LambEat,
            1,
            "Walk",
            LevelData.Instance.LambSpeed*2,
            4f
            );
    }
    private void Destroy()
    {
        if (Inputs.Instance.ActiveObject == this)
        {
            Inputs.Instance.ActiveObjectDestroyed();
        }
    }
    protected void LambEat()
    {
        LevelData.Instance.MotherSheep.transform.FaceObjectOnAxis(LevelData.Instance.LambHead, new Vector3(0, 1, 0));
        
        LevelData.Instance.Lamb.transform.FaceObjectOnAxis(LevelData.Instance.Lamb.GetComponent<Lamb>().motherHead.position, new Vector3(0, 1, 0));
        if (LevelData.Instance.Lamb.GetComponent<Animator>() != null)
        {
            LevelData.Instance.Lamb.GetComponent<Animator>().SetBool(LambFedAnimation, true);
        }
        Invoke("LambFinishEat", 1.0f);
    }
    protected void LambFinishEat()
    {
        Inputs.Instance.canInteract = true;
        Destroy(PlantInMouth.gameObject);
        Destroy(this.gameObject,0.1f);
    }
    protected void FinishEat()
    {
        Inputs.Instance.canInteract = true;
        Destroy(this.gameObject,0.1f);
    }
   
}
