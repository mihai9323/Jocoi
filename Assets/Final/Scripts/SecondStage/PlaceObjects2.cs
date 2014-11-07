using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class PlaceObjects2 : MonoBehaviour {
   
    public AnimationCurve animX;
    public AnimationCurve animY;
    public AnimationCurve animZ;

    public float xScale, yScale, zScale;
    private GameObject[] debugObj;

    private void Update()
    {
        //  Memories = GameData.Memory.ToArray();
        debugObj = GameObject.FindGameObjectsWithTag("Child");
        float distance = 1.0f / (debugObj.Length + 1);
        for (int i = 0; i < debugObj.Length; i++)
        {
            float ox = animX.Evaluate(i * distance);
            float oy = animY.Evaluate(i * distance);
            float oz = animZ.Evaluate(i * distance);


            debugObj[i].transform.position = new Vector3(

                ox *xScale,
                oy * yScale,
                oz * zScale


                );

        }
    }
}
