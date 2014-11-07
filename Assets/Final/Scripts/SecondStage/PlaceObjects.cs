using UnityEngine;
using System.Collections;


public class PlaceObjects : MonoBehaviour {

    public AnimationCurve line;
    public float xMultiplier, amplitude;

    public enum Axis
    {
        XY,XZ,YX,YZ,ZX,ZY

    }
    public Axis axis;
    public GameObject[] objectsToPlace;

    public bool placeOnStart;
    
    private void Start()
    {
        
        if (placeOnStart) PlaceObjectsOnLine();
    }

    public void PlaceObjectsOnLine()
    {
        
        if(objectsToPlace != null){
            float distance = 1.0f / (objectsToPlace.Length+1);
            
            for (int i = 0; i < objectsToPlace.Length; i++)
            {
                float oy = line.Evaluate(i * distance) * amplitude;
                float ox = distance*i * xMultiplier;

                switch (axis)
                {
                    case Axis.XY: objectsToPlace[i].transform.position = new Vector3(ox, oy, 0); break;
                    case Axis.XZ: objectsToPlace[i].transform.position = new Vector3(ox, 0, oy) ; break;
                    case Axis.YX: objectsToPlace[i].transform.position = new Vector3(oy,ox,0) ; break;
                    case Axis.YZ: objectsToPlace[i].transform.position = new Vector3(0,ox,oy) ; break;
                    case Axis.ZX: objectsToPlace[i].transform.position = new Vector3(oy,0,ox) ; break;
                    case Axis.ZY: objectsToPlace[i].transform.position = new Vector3(0,oy,ox); break;
                }
            }
        }
    }
}
