using UnityEngine;
using System.Collections;

public class HelperSheep : InteractableObject {

    private PuzzleGrass targetGrass;
    public AudioClip bahSound;
    public override void StartLMB()
    {
        //throw new System.NotImplementedException();
        
        targetGrass = DetectGrassPatch();
        AudioSource.PlayClipAtPoint(bahSound, transform.position);
        StartCoroutine("CallOtherSheep");
    }

    public override void StartRMB()
    {
        //throw new System.NotImplementedException();
    }

    public override void StopLMB()
    {
        //throw new System.NotImplementedException();
    }

    public override void StopRMB()
    {
        //throw new System.NotImplementedException();
    }

    public override void StopAllInteractions()
    {
        GetComponent<MoveToPosition>().StopMovement();
        StopAllCoroutines();
    }

    private PuzzleGrass DetectGrassPatch()
    {
        float minDistance = float.MaxValue;
        PuzzleGrass returnGrass = Puzzle.Instance.Grass[0];
        foreach (PuzzleGrass pg in Puzzle.Instance.Grass)
        {
            if (pg.state != PuzzleGrass.GrassStates.Flower)
            {
                if (minDistance > pg.transform.position.SquaredDistance(LevelData.Instance.MotherSheep.transform.position))
                {
                    minDistance = pg.transform.position.SquaredDistance(LevelData.Instance.MotherSheep.transform.position);
                    returnGrass = pg;
                }
            }
        }
        foreach (PuzzleGrass pg in Puzzle.Instance.IncorrectGrass)
        {
            if (pg.state != PuzzleGrass.GrassStates.Flower)
            {
                if (minDistance > pg.transform.position.SquaredDistance(LevelData.Instance.MotherSheep.transform.position))
                {
                    minDistance = pg.transform.position.SquaredDistance(LevelData.Instance.MotherSheep.transform.position);
                    returnGrass = pg;
                }
            }
        }
        return returnGrass;
    }

    private IEnumerator CallOtherSheep()
    {
        foreach (HelperSheep hs in LevelData.Instance.HelperSheep)
        {
            hs.StartMoving(targetGrass);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.3f) * 2);
        }
    }

    public void StartMoving(PuzzleGrass tg)
    {
        targetGrass = tg;
        GetComponent<MoveToPosition>().StartMovingOnPath(
            targetGrass.gameObject.transform,
            StartEating,
            0,
            "Walk",
            LevelData.Instance.MotherSpeed * Random.Range(.95f,1.05f),
            1.5f

            );

       
    }

    public void StartEating()
    {
        GetComponent<MoveToPosition>().anim.SetBool("Eat", true);
        Invoke("StopEating", 1.0f);
    }
    private void StopEating()
    {
       
        GetComponent<MoveToPosition>().anim.SetBool("Eat", false);
        if (targetGrass.state != PuzzleGrass.GrassStates.Flower) { 
            targetGrass.state++;
        }
    }
}
