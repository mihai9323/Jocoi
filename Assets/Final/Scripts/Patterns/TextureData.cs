/*
 * Mihai-Ovidiu Anton 12-10-2014
 * 
 * */
using UnityEngine;
using System.Collections;

//singleton structure used to configure the textures used in the animation
public class TextureData :MonoBehaviour {
    
    public static TextureData Instance;
    //speed of the animation
    public int framesPerSecond;
    //how many frames per one row of animation
    public int rows;
    //how many frames per one column of animation
    public int columns;
    //resolution of the total texture atlas
    public int width, height;
	// Use this for initialization
	void Awake () {
        Instance = this;
	}
	
	
}
