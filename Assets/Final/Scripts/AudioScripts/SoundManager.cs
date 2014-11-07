using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance;
    public FlowerAudio[] FlowerSources;

    private void Awake()
    {
        Instance = this;
    }
}
