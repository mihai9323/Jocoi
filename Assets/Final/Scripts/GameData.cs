using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameData  {
    public delegate void VOID_FUNCTION();
    public delegate void VOID_FUNCTION_INT(int i);
    public delegate void VOID_FUNCITON_PATH(Vector3[] path);

    public static List<Flower> Memory;
    public static int PuzzleProgress
    {
        set
        {
            _PuzzleProgress = Mathf.Clamp(value,0,Memory.Count);
            
        }
        get
        {
            return _PuzzleProgress;
        }
    }
    private static int _PuzzleProgress;
    public static void addToMemory(Flower flower)
    {
        if (Memory == null) Memory = new List<Flower>();
        Memory.Add(flower);
    }
}
