using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance;

    public bool PlayOnAwake;
	[Range(.0f,1.0f)]
	public float lowVolume = .2f;
	[Range(.0f,1.0f)]
	public float highVolume = 1.0f;
    public InstrumentManager[] instruments;
	
    private void Update()
    {
        if (PlayOnAwake)
        {
			if(GameData.Memory != null)
	            foreach (FlowersInMemory f in GameData.Memory)
	            {
	                if(f.trackID!=-1)instruments[f.instrumentID].AddSound(f.trackID);
			    }
            
            PlayOnAwake = false;
        }
    }

    private void Awake()
    {
        Instance = this;
    }
    public void FadeAllDown(int instrumentException)
    {
       
        for(int i = 0; i<instruments.Length; i++){
				if(instrumentException == i){
				     instruments[i].FadeDown(0);
				}else{
				     instruments[i].FadeDown();
				}
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
        
        if(i!=-1){
	        FlowerSources[i].PlaySound();
			isPlaying = true;
			}
		CleanUp();
    }
    public void FadeDown()
    {
		
        if (currentTrack != -1) FlowerSources[currentTrack].FadeSoundTo(SoundManager.Instance.lowVolume);
        
    }
	public void FadeDown(float volume)
	{
		
		if (currentTrack != -1) FlowerSources[currentTrack].FadeSoundTo(volume);
		
	}
    public void FadeUp()
    {
		
        if (currentTrack != -1) FlowerSources[currentTrack].FadeSoundTo(SoundManager.Instance.highVolume);
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
