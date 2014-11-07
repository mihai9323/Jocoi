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
                        AudioSource.PlayClipAtPoint(FlowerSound, transform.position);
                    }
                }
            }
            if (_state != value)
            {
                TallGraphic.SetActive(false);
                HighGraphic.SetActive(false);
                MediumGraphic.SetActive(false);
                LowGraphic.SetActive(false);
                flowerGraphic.SetActive(false);
                switch (value)
                {
                    case GrassStates.TallGrass: TallGraphic.SetActive(true); break;
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
    

        public override void StartLMB()
        {
            AudioSource.PlayClipAtPoint(BaahSound, transform.position);
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
                        1.0f

                    );
            }
        }

        public override void StopLMB()
        {
            throw new NotImplementedException();
        }

        public override void StopRMB()
        {
            throw new NotImplementedException();
        }

        public override void StopAllInteractions()
        {
            throw new NotImplementedException();
        }

        private void StartEating()
        {
            LevelData.Instance.MotherSheep.transform.FaceObjectOnAxis(transform, Vector3.one);
            LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().anim.SetBool("Eat", true);
            AudioSource.PlayClipAtPoint(EatSound, transform.position);
            Invoke("FinishedEating", 1.0f);
        }
        private void FinishedEating()
        {
            LevelData.Instance.MotherSheep.GetComponent<MoveToPosition>().anim.SetBool("Eat", false);
            state = state + 1;
        }
        private void ResetState()
        {
            AudioSource.PlayClipAtPoint(GrowBackSound, transform.position);
        }
    }

