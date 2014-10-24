using UnityEngine;
using System.Collections;

class AnimateTexture : MonoBehaviour
{
    public int columns = 2;
    public int rows = 2;
    public float framesPerSecond = 10f;
    private Vector2 size;
    //the current frame to display
    private int index = 0;

    void Start()
    {
       

        //set the tile size of the texture (in UV units), based on the rows and columns
        size = new Vector2(1f / columns, 1f / rows);
        StartCoroutine(updateTiling());
        renderer.sharedMaterial.SetTextureScale("_MainTex", size);
    }

    private IEnumerator updateTiling()
    {
        int iX = 0;
        int iY = 0;
        while (true)
        {
            //move to the next index
             
            iX++;
            if (iX / rows == 1)
            {
                if (columns != 1) iY++;
                iX = 0;
                if (iY / columns == 1)
                {
                    iY = 0;
                }
            }
            Vector2 offset = new Vector2(iX * size.x,
                                          1 - (size.y * iY));
            renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);

            yield return new WaitForSeconds(1f / framesPerSecond);
        }

    }
}