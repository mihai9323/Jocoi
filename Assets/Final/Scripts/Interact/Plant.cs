using UnityEngine;
using System.Collections;

public class Plant : InteractableObject {

    public string DieAnimation;
    public string GrowAnimation;
    public string BeEatenAnimation;
    public string BePickedAnimation;
    public string LambFedAnimation;
    public string MotherEatAnimation;

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
    }
    
    public override void StartRMB()
    {
        
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
        Debug.Log("eat");
        if (MotherEatSound != null)
        {
            AudioSource.PlayClipAtPoint(MotherEatSound, transform.position);
        }
    }
    public virtual void Feed()
    {
        Debug.Log("feed");
        if (BePickedSound != null)
        {
            AudioSource.PlayClipAtPoint(BePickedSound, transform.position);
        }
    }
    private void Destroy()
    {
        if (Inputs.Instance.ActiveObject == this)
        {
            Inputs.Instance.ActiveObjectDestroyed();
        }
    }
    
   
}
