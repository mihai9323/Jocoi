using UnityEngine;
using System.Collections;

public class RiverStone : MonoBehaviour {

    public Color[] colors;
    public int colorInfluenced;
	public int typeInfluenced;





    private void OnMouseOver()
    {


        SoundManager.Instance.FlowerSources[colorInfluenced].volumeLevel = SoundManager.Instance.FlowerSources[colorInfluenced].IncreaseVolume(SoundManager.Instance.FlowerSources[colorInfluenced].fixedLevel);
		SoundManager.Instance.FlowerSources[typeInfluenced].volumeLevel = SoundManager.Instance.FlowerSources[typeInfluenced].IncreaseVolume(SoundManager.Instance.FlowerSources[typeInfluenced].fixedLevel);
        this.gameObject.GetComponent<Animator>().SetBool("Hover", true);



    }
    private void OnMouseExit()
    {
        SoundManager.Instance.FlowerSources[colorInfluenced].volumeLevel = SoundManager.Instance.FlowerSources[colorInfluenced].fixedLevel;
		SoundManager.Instance.FlowerSources[typeInfluenced].volumeLevel = SoundManager.Instance.FlowerSources[typeInfluenced].fixedLevel;
        this.gameObject.GetComponent<Animator>().SetBool("Hover", false);
    }

    public void ActivateStone()
    {
        for (int i = 0; i < colors.Length && i<renderer.materials.Length; i++)
        {
            renderer.materials[i].color = colors[i];
        }
        SoundManager.Instance.FlowerSources[colorInfluenced].fixedLevel++;

		SoundManager.Instance.FlowerSources[typeInfluenced].fixedLevel++;}

   

}
