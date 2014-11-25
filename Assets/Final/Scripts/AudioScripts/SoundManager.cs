using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance;

    public bool PlayOnAwake;
    public InstrumentManager[] instruments;

    private void Update()
    {
        if (PlayOnAwake)
        {
            foreach (Flower f in GameData.Memory)
            {
                instruments[f.patternToAdd.instrumentID].AddSound(f.patternToAdd.trackID);
            }
            PlayOnAwake = false;
        }
    }

    private void Awake()
    {
        Instance = this;
    }
    public void FadeAllDown()
    {
        foreach (InstrumentManager im in instruments)
        {
            im.FadeDown();
        }
    }
    public void FadeAllUp()
    {
        foreach (InstrumentManager im in instruments)
        {
            im.FadeUp();
        }
    }
    public void StopAll()
    {
        foreach (InstrumentManager im in instruments)
        {
            im.StopSound();
        }
    }

}
[System.Serializable]
public class InstrumentManager
{
    public FlowerAudio[] FlowerSources;

    internal bool isPlaying = false;
    internal int currentTrack = -1;


    public InstrumentManager()
    {
        isPlaying = false;
        currentTrack = -1;
        CleanUp();
    }

    public void AddSound(int i){
		
        if (currentTrack != -1) FlowerSources[currentTrack].StopSound();
        currentTrack = i;
        FlowerSources[i].PlaySound();
        isPlaying = true;
		CleanUp();
    }
    public void FadeDown()
    {
		
        if (currentTrack != -1) FlowerSources[currentTrack].FadeSoundTo(.2f);
        
    }
    public void FadeUp()
    {
		
        if (currentTrack != -1) FlowerSources[currentTrack].FadeSoundTo(1.0f);
    }
    public void StopSound()
    {
		
        if (currentTrack != -1) FlowerSources[currentTrack].StopSound();
        currentTrack = -1;
        
    }
    public void CleanUp(){
		if(FlowerSources!=null){
			for(int i = 0; i<FlowerSources.Length; i++){
				if(i != currentTrack){
					FlowerSources[i].audioSource.volume = 0;
				}
			}
		}
    }

}
