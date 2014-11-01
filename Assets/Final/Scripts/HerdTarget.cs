using UnityEngine;
using System.Collections;

public class HerdTarget : MonoBehaviour {

    public float changePositionFrequency;
    public float delay;

    private float lastTimeMoved;
    public bool canMove
    {
        set
        {
            if (value == false)
            {
                StopAllCoroutines();

            }
            _canMove = value;
        }
        get
        {
            return _canMove;
        }
    }
    private bool _canMove;

	private void Start () {
        lastTimeMoved = Time.timeSinceLevelLoad;
        canMove = true;
        if (LevelData.Instance.Sheep != null)
        {
            changePositionFrequency += delay * LevelData.Instance.Sheep.Length;
        }
	}
	
	
	private void Update () {
        if (lastTimeMoved + changePositionFrequency < Time.timeSinceLevelLoad && canMove)
        {
            lastTimeMoved = Time.timeSinceLevelLoad;
            ChangePosition();

        }
	}
    private IEnumerator CallSheepDelay()
    {
        int i = 0;
        if(LevelData.Instance.Sheep !=null){
            while(i<LevelData.Instance.Sheep.Length){
                LevelData.Instance.Sheep[i].MoveToNewPosition(transform.position);
                i++;
                yield return new WaitForSeconds(delay);
            }
        }
        
    }
    private void ChangePosition()
    {
        if(LevelData.Instance.HerdWaypoints !=null){
            if (LevelData.Instance.HerdWaypoints.Length > 0)
            {
                int i = Random.Range(0, LevelData.Instance.HerdWaypoints.Length);
                transform.position = LevelData.Instance.HerdWaypoints[i].transform.position;
            }
        }
        StartCoroutine(CallSheepDelay());
    }

}
