using System;
using System.Collections;
using UnityEngine;




public class PuzzleGrass : InteractableObject
    {

    public enum GrassStates
    {
        TallGrass,
        HighGrass,
        Medium,
        Low,
        Flower
    }

    public GrassStates state{
        set
        {
            if (_state != GrassStates.Flower)
            {
                if (value == GrassStates.Flower)
                {
                    
                    if (!Puzzle.Instance.CheckFlower(this))
                    {
                        Invoke("ResetState", 1.5f);
                    }
                    else
                    {
                        if (FlowerSound != null) AudioSource.PlayClipAtPoint(FlowerSound, transform.position);
                    }
                }
            }
            if (_state != value )
            {
                TallGraphic.SetActive(false);
                HighGraphic.SetActive(false);
                MediumGraphic.SetActive(false);
                LowGraphic.SetActive(false);
                flowerGraphic.SetActive(false);
                switch (value)
                {
                    case GrassStates.TallGrass: TallGraphic.SetActive(true); TallGraphic.GetComponent<Animator>().SetTrigger("Grow"); break;
                    case GrassStates.HighGrass: HighGraphic.SetActive(true); break;
                    case GrassStates.Medium: MediumGraphic.SetActive(true); break;
                    case GrassStates.Low: LowGraphic.SetActive(true); break;
                    case GrassStates.Flower: flowerGraphic.SetActive(true); break;
                }
            }
            _state = value;
        }
        get
        {
            return _state;
        }
    }
    private GrassStates _state;


    public GameObject TallGraphic, HighGraphic, MediumGraphic, LowGraphic;
    
    internal GameObject flowerGraphic;

    public AudioClip BaahSound, GrowBackSound, FlowerSound, EatSound;
    private bool StartedEating;
    internal int instrumentID, trackID;
        public override void StartLMB()
        {
           if(BaahSound!=null) AudioSource.PlayClipAtPoint(BaahSound, transform.position);
        }

        public override void StartRMB()
        {
            if (state != GrassStates.Flower)
            {
                LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().StartMoving
                    (
                        transform.position,
                        StartEating,
                        0,
                        "Walk",
                        LevelData.Instance.MotherSpeed,
                        1.9f

                    );
            }
        }

        public override void StopLMB()
        {
           // throw new NotImplementedException();
        }

        public override void StopRMB()
        {
           // throw new NotImplementedException();
        }

        public override void StopAllInteractions()
        {

            LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().StopMovement();
        }

        private void StartEating()
        {
            StartedEating = true;
            LevelData.Instance.MotherSheep.transform.FaceObjectOnAxis(transform, Vector3.one);
            LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().anim.SetBool("Eat", true);
            if(EatSound!=null)AudioSource.PlayClipAtPoint(EatSound, transform.position);
            Invoke("FinishedEating", 1.0f);
            Inputs.Instance.canInteract = false;
            
        }
        private void FinishedEating()
        {
            LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().anim.SetBool("Eat", false);
            state = (GrassStates)Mathf.Clamp((int)state + 1,0,4);
            StartedEating = false;
            Inputs.Instance.canInteract = true;
        }
        private void ResetState()
        {
           if(GrowBackSound!=null) AudioSource.PlayClipAtPoint(GrowBackSound, transform.position);
           state = GrassStates.TallGrass;
        }

        private void OnMouseOver()
        {

            SoundManager.Instance.FadeAllDown();
            SoundManager.Instance.instruments[instrumentID].FlowerSources[trackID].FadeSoundTo(1.0f);

            switch (state)
            {
                case GrassStates.TallGrass: TallGraphic.GetComponent<Animator>().SetBool("Hover", true); break;
                case GrassStates.HighGrass: HighGraphic.GetComponent<Animator>().SetBool("Hover", true); break;
                case GrassStates.Medium: MediumGraphic.GetComponent<Animator>().SetBool("Hover", true); break;
                case GrassStates.Low: LowGraphic.GetComponent<Animator>().SetBool("Hover", true); break;
            }


        }
        private void OnMouseExit()
        {

            SoundManager.Instance.FadeAllUp();
            if (SoundManager.Instance.instruments[instrumentID].currentTrack != trackID) SoundManager.Instance.instruments[instrumentID].FlowerSources[trackID].FadeSoundTo(0.0f);
            switch (state)
            {
                case GrassStates.TallGrass: TallGraphic.GetComponent<Animator>().SetBool("Hover", false); break;
                case GrassStates.HighGrass: HighGraphic.GetComponent<Animator>().SetBool("Hover", false); break;
                case GrassStates.Medium: MediumGraphic.GetComponent<Animator>().SetBool("Hover", false); break;
                case GrassStates.Low: LowGraphic.GetComponent<Animator>().SetBool("Hover", false); break;
            }
        }
    }

