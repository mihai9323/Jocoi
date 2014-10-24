using UnityEngine;
using System.Collections;

public class SheepInHerd : InteractableObject {

    private MoveToPosition mtp;
    private bool moving;
	// Use this for initialization
	void Awake () {
        moving = false;
        if (GetComponent<MoveToPosition>() != null)
        {
            mtp = GetComponent<MoveToPosition>();
        }
        else mtp = gameObject.AddComponent<MoveToPosition>();
        StartCoroutine(checkDistanceFromTarget());
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    private IEnumerator checkDistanceFromTarget()
    {
        yield return new WaitForSeconds(Random.value);
        while (true)
        {
            if(!moving){
                if (Vector3.Distance(transform.position, LevelData.Instance.herdTarget.transform.position) > 3.0f)
                {
                    MoveToNewPosition(LevelData.Instance.herdTarget.transform.position);
                }
            }
            yield return new WaitForSeconds(1f);

        }
    }
    public void MoveToNewPosition(Vector3 position)
    {
        moving = true;
        mtp.StartMoving(position, ReachedDestination, Random.Range(.8f, 1.2f) * LevelData.Instance.MotherSpeed);
    }
    private void ReachedDestination()
    {
        moving = false;
    }

    public override void StartLMB()
    {
        throw new System.NotImplementedException();
    }

    public override void StartRMB()
    {
        throw new System.NotImplementedException();
    }

    public override void StopLMB()
    {
        throw new System.NotImplementedException();
    }

    public override void StopRMB()
    {
        throw new System.NotImplementedException();
    }

    public override void StopAllInteractions()
    {
        throw new System.NotImplementedException();
    }
}
