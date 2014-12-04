using UnityEngine;
using System.Collections;

public class Grass : Plant {

    protected override void LambEat()
    {
       
        base.LambEat();

        LevelData.Instance.Lamb.GetComponent<PatternAnimator>().RemoveLastPattern();
        LevelData.Instance.Lamb.GetComponent<PatternAnimator>().StartAnimation();
		

    }
    protected override void LambFinishEat()
    {
        LevelData.Instance.Lamb.GetComponent<PatternAnimator>().StopAnimation();
        

        base.LambFinishEat();
    }
    private void Start()
    {
        LevelData.Instance.grass.Add(this);


        /*for (int i =0; i<renderer.materials.Length; i++) {
=======
        /*
        for (int i =0; i<renderer.materials.Length; i++) {
>>>>>>> origin/master
            if(i==TrunchiNr) renderer.materials[i].color = trunchiColor;
            else{
                //Color c= patternToAdd.color.RGBtoHSI();
                //c.g = c.g - (float)i * 0.1f;
                //c = c.HSItoRGB();
                renderer.materials[i].color =  patternToAdd.color;

            }
        }
<<<<<<< HEAD
*/


    }
    protected override void OnDestroy()
    {
        LevelData.Instance.grass.Remove(this);
        base.OnDestroy();

    }
    private void OnMouseOver()
    {

      
        this.gameObject.GetComponent<Animator>().SetBool("Hover", true);



    }
    private void OnMouseExit()
    {

       
        this.gameObject.GetComponent<Animator>().SetBool("Hover", false);
    }
}
