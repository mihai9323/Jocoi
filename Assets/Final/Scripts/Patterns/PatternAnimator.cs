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
    
    private int frameShader = 0;
    private void Start()
    {
        
       // frames = new Texture2D[TextureData.Instance.width * TextureData.Instance.height];
       AttachPaternsFromList();
        StartAnimation();
        StopAnimation();
        //StartCoroutine("AnimateTextures");
    }
    private void Update()
    {
        if (recalculate)
        {
            recalculate = false;
            //frames = new Texture2D[TextureData.Instance.width * TextureData.Instance.height];
			AttachPaternsFromList();
        }
    }
    private void AttachPaternsFromList(){
		if(patterns!=null){
			foreach(PatternInfo pi in patterns){
				switch(pi.instrumentID){
					case 0: body.material.SetTexture("_Pattern1",pi.texture);  body.material.SetTextureScale("_Pattern1", new Vector2(1.0f/TextureData.Instance.columns,1.0f/TextureData.Instance.rows)); body.material.SetColor("_P1Color",pi.color); break;
					case 1: body.material.SetTexture("_Pattern2",pi.texture); body.material.SetTextureScale("_Pattern2", new Vector2(1.0f/TextureData.Instance.columns,1.0f/TextureData.Instance.rows)); body.material.SetColor("_P2Color",pi.color);break;
					case 2: body.material.SetTexture("_Pattern3",pi.texture); body.material.SetTextureScale("_Pattern3", new Vector2(1.0f/TextureData.Instance.columns,1.0f/TextureData.Instance.rows)); body.material.SetColor("_P3Color",pi.color);break;
				    case 3: body.material.SetTexture("_Pattern4",pi.texture); body.material.SetTextureScale("_Pattern4", new Vector2(1.0f/TextureData.Instance.columns,1.0f/TextureData.Instance.rows)); body.material.SetColor("_P4Color",pi.color);break;
				}
			}
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
      //  ApplyTexture(false);
        //if a new pattern is added to the drawing we have to calculate the frames again so we reset the cache
      //  frames = new Texture2D[TextureData.Instance.width*TextureData.Instance.height];
		switch(pi.instrumentID){
		case 0: body.material.SetTexture("_Pattern1",pi.texture);  body.material.SetTextureScale("_Pattern1", new Vector2(1.0f/TextureData.Instance.columns,1.0f/TextureData.Instance.rows)); body.material.SetColor("_P1Color",pi.color); break;
		case 1: body.material.SetTexture("_Pattern2",pi.texture); body.material.SetTextureScale("_Pattern2", new Vector2(1.0f/TextureData.Instance.columns,1.0f/TextureData.Instance.rows)); body.material.SetColor("_P2Color",pi.color);break;
		case 2: body.material.SetTexture("_Pattern3",pi.texture); body.material.SetTextureScale("_Pattern3", new Vector2(1.0f/TextureData.Instance.columns,1.0f/TextureData.Instance.rows)); body.material.SetColor("_P3Color",pi.color);break;
		case 3: body.material.SetTexture("_Pattern4",pi.texture); body.material.SetTextureScale("_Pattern4", new Vector2(1.0f/TextureData.Instance.columns,1.0f/TextureData.Instance.rows)); body.material.SetColor("_P4Color",pi.color);break;
		}
		//pi.texture = null;
        
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
        
        SoundManager.Instance.instruments[pi.instrumentID].AddSound(pi.trackID);
        if (patterns == null) patterns = new List<PatternInfo>();
        patterns.Add(pi);
       // ApplyTexture(false);
        //if a new pattern is added to the drawing we have to calculate the frames again so we reset the cache
       // frames = new Texture2D[TextureData.Instance.width*TextureData.Instance.height];
		switch(pi.instrumentID){
		case 0: body.material.SetTexture("_Pattern1",pi.texture);  body.material.SetTextureScale("_Pattern1", new Vector2(1.0f/TextureData.Instance.columns,1.0f/TextureData.Instance.rows)); body.material.SetColor("_P1Color",pi.color); break;
		case 1: body.material.SetTexture("_Pattern2",pi.texture);  body.material.SetTextureScale("_Pattern2", new Vector2(1.0f/TextureData.Instance.columns,1.0f/TextureData.Instance.rows)); body.material.SetColor("_P2Color",pi.color);break;
		case 2: body.material.SetTexture("_Pattern3",pi.texture);  body.material.SetTextureScale("_Pattern3", new Vector2(1.0f/TextureData.Instance.columns,1.0f/TextureData.Instance.rows)); body.material.SetColor("_P3Color",pi.color);break;
		case 3: body.material.SetTexture("_Pattern4",pi.texture);  body.material.SetTextureScale("_Pattern4", new Vector2(1.0f/TextureData.Instance.columns,1.0f/TextureData.Instance.rows)); body.material.SetColor("_P4Color",pi.color);break;
		}
		//pi.texture = null;
	}
	
	public void RemoveLastPattern()
    {
		if(patterns!=null){
			//Debug.Log(patterns.Count + " " + GameData.Memory.Length);
        }
        if (patterns != null) if (patterns.Count > 0)
            {

                RemovePattern(patterns.Count - 1);
            }
		//Debug.Log(patterns.Count + " " + GameData.Memory.Length);
    }

    public void RemovePattern(int i)
    {
        if (patterns != null){
         if (patterns.Count > 0)
            {
			switch(patterns[i].instrumentID){
			    case 0: body.material.SetTexture("_Pattern1",null); body.material.SetTextureScale("_Pattern1", new Vector2(1.0f/TextureData.Instance.columns,1.0f/TextureData.Instance.rows)); body.material.SetColor("_P1Color",Color.white); break;
				case 1: body.material.SetTexture("_Pattern2",null); body.material.SetTextureScale("_Pattern2", new Vector2(1.0f/TextureData.Instance.columns,1.0f/TextureData.Instance.rows)); body.material.SetColor("_P2Color",Color.white);break;
				case 2: body.material.SetTexture("_Pattern3",null); body.material.SetTextureScale("_Pattern3", new Vector2(1.0f/TextureData.Instance.columns,1.0f/TextureData.Instance.rows)); body.material.SetColor("_P3Color",Color.white);break;
				case 3: body.material.SetTexture("_Pattern4",null); body.material.SetTextureScale("_Pattern4", new Vector2(1.0f/TextureData.Instance.columns,1.0f/TextureData.Instance.rows)); body.material.SetColor("_P4Color",Color.white);break;
				}
			SoundManager.Instance.instruments[patterns[i].instrumentID].currentTrack = -1;
                SoundManager.Instance.instruments[patterns[i].instrumentID].FlowerSources[patterns[i].trackID].StopSound();
				if(LevelData.Instance!=null) {
				    //LevelData.Instance.flowerPannels[patterns[i].flowerPannel].Outline.color = LevelData.Instance.flowerPannels[patterns[i].flowerPannel].OutlineNoFlower;
					if(LevelData.Instance.flowerPannels!=null)LevelData.Instance.flowerPannels[patterns[i].flowerPannel].RemoveFlowerColor();
					if(LevelData.Instance.flowerPannels!=null)LevelData.Instance.flowerPannels[patterns[i].flowerPannel].trackId = -1;
				    
			    }
                patterns.RemoveAt(i);
                
            }
        //ApplyTexture(false);
        //frames = new Texture2D[TextureData.Instance.width * TextureData.Instance.height];
		}
		
	}
	public void RemovePattern(PatternInfo pi)
    {
		switch(pi.instrumentID){
			case 0: body.material.SetTexture("_Pattern1",null); body.material.SetTextureScale("_Pattern1", new Vector2(1.0f/TextureData.Instance.columns,1.0f/TextureData.Instance.rows)); body.material.SetColor("_P1Color",Color.white); break;
			case 1: body.material.SetTexture("_Pattern2",null); body.material.SetTextureScale("_Pattern2", new Vector2(1.0f/TextureData.Instance.columns,1.0f/TextureData.Instance.rows)); body.material.SetColor("_P2Color",Color.white);break;
			case 2: body.material.SetTexture("_Pattern3",null); body.material.SetTextureScale("_Pattern3", new Vector2(1.0f/TextureData.Instance.columns,1.0f/TextureData.Instance.rows)); body.material.SetColor("_P3Color",Color.white);break;
		    case 3: body.material.SetTexture("_Pattern4",null); body.material.SetTextureScale("_Pattern4", new Vector2(1.0f/TextureData.Instance.columns,1.0f/TextureData.Instance.rows)); body.material.SetColor("_P4Color",Color.white);break;
		}
		
		if (patterns != null) if (patterns.Count > 0) patterns.Remove(pi);
        //ApplyTexture(false);
        //frames = new Texture2D[TextureData.Instance.width * TextureData.Instance.height];
        
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
           // pi.frameNumber += currentFrame;
        }
    }
    //animates the textures
    private IEnumerator AnimateTextures()
    {
		Debug.Log ("Animate");
        while (true)
        {
            //ApplyTexture(true);
			body.material.SetTextureOffset("_Pattern1",new Vector2(1.0f/TextureData.Instance.columns* (frameShader%TextureData.Instance.columns),-1.0f/TextureData.Instance.columns* (frameShader/TextureData.Instance.columns)));
			body.material.SetTextureOffset("_Pattern2",new Vector2(1.0f/TextureData.Instance.columns* (frameShader%TextureData.Instance.columns),-1.0f/TextureData.Instance.columns* (frameShader/TextureData.Instance.columns)));
			body.material.SetTextureOffset("_Pattern3",new Vector2(1.0f/TextureData.Instance.columns* (frameShader%TextureData.Instance.columns),-1.0f/TextureData.Instance.columns* (frameShader/TextureData.Instance.columns)));
			body.material.SetTextureOffset("_Pattern4",new Vector2(1.0f/TextureData.Instance.columns* (frameShader%TextureData.Instance.columns),-1.0f/TextureData.Instance.columns* (frameShader/TextureData.Instance.columns)));
			frameShader++;
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
