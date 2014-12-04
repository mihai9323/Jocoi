/*
 * Mihai-Ovidiu Anton 12-10-2014
 * 
 * */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PatternAnimator : MonoBehaviour {


    //A list with all the patterns
    public List<PatternInfo> patterns;
    private Texture2D[] frames;
    private int _currentFrame = 0;
    public Renderer body;
    public int currentFrame
    {
        set
        {
            _currentFrame = value % (TextureData.Instance.columns * TextureData.Instance.rows);

        }
        get
        {
            return _currentFrame;
        }
    }
   //The color of the pixels not affected by any pattern
    public Color defaultColor;
    public bool recalculate = true;
    private void Start()
    {
        
        frames = new Texture2D[TextureData.Instance.width * TextureData.Instance.height];
        StartAnimation();
        StopAnimation();
        //StartCoroutine("AnimateTextures");
    }
    private void Update()
    {
        if (recalculate)
        {
            recalculate = false;
            frames = new Texture2D[TextureData.Instance.width * TextureData.Instance.height];
        }
    }
    //used to AddPatterns
	public void AddPattern(PatternInfo pi){

        if (SoundManager.Instance.instruments[pi.instrumentID].currentTrack != -1)
        {
            PatternInfo patternToRemove = null;
            foreach (PatternInfo _pi in patterns)
            {
                if (_pi.instrumentID == pi.instrumentID)
                {
                    patternToRemove = _pi;
                }
            }
            if (patternToRemove != null)
            {
                RemovePattern(patternToRemove);
            }
        }
        SoundManager.Instance.instruments[pi.instrumentID].AddSound(pi.trackID);
        if (patterns == null) patterns = new List<PatternInfo>();
        patterns.Add(pi);
        ApplyTexture(false);
        //if a new pattern is added to the drawing we have to calculate the frames again so we reset the cache
        frames = new Texture2D[TextureData.Instance.width*TextureData.Instance.height];
        
    }
    public void AddPattern(Flower flower){
       
        PatternInfo pi = flower.patternToAdd;
        if (SoundManager.Instance.instruments[pi.instrumentID].currentTrack != -1)
        {
            PatternInfo patternToRemove = null;
            foreach (PatternInfo _pi in patterns)
            {
                if (_pi.instrumentID == pi.instrumentID)
                {
                    patternToRemove = _pi;
                }
            }
            if (patternToRemove != null)
            {
                RemovePattern(patternToRemove);
            }
        }
        GameData.addToMemory(flower);
        SoundManager.Instance.instruments[pi.instrumentID].AddSound(pi.trackID);
        if (patterns == null) patterns = new List<PatternInfo>();
        patterns.Add(pi);
        ApplyTexture(false);
        //if a new pattern is added to the drawing we have to calculate the frames again so we reset the cache
        frames = new Texture2D[TextureData.Instance.width*TextureData.Instance.height];
        
    }
   
    public void RemoveLastPattern()
    {
		if(patterns!=null){
			Debug.Log(patterns.Count + " " + GameData.Memory.Count);
        }
        if (patterns != null) if (patterns.Count > 0)
            {
                if (GameData.Memory != null) GameData.Memory.RemoveAt(GameData.Memory.Count - 1);
                RemovePattern(patterns.Count - 1);
            }
        Debug.Log(patterns.Count + " " + GameData.Memory.Count);
    }

    public void RemovePattern(int i)
    {
        if (patterns != null) if (patterns.Count > 0)
            {
                SoundManager.Instance.instruments[patterns[i].instrumentID].FlowerSources[patterns[i].trackID].StopSound();
				if(LevelData.Instance!=null) {
				    //LevelData.Instance.flowerPannels[patterns[i].flowerPannel].Outline.color = LevelData.Instance.flowerPannels[patterns[i].flowerPannel].OutlineNoFlower;
					if(LevelData.Instance.flowerPannels!=null)LevelData.Instance.flowerPannels[patterns[i].flowerPannel].RemoveFlowerColor();
				    
			    }
                patterns.RemoveAt(i);
                
            }
        ApplyTexture(false);
        frames = new Texture2D[TextureData.Instance.width * TextureData.Instance.height];
    }
    public void RemovePattern(PatternInfo pi)
    {
        GameData.removeFromMemory(pi);
        if (patterns != null) if (patterns.Count > 0) patterns.Remove(pi);
        ApplyTexture(false);
        frames = new Texture2D[TextureData.Instance.width * TextureData.Instance.height];
        
    }

    public void StartAnimation()
    {
        StopAnimation();
        StartCoroutine("AnimateTextures");
    }
    public void StopAnimation()
    {
        StopCoroutine("AnimateTextures");
        foreach (PatternInfo pi in patterns)
        {
            pi.frameNumber += currentFrame;
        }
    }
    //animates the textures
    private IEnumerator AnimateTextures()
    {
        while (true)
        {
            ApplyTexture(true);
            yield return new WaitForSeconds(1.0f / TextureData.Instance.framesPerSecond);
        }
    }

    //calculates the texture from all the patterns
    //if animate is on the patterns will move on the following frame after the generation is done
    public void ApplyTexture(bool animate)
    {
        //if the frame was not calculated we will do it again
        if (frames[currentFrame] == null)
        {
            //pixels that form the texture
            Color[] pixels = new Color[TextureData.Instance.width * TextureData.Instance.height / TextureData.Instance.columns / TextureData.Instance.rows];
            for (int i = 0; i < pixels.Length; i++)
            {
                //set all the pixels to the default color
                pixels[i] = defaultColor;
            }
            //go through all the patterns
            for (int i = 0; i < patterns.Count; i++)
            {
                //get only the frame needed from the texture
                Color[] patternPixels = patterns[i].CropFrame();
                //go through all the pixels from the pattern
                for (int c = 0; c < patternPixels.Length; c++)
                {
                    //if the pixels are not black we color the new texture accordingly 
                    //note we only cheack for the red channel as we assume that the input is in grayscale
                    if (patternPixels[c].r > 0.1f)
                    {
                        //the pixel gets colored by the amount of gray
                        pixels[c] = patterns[i].color * patternPixels[c].r;
                    }

                }
                //if the animate option is chosen we go on the next frame
                if (animate) patterns[i].frameNumber++;
            }

            //forming the texture
            Texture2D text = new Texture2D(TextureData.Instance.width / TextureData.Instance.columns, TextureData.Instance.height / TextureData.Instance.rows);
            text.SetPixels(pixels);
            text.Apply();
            frames[currentFrame] = text;
        }

        //applying the texture to the material
        body.material.mainTexture = frames[currentFrame];
        if (animate)
        {
            currentFrame++;
            for (int i = 0; i < patterns.Count; i++) {
                patterns[i].frameNumber++;
            }
        }
    }
    
}
