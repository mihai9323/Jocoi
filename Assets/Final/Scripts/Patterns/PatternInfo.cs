/*
 * Mihai-Ovidiu Anton 12-10-2014
 * 
 * */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class PatternInfo  {
    //holds the color of the pattern
    public Color color = Color.white;
    public List<Color[]> croppedPatterns;
    public int frameNumber
    {
        set
        {
            _frameNumber = value % (TextureData.Instance.columns * TextureData.Instance.rows);
            
        }
        get
        {
            return _frameNumber;
        }

    }
    //the pattern
    public Texture2D texture;
    //the frame at which the animation is currently
    
    public int _frameNumber = 0;

    public int flowerSource;


    //crops only the current frame of the animation and returns it as an array of Color
    public Color[] CropFrame()
    {
        //Color[] returnedImage;
        if (croppedPatterns == null)
        {
            croppedPatterns = new List<Color[]>();
            CalculateCrops();
        }
        return croppedPatterns[frameNumber];

       

    }
    private Color[] CropFrame(int f)
    {
        //crops the frame specified in f
        Color[] returnedImage;
       


        returnedImage = texture.GetPixels(
                            (f % TextureData.Instance.columns) * TextureData.Instance.width / TextureData.Instance.columns,
                            ((TextureData.Instance.columns * TextureData.Instance.rows - 1 - f) / TextureData.Instance.rows) * TextureData.Instance.height / TextureData.Instance.rows,
                            TextureData.Instance.width / TextureData.Instance.columns,
                            TextureData.Instance.height / TextureData.Instance.rows
           );
        return returnedImage;
    }
    private void CalculateCrops()
    {
        //crops all the frames and stores them into croppedPatterns
        //all the calculations are done when adding a pattern instead of making them every time
        for (int i = 0; i < TextureData.Instance.rows * TextureData.Instance.columns; i++)
        {
           
            croppedPatterns.Add(CropFrame(i));
        }

    }
}
