using UnityEngine;
using System.Collections;

public class AnimateEndingTrees : MonoBehaviour {

    public bool playOnAwake;
    public AnimationCurve animCurveX;
    public AnimationCurve animCurveY;
    public AnimationCurve animCurveZ;


    private void Start()
    {
        if (Puzzle.Instance != null) Puzzle.Instance.PuzzleCompleted += StartAnimation;
    }
    private void Destroy()
    {
        if (Puzzle.Instance != null) Puzzle.Instance.PuzzleCompleted -= StartAnimation;
    }
    private void Update()
    {
        if (playOnAwake)
        {
            playOnAwake = false;
            StartAnimation();
        }
    }
    public void StartAnimation()
    {
        StartCoroutine("Animate");
    }
    private IEnumerator Animate()
    {
        Vector3 initialPos = transform.position;
        float ct = 0;
        float maxTimeOfCurves = Mathf.Max(new float[3]{animCurveX.length,animCurveY.length,animCurveZ.length});
        while (ct < maxTimeOfCurves)
        {
            ct += Time.fixedDeltaTime;
            transform.position = initialPos + new Vector3(animCurveX.Evaluate(ct),animCurveY.Evaluate(ct),animCurveZ.Evaluate(ct));
            yield return new WaitForFixedUpdate();
        }
    }
}
