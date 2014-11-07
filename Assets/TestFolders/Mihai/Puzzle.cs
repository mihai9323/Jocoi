using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Puzzle : MonoBehaviour {

    public static Puzzle Instance;

    


    
    
  


    private PatternInfo[] Memories;

    private void Awalke()
    {
        Instance = this;
    }

    private void LoadMemories()
    {
        Memories = GameData.Memory.ToArray();
    }



   

    
}
