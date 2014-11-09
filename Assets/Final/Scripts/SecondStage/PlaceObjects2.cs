using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class PlaceObjects2 : MonoBehaviour {
   
    public AnimationCurve animX;
    public AnimationCurve animY;
    public AnimationCurve animZ;

    public float xScale, yScale, zScale;
    public GameObject[] ObjectsToPlace;
    public bool editMode;
    private void Update()
    {
        if(editMode)PlaceObjects();
    }
    public void PlaceObjects()
    {
      
        if(editMode) ObjectsToPlace = GameObject.FindGameObjectsWithTag("Child");
        float distance = 1.0f / (ObjectsToPlace.Length + 1);
        for (int i = 0; i < ObjectsToPlace.Length; i++)
        {
            float ox = animX.Evaluate(i * distance);
            float oy = animY.Evaluate(i * distance);
            float oz = animZ.Evaluate(i * distance);


            ObjectsToPlace[i].transform.position = new Vector3(

                ox *xScale,
                oy * yScale,
                oz * zScale


                )+ transform.position;

        }
    }

    

   
}
