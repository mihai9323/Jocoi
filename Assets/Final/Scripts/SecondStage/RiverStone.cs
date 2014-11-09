using UnityEngine;
using System.Collections;

public class RiverStone : MonoBehaviour {

    public Color[] colors;
<<<<<<< HEAD
    public int colorInfluenced;
	public int typeInfluenced;
=======
    public int audioSourceInfluenced1;
    public int audioSourceInfluenced2;
>>>>>>> 7b1461253391a3c034e9313ebb2964f9888713bc





    private void OnMouseOver()
    {


<<<<<<< HEAD
        SoundManager.Instance.FlowerSources[colorInfluenced].volumeLevel = SoundManager.Instance.FlowerSources[colorInfluenced].IncreaseVolume(SoundManager.Instance.FlowerSources[colorInfluenced].fixedLevel);
		SoundManager.Instance.FlowerSources[typeInfluenced].volumeLevel = SoundManager.Instance.FlowerSources[typeInfluenced].IncreaseVolume(SoundManager.Instance.FlowerSources[typeInfluenced].fixedLevel);
        this.gameObject.GetComponent<Animator>().SetBool("Hover", true);
=======
        SoundManager.Instance.FlowerSources[audioSourceInfluenced1].volumeLevel = SoundManager.Instance.FlowerSources[audioSourceInfluenced1].IncreaseVolume(SoundManager.Instance.FlowerSources[audioSourceInfluenced1].fixedLevel);

        SoundManager.Instance.FlowerSources[audioSourceInfluenced2].volumeLevel = SoundManager.Instance.FlowerSources[audioSourceInfluenced2].IncreaseVolume(SoundManager.Instance.FlowerSources[audioSourceInfluenced2].fixedLevel);
        
       // this.gameObject.GetComponent<Animator>().SetBool("Hover", true);
>>>>>>> 7b1461253391a3c034e9313ebb2964f9888713bc



    }
    private void OnMouseExit()
    {
<<<<<<< HEAD
        SoundManager.Instance.FlowerSources[colorInfluenced].volumeLevel = SoundManager.Instance.FlowerSources[colorInfluenced].fixedLevel;
		SoundManager.Instance.FlowerSources[typeInfluenced].volumeLevel = SoundManager.Instance.FlowerSources[typeInfluenced].fixedLevel;
        this.gameObject.GetComponent<Animator>().SetBool("Hover", false);
=======
        SoundManager.Instance.FlowerSources[audioSourceInfluenced1].volumeLevel = SoundManager.Instance.FlowerSources[audioSourceInfluenced1].fixedLevel;

        SoundManager.Instance.FlowerSources[audioSourceInfluenced2].volumeLevel = SoundManager.Instance.FlowerSources[audioSourceInfluenced2].fixedLevel;
        
       // this.gameObject.GetComponent<Animator>().SetBool("Hover", false);
>>>>>>> 7b1461253391a3c034e9313ebb2964f9888713bc
    }

    public void ActivateStone()
    {
        for (int i = 0; i < colors.Length && i<renderer.materials.Length; i++)
        {
            renderer.materials[i].color = colors[i];
        }
<<<<<<< HEAD
        SoundManager.Instance.FlowerSources[colorInfluenced].fixedLevel++;

		SoundManager.Instance.FlowerSources[typeInfluenced].fixedLevel++;}
=======
        SoundManager.Instance.FlowerSources[audioSourceInfluenced1].fixedLevel++;
        SoundManager.Instance.FlowerSources[audioSourceInfluenced2].fixedLevel++;
    }
>>>>>>> 7b1461253391a3c034e9313ebb2964f9888713bc

   

}
