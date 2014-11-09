using UnityEngine;
using System.Collections;

public class FlowerAudio : MonoBehaviour {

    public AudioSource audioSource;

    

    private void Start(){
        
        audioSource.volume = 0;
    }
    public void PlaySound()
    {
        StopAllCoroutines();
        StartCoroutine(FadeSound(1.0f));
       
    }
    public void StopSound()
    {
        StopAllCoroutines();
        StartCoroutine(FadeSound(0));
    }
    public void FadeSoundTo(float value)
    {
        StopAllCoroutines();
        StartCoroutine(FadeSound(value));
    }
    private IEnumerator FadeSound(float vol)
    {
        float ct = 0;
        while (ct < 1.0f)
        {
            ct += Time.fixedDeltaTime;
            audioSource.volume = Mathf.Lerp(audioSource.volume, vol, ct);
            yield return new WaitForFixedUpdate();
        }

    }
   
}
