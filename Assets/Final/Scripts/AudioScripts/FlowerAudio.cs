using UnityEngine;
using System.Collections;

public class FlowerAudio : MonoBehaviour {

    public AudioSource audioSource;

    public enum Volume
    {
        none = 0,
        veryLow = 1,
        low = 2,
        medium = 3,
        high = 4,
        veryHigh = 5
    }

    public Volume volumeLevel{
        set
        {
            if (value != _volumeLevel)
            {
                _volumeLevel = value;
                audioSource.volume = (float)((int)value) * .2f;
            }
        }
        get
        {
            return _volumeLevel ;
        }
    }
    private Volume _volumeLevel;
    public Volume fixedLevel;

    public Volume IncreaseVolume(Volume vol){
        int fVol = Mathf.Clamp((int)vol + 1,0,5);
        return  (Volume)fVol;
    }
    public Volume DecreaseVolume(Volume vol)
    {
        int fVol = Mathf.Clamp((int)vol - 1, 0, 5);
        return  (Volume)fVol;
    }

   
}
