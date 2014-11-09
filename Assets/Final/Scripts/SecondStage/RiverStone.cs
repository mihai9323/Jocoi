using UnityEngine;
using System.Collections;

public class RiverStone : MonoBehaviour {

    public Color[] colors;
    public int audioSourceInfluenced;





    private void OnMouseOver()
    {


        SoundManager.Instance.FlowerSources[audioSourceInfluenced].volumeLevel = SoundManager.Instance.FlowerSources[audioSourceInfluenced].IncreaseVolume(SoundManager.Instance.FlowerSources[audioSourceInfluenced].fixedLevel);
        
        this.gameObject.GetComponent<Animator>().SetBool("Hover", true);



    }
    private void OnMouseExit()
    {
        SoundManager.Instance.FlowerSources[audioSourceInfluenced].volumeLevel = SoundManager.Instance.FlowerSources[audioSourceInfluenced].fixedLevel;
        
        this.gameObject.GetComponent<Animator>().SetBool("Hover", false);
    }

    public void ActivateStone()
    {
        for (int i = 0; i < colors.Length && i<renderer.materials.Length; i++)
        {
            renderer.materials[i].color = colors[i];
        }
        SoundManager.Instance.FlowerSources[audioSourceInfluenced].fixedLevel++;
    }

   

}
