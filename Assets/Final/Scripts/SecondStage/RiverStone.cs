using UnityEngine;
using System.Collections;

public class RiverStone : MonoBehaviour {

    public Color[] colors;

    public int audioSourceInfluenced1;
    public int audioSourceInfluenced2;






    private void OnMouseOver()
    {



        SoundManager.Instance.FlowerSources[audioSourceInfluenced1].volumeLevel = SoundManager.Instance.FlowerSources[audioSourceInfluenced1].IncreaseVolume(SoundManager.Instance.FlowerSources[audioSourceInfluenced1].fixedLevel);

        SoundManager.Instance.FlowerSources[audioSourceInfluenced2].volumeLevel = SoundManager.Instance.FlowerSources[audioSourceInfluenced2].IncreaseVolume(SoundManager.Instance.FlowerSources[audioSourceInfluenced2].fixedLevel);
        
     


    }
    private void OnMouseExit()
    {

        SoundManager.Instance.FlowerSources[audioSourceInfluenced1].volumeLevel = SoundManager.Instance.FlowerSources[audioSourceInfluenced1].fixedLevel;

        SoundManager.Instance.FlowerSources[audioSourceInfluenced2].volumeLevel = SoundManager.Instance.FlowerSources[audioSourceInfluenced2].fixedLevel;
      
    }

    public void ActivateStone()
    {
        for (int i = 0; i < colors.Length && i<renderer.materials.Length; i++)
        {
            renderer.materials[i].color = colors[i];
        }

        SoundManager.Instance.FlowerSources[audioSourceInfluenced1].fixedLevel++;
        SoundManager.Instance.FlowerSources[audioSourceInfluenced2].fixedLevel++;
    }

   

}
